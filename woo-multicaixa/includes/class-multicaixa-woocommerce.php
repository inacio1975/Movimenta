<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
}

/**
 * Our main class
 *
 */
final class Multicaixa_WooCommerce {
	
	/* Version */
	public $version = '1.0.9';

	/* IDs */
	public $multicaixa_id = 'multicaixa_proxypay';

	/* Debug */
	public $log = null;

	/* Internal variables */
	public $test_mode		   = false;
	public $url				   = false;
	public $wpml_active		   = false;
	public $wc_deposits_active = false;
	public $locale			   = null;
	public $out_link_utm	   = '';
	public $currency		   = 'AOA';
	public $min_amount		   = 0.01;
	public $max_amount		   = 99999999.99;
	public $ref_validity	   = '24 hours';

	/* Internal variables - For Multicaixa */
	public $multicaixa_settings = null;

	/* Single instance */
	protected static $_instance = null;

	/* Constructor */
	public function __construct() {
		$this->wpml_active		 = function_exists( 'icl_object_id' ) && function_exists( 'icl_register_string' );
		$this->wc_deposits_active  = function_exists( 'wc_deposits_woocommerce_is_active' );
		$this->out_link_utm		= '?utm_source='.rawurlencode( esc_url( home_url( '/' ) ) ).'&amp;utm_medium=link&amp;utm_campaign=multicaixa_proxypay_plugin';
		$this->multicaixa_settings = get_option( 'woocommerce_multicaixa_proxypay_settings', '' );
		$this->init_hooks();
	}

	/* Ensures only one instance of our plugin is loaded or can be loaded */
	public static function instance() {
		if ( is_null( self::$_instance ) ) {
			self::$_instance = new self();
		}
		return self::$_instance;
	}

	/* Hooks */
	private function init_hooks() {
		add_filter( 'woocommerce_payment_gateways', array( $this, 'woocommerce_add_payment_gateways' ) );
		add_action( 'add_meta_boxes', array( $this, 'multicaixa_order_metabox' ) );
		add_filter( 'woocommerce_order_data_store_cpt_get_orders_query', array( $this, 'multicaixa_woocommerce_order_data_store_cpt_get_orders_query' ), 10, 2 );
		add_action( 'plugins_loaded', array( $this, 'wpml_ajax_fix_locale' ) );
		add_action( 'plugins_loaded', array( $this, 'init_vars' ) );
		add_action( 'woocommerce_new_customer_note', array( $this, 'woocommerce_new_customer_note_fix_wpml' ), 1 );
	}

	/* Init some additional vars */
	function init_vars() {
		$this->test_mode = apply_filters( 'multicaixa_proxypay_test_mode', false );
		$this->url	   = $this->test_mode ? 'https://api.sandbox.proxypay.co.ao' : 'https://api.proxypay.co.ao';
	}

	/* Get Multicaixa setting */
	public function get_multicaixa_setting( $setting ) {
		return isset( $this->multicaixa_settings[$setting] ) ? $this->multicaixa_settings[$setting] : '';
	}

	/* Add settings link to plugin actions */
	public function add_settings_link( $links ) {
		$action_links = array(
			'multicaixa_settings' => '<a href="admin.php?page=wc-settings&amp;tab=checkout&amp;section='.$this->multicaixa_id.'">' . __( 'Multicaixa settings', 'woo-multicaixa' ) . '</a>',
		);
		return array_merge( $action_links, $links );
	}

	/* Add to WooCommerce */
	public function woocommerce_add_payment_gateways( $methods ) {
		$methods[] = 'WC_Multicaixa_WooCommerce';
		return $methods;
	}

	/* Debug / Log - Outside payment gateway class because we might need it even if the gateway is not initiated */
	public function debug_log( $gateway_id, $message, $level = 'debug', $debug_email = '', $email_message = '' ) {
		if ( !$this->log ) $this->log = wc_get_logger(); //Init log 
		$this->log->$level( $message, array( 'source' => $gateway_id ) );
		if ( $debug_email ) {
			wp_mail(
				trim( $debug_email ),
				$gateway_id.' - '.$message,
				$email_message
			);
		}
	}

	/* Format Multicaixa reference - Outside payment gateway class because we might need it even if the gateway is not initiated */
	public function format_multicaixa_ref( $ref ) {
		return apply_filters( 'multicaixa_proxypay_format_ref', trim( chunk_split( trim( $ref ), 3, ' ' ) ) );
	}

	/* Format Multicaixa validity date - Outside payment gateway class because we might need it even if the gateway is not initiated */
	public function format_multicaixa_validity_date( $date ) {
		$date = str_replace( 'T', ' ', substr( $date, 0, 16 ) );
		return $date;
		//return apply_filters( 'multicaixa_proxypay_format_validity_date', date_i18n( 'Y-m-d H:i', strtotime( $date ) ) );
	}

	/* Get validity date in iso */
	public function get_date_validity_in_iso( $ref_validity ) {
		$d = date_create( date_i18n( DateTime::ISO8601 ) );
		date_add( $d, date_interval_create_from_date_string( $ref_validity ) );
		return date_format( $d, DateTime::ISO8601 );
	}
	/* Get date in past in iso */
	public function get_past_date_in_iso( $time ) {
		$d = date_create( date_i18n( DateTime::ISO8601 ) );
		date_sub( $d, date_interval_create_from_date_string( $time ) );
		return date_format( $d, DateTime::ISO8601 );
	}

	/* Disable payment gateway if not Kwanza - Outside payment gateway class because we might implement other payment methods */
	public function disable_if_currency_not_kwanza( $available_gateways, $gateway_id ) {
		if ( isset( $available_gateways[$gateway_id] ) ) {
			if ( trim( get_woocommerce_currency() ) != $this->currency ) unset( $available_gateways[$gateway_id] );
		}
		return $available_gateways;
	}

	/* Get Multicaixa order details */
	public function get_multicaixa_order_details( $order_id ) {
		$order = wc_get_order( $order_id );
		$ent = $order->get_meta( '_'.$this->multicaixa_id.'_ent' );
		$ref = $order->get_meta( '_'.$this->multicaixa_id.'_ref' );
		$val = $order->get_meta( '_'.$this->multicaixa_id.'_val' );
		$end_datetime = $order->get_meta( '_'.$this->multicaixa_id.'_end_datetime' );
		if ( !empty( $ent ) &&  !empty( $ref ) &&  !empty( $val ) &&  !empty( $end_datetime ) ) {
			return array(
				'ent'		  => $ent,
				'ref'		  => $ref,
				'val'		  => $val,
				'end_datetime' => $end_datetime,
			);
		}
		return false;
	}

	/* Order metabox to show Multicaixa payment details - This will need to change when the order is no longer a WP post */
	public function multicaixa_order_metabox() {
		add_meta_box( $this->multicaixa_id, __( 'Multicaixa payment details', 'woo-multicaixa' ), array( $this, 'multicaixa_order_metabox_html' ), 'shop_order', 'side', 'core' );
	}
	public function multicaixa_order_metabox_html( $post ) {
		$order = wc_get_order( $post->ID );
		if ( $order->get_payment_method() == $this->multicaixa_id ) {
			if (
				$order_multicaixa_details = $this->get_multicaixa_order_details( $order->get_id() )
			) {
				echo '<p><strong>'.__( 'Multicaixa', 'woo-multicaixa' ).'</strong></p>';
				echo '<p>'.__( 'Entity', 'woo-multicaixa' ).': '.trim( $order_multicaixa_details['ent'] ).'<br/>';
				echo __( 'Reference', 'woo-multicaixa' ).': '.$this->format_multicaixa_ref( $order_multicaixa_details['ref'] ).'<br/>';
				echo __( 'Value', 'woo-multicaixa' ).': '.wc_price( $order_multicaixa_details['val'] ).'<br/>';
				echo __( 'Validity', 'woo-multicaixa' ).': '.$this->format_multicaixa_validity_date( $order_multicaixa_details['end_datetime'] ).'</p>';
				if ( $order->has_status( 'on-hold' ) || $order->has_status( 'pending' ) ) {
					echo '<p>'.__( 'Awaiting Multicaixa payment.', 'woo-multicaixa' ).'</p>';
					do_action( 'multicaixa_proxypay_metabox_on_hold', $order->get_id() );
				} else {
					//PAID?
				}
			} else {
				echo '<p>'.__( 'No details available', 'woo-multicaixa' ).'.</p><p>'.__( 'This must be an error because the payment method of this order is Multicaixa', 'woo-multicaixa' ).'.</p>';
			}
		} else {
			echo '<p>'.__( 'This order does not have Multicaixa as the payment gateway.', 'woo-multicaixa' ).'</p>';
			echo '<style type="text/css">#'.$this->multicaixa_id.' { display: none; }</style>';//If we have MB data, we should delete it
			if ( $order_multicaixa_details = $this->get_multicaixa_order_details( $order->get_id() ) ) {
				foreach ( $order_multicaixa_details as $key => $value ) {
					$order->delete_meta_data( '_'.$this->multicaixa_id.'_'.$key );
				}
				$order->save();
			}
		}
	}

	/* Set new order Entity/Reference/Value on meta */
	public function multicaixa_set_order_multicaixa_details( $order_id, $order_multicaixa_details ) {
		$order = wc_get_order( $order_id );
		$order->update_meta_data( '_'.$this->multicaixa_id.'_ent', $order_multicaixa_details['ent'] );
		$order->update_meta_data( '_'.$this->multicaixa_id.'_ref', $order_multicaixa_details['ref'] );
		$order->update_meta_data( '_'.$this->multicaixa_id.'_val', $order_multicaixa_details['val'] );
		$order->update_meta_data( '_'.$this->multicaixa_id.'_end_datetime', $order_multicaixa_details['end_datetime'] );
		$order->save();
	}

	/* Get/Create Multicaixa Reference */
	public function multicaixa_get_ref( $order_id, $force_change = false ) {
		$order = wc_get_order( $order_id );
		if ( trim( get_woocommerce_currency() ) == $this->currency ) {
			if (
				! $force_change
				&&
				$order_multicaixa_details = $this->get_multicaixa_order_details( $order_id )

			) {
				//Already created, return it!
				return $order_multicaixa_details;
			} else {
				//Value ok?
				if ( $order->get_total() < $this->min_amount ) {
					return sprintf( __( 'It\'s not possible to use Multicaixa to pay values under %s Kz.', 'woo-multicaixa' ), $this->min_amount );
			 	} else {
			 		//Value ok? (again)
					if ( $order->get_total() >= $this->max_amount ) {
						return sprintf( __( 'It\'s not possible to use Multicaixa to pay values above %s Kz.', 'woo-multicaixa' ), $this->max_amount );
					} else {
						//Create a new reference
						$base = apply_filters( 'multicaixa_proxypay_base_ent_subent', array( 'ent' => $this->get_multicaixa_setting('ent') ), $order );
						if (
							strlen( trim( $base['ent'] ) ) == 5
							&&
							intval( $base['ent'] ) > 0
							&&
							trim( $this->get_multicaixa_setting('api_key') ) != ''
						) {
							$ref = $this->multicaixa_create_ref( $base['ent'], rand( 0, 999999999 ), $order->get_total() );
							//ProxyPay API
							$data = apply_filters( 'multicaixa_api_create_ref_data', array(
								'amount'		=> (string) floatval( $order->get_total() ),
								'end_datetime'  => (string) Multicaixa_WooCommerce()->get_date_validity_in_iso( $this->ref_validity ),
								'custom_fields' => array(
									'plugin'		=> (string) $this->multicaixa_id,
									'order_id'	  => (string) $order_id,
									'client_name'   => (string) $order->get_formatted_billing_full_name(),
								),
							), $order_id, $ref );
							$this->debug_log( $this->multicaixa_id, 'Generating payment details with for Order '.$order_id.' with this data: '.serialize( $data ) );
							$api = Multicaixa_ProxyPay_API()->put( 'references/'.$ref, $data );
							if ( $api['success'] ) {
								//Store on the order for later use (like the email)
								$ref_details = array(
									'ent'		  => $base['ent'],
									'ref'		  => $ref,
									'val'		  => $order->get_total(),
									'end_datetime' => $data['end_datetime'],
								);
								$this->multicaixa_set_order_multicaixa_details( $order_id, $ref_details );
								$this->debug_log( $this->multicaixa_id, 'Multicaixa payment details ('.implode( ' / ', $ref_details ).') generated for Order '.$order_id );
								//$this->debug_log( $this->multicaixa_id, $api['args'] );
								//Return it!
								do_action( 'multicaixa_proxypay_created_reference', $ref_details, $order_id, $force_change );
								return $ref_details;
							} else {
								$error_details = 'HTTP Code: '.( $api['http_code'] ? $api['http_code'] : 'N/A' );
								$error_details .= ' / Error: '.( $api['error'] ? $api['error'] : 'N/A' );
								$error_details .= ' / Args: '.( $api['args'] ? $api['args'] : 'N/A' );
								$this->debug_log( $this->multicaixa_id, $error_details, 'warning', true, $error_details."\n\n".serialize( $api['response'] ) );
								return __( 'ProxyPay API error.', 'woo-multicaixa' );
							}
						} else {
							$error_details='';
							if ( trim( strlen( $base['ent'] ) ) != 5 || ( !intval( $base['ent'] ) > 0 ) ) {
								$error_details = __( 'Entity missing', 'woo-multicaixa' );
							} else {
								if ( trim( $this->get_multicaixa_setting('api_key') ) == '' ) {
									$error_details = __( 'API key missing', 'woo-multicaixa' );
								}
							}
							$this->debug_log( $this->multicaixa_id, $error_details, 'warning', true, $error_details );
							return __( 'Configuration error. This payment method is disabled because required information was not set.', 'woo-multicaixa' ).' '.$error_details.'.';
						}
					}
				}
			}
		} else {
			$error_details = __( 'Configuration error. This store currency is not Kwanza (Kz).', 'woo-multicaixa' );
			$this->debug_log( $this->multicaixa_id, $error_details, 'warning', true, $error_details );
			return $error_details;
		}
	}
	public function multicaixa_create_ref( $ent, $seed, $total, $just_create_no_check = false ) {
		$ref = str_pad( intval( $seed ), 9, "0", STR_PAD_LEFT );
		//Does it exists already? Let's browse the database!
		if ( ! $just_create_no_check ) {
			//WE HAVE TO CHECK for on-hold / pending and we can also check for validity, maybe?
			$exists = false;
			$orders = wc_get_orders( array(
				'type'	=> array( 'shop_order' ),
				'limit'	=> 1, //If there's one, it's enough
				//'_'.$this->multicaixa_id.'_ent' => $ent, //We do not check for entity, only reference
				'_'.$this->multicaixa_id.'_ref' => $ref,
				'status' => array( 'wc-on-hold', 'wc-pending' ),
			) );
			if ( count($orders) > 0 ) $exists = true;
			if ( $exists ) {
				//Reference exists - Let's try again
				//$seed = intval( $seed ) + 1; //For incremental mode
				$this->debug_log( $this->multicaixa_id, 'Reference '.$ref.' already exists, trying another one', 'debug' );
				$seed = rand( 0, 999999999 ); //For random mode - Less loop possibility
				$ref = $this->multicaixa_create_ref( $ent, $seed, $total );
			}
		} else {
			//No checking - for tests only
		}
		return $ref;
	}

	/* Filter to be able to use wc_get_orders with our Multicaixa references */
	public function multicaixa_woocommerce_order_data_store_cpt_get_orders_query( $query, $query_vars ) {
		//Multicaixa - Entity
		if ( ! empty( $query_vars['_'.$this->multicaixa_id.'_ent'] ) ) {
			$query['meta_query'][] = array(
				'key' => '_'.$this->multicaixa_id.'_ent',
				'value' => esc_attr( $query_vars['_'.$this->multicaixa_id.'_ent'] ),
			);
		}
		//Multicaixa - Reference
		if ( ! empty( $query_vars['_'.$this->multicaixa_id.'_ref'] ) ) {
			$query['meta_query'][] = array(
				'key' => '_'.$this->multicaixa_id.'_ref',
				'value' => esc_attr( $query_vars['_'.$this->multicaixa_id.'_ref'] ),
			);
		}
		//Multicaixa - Validity
		if ( ! empty( $query_vars['_'.$this->multicaixa_id.'_end_datetime'] ) ) {
			$query['meta_query'][] = array(
				'key' => '_'.$this->multicaixa_id.'_end_datetime',
				'value' => esc_attr( $query_vars['_'.$this->multicaixa_id.'_end_datetime'] ),
			);
		}
		if ( ! empty( $query_vars['_'.$this->multicaixa_id.'_end_datetime_expired'] ) ) {
			$query['meta_query'][] = array(
				'key' => '_'.$this->multicaixa_id.'_end_datetime',
				'value' => esc_attr( $query_vars['_'.$this->multicaixa_id.'_end_datetime_expired'] ),
				'compare' => '<',
			);
		}
		return $query;
	}

	/* Set email correct language - Stolen from WCML emails.class.php - Not sure if this is still needed */
	public function change_email_language( $lang ) {
		global $sitepress;
		//Unload
		unload_textdomain( 'woo-multicaixa' );
		if ( $lang == 'en' ) {
			//English? Just use plugin default strings
		} else {
			$this->locale = $sitepress->get_locale( $lang );
			add_filter( 'plugin_locale', array( $this, 'set_locale_for_emails' ), 10, 2 );
			load_plugin_textdomain( 'woo-multicaixa' );
			remove_filter( 'plugin_locale', array( $this, 'set_locale_for_emails' ), 10, 2 );
		}
	}
	public function set_locale_for_emails( $locale, $domain ) {
		if ( $domain == 'woo-multicaixa' && $this->locale ) {
			$locale = $this->locale;
		}
		return $locale;
	}

	/* WPML AJAX fix locale */
	public function wpml_ajax_fix_locale() {
		//If WPML is present and we're loading via ajax, let's try to fix the locale
		if ( $this->wpml_active ) {
			if ( function_exists( 'wpml_is_ajax' ) && wpml_is_ajax() ) {  //We check the function because we may be using Polylang
				if ( ICL_LANGUAGE_CODE != 'en' ) {
					add_filter( 'plugin_locale', array( $this, 'wpml_ajax_fix_locale_do_it' ), 1, 2 );
				}
			}
		}
	}
	/* This should NOT be needed! - Check with WooCommerce Multilingual team */
	public function wpml_ajax_fix_locale_do_it( $locale, $domain ) {
		if ( $domain == 'woo-multicaixa' ) {
			global $sitepress;
			$locales = icl_get_languages_locales();
			if ( isset( $locales[ICL_LANGUAGE_CODE] ) ) $locale = $locales[ICL_LANGUAGE_CODE];
			//But if it's notes
			if ( $this->locale ) $locale = $this->locale;
		}
		return $locale;
	}
	/* Languages on Notes emails - We need to check if it's our order (Multicaixa) */
	public function woocommerce_new_customer_note_fix_wpml( $order_id ) {
		if ( is_array( $order_id ) ){
			if ( isset( $order_id['order_id'] ) ) {
				$order_id = $order_id['order_id'];
			} else {
				return;
			}
		}
		if ( $this->wpml_active ) {
			$this->woocommerce_new_customer_note_fix_wpml_do_it( $order_id );
		}
	}
	public function woocommerce_new_customer_note_fix_wpml_do_it( $order_id ) {
		global $sitepress;
		$order = wc_get_order( $order_id );
		$lang = $order->get_meta( 'wpml_language' );
		if( !empty( $lang ) && $lang != $sitepress->get_default_language() ){
			$this->locale = $sitepress->get_locale( $lang ); //Set global to be used on wpml_ajax_fix_locale_do_it above
			add_filter( 'plugin_locale', array( $this, 'wpml_ajax_fix_locale_do_it' ), 1, 2 );
			multicaixa_load_textdomain();
		}
	}

	/* Right sidebar on payment gateway settings - Outside payment gateway class because we might implement other payment methods */
	public function admin_right_bar() {
		?>
		<div id="multicaixa_rightbar">
			<h4><?php _e( 'Commercial information', 'woo-multicaixa' ); ?>:</h4>
			<p><a href="https://proxypay.co.ao/<?php echo esc_attr( $this->out_link_utm); ?>" title="<?php echo esc_attr( sprintf( __( 'Please contact %s', 'woo-multicaixa' ), 'ProxyPay' ) ); ?>" target="_blank"><img src="<?php echo plugins_url( '../images/proxypay.svg', __FILE__ ); ?>" width="200"/></a></p>
			<h4><?php _e( 'Technical support or custom WordPress/WooCommerce development', 'woo-multicaixa' ); ?>:</h4>
			<p><a href="https://www.webdados.pt/contactos/<?php echo esc_attr( $this->out_link_utm); ?>" title="<?php echo esc_attr( sprintf( __( 'Please contact %s', 'woo-multicaixa' ), 'Webdados' ) ); ?>" target="_blank"><img src="<?php echo plugins_url( '../images/webdados.svg', __FILE__ ); ?>" width="200"/></a></p>
			<h4><?php _e( 'Please rate our plugin at WordPress.org', 'woo-multicaixa' ); ?>:</h4>
			<a href="https://wordpress.org/support/view/plugin-reviews/woo-multicaixa?filter=5#postform" target="_blank" style="text-align: center; display: block;">
				<div class="star-rating"><div class="star star-full"></div><div class="star star-full"></div><div class="star star-full"></div><div class="star star-full"></div><div class="star star-full"></div></div>
			</a>
			<div class="clear"></div>
		</div>
		<?php
	}

}