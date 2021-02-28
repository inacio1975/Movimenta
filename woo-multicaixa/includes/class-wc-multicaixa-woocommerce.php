<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
}

/**
 * Multicaixa ProxyPay Class
 *
 */
if ( ! class_exists( 'WC_Multicaixa_WooCommerce' ) ) {

	class WC_Multicaixa_WooCommerce extends WC_Payment_Gateway {

		/* Single instance */
		protected static $_instance = null;
		public static $instances = 0;
		
		/**
		 * Constructor for your payment class
		 *
		 * @access public
		 * @return void
		 */
		public function __construct() {

			self::$instances++;

			$this->id = Multicaixa_WooCommerce()->multicaixa_id;
	
			// Logs
			$this->debug = ( $this->get_option( 'debug' ) == 'yes' ? true : false );
			$this->debug_email = $this->get_option( 'debug_email' );
			
			//Check version and upgrade
			$this->version = Multicaixa_WooCommerce()->version;
			$this->upgrade();
	
			$this->icon = plugins_url( '../images/icon_multicaixa_48.png', __FILE__ );
			$this->icon_svg = plugin_dir_path( __FILE__ ).'../images/icon_multicaixa_48.svg';
			$this->banner = plugins_url( '../images/banner_multicaixa.png', __FILE__ );
			$this->banner_svg = plugin_dir_path( __FILE__ ).'../images/banner_multicaixa.svg';
			$this->has_fields = false;

			$this->method_title = __( 'Pagamento por Referência no Multicaixa (ProxyPay)', 'woo-multicaixa' );
			$this->method_description = __( 'Easy and simple payment using "Pagamento por Referência" at any "Multicaixa" ATM terminal or your Home Banking service. (Only available to customers of Angolan banks.)', 'woo-multicaixa' );
			$this->api_key = $this->get_option( 'api_key' );
	
			//Plugin options and settings
			$this->init_form_fields();
			$this->init_settings();
	
			//User settings
			$this->title = $this->get_option( 'title' );
			$this->description = $this->get_option( 'description' );
			$this->extra_instructions = $this->get_option( 'extra_instructions' );
			$this->ent = $this->get_option( 'ent' );
			$this->settings_saved = $this->get_option( 'settings_saved' );
			$this->send_to_admin = ( $this->get_option( 'send_to_admin' )=='yes' ? true : false );
	 	
			// Actions and filters
			if ( self::$instances == 1 ) { //Avoid duplicate actions and filters if it's initiated more than once (if WooCommerce loads after us)
				add_action( 'woocommerce_update_options_payment_gateways_'.$this->id, array( $this, 'process_admin_options' ) );
				add_action( 'woocommerce_update_options_payment_gateways_' . $this->id, array( $this, 'process_admin_options_pro' ) );
				if ( Multicaixa_WooCommerce()->wpml_active ) add_action( 'woocommerce_update_options_payment_gateways_' . $this->id, array( $this, 'register_wpml_strings' ) );
				add_action( 'woocommerce_thankyou_'.$this->id, array( $this, 'thankyou' ) );
				add_action( 'woocommerce_order_details_after_order_table', array( $this, 'order_details_after_order_table' ), 9 );
				add_filter( 'woocommerce_available_payment_gateways', array( $this, 'disable_if_currency_not_kwanza' ) );
				// Customer Emails
				//add_action( 'woocommerce_email_before_order_table', array( $this, 'email_instructions' ), 10, 3 ); - "Hyyan WooCommerce Polylang Integration" removes this action
				add_action( 'woocommerce_email_before_order_table', array( $this, 'email_instructions_1' ), 10, 3 ); //Avoid "Hyyan WooCommerce Polylang Integration" remove_action
			}

			// Ensures only one instance of our plugin is loaded or can be loaded - works if WooCommerce loads the payment gateways before we do
			if ( is_null( self::$_instance ) ) {
				self::$_instance = $this;
			}
			
		}

		/* Ensures only one instance of our plugin is loaded or can be loaded */
		public static function instance() {
			if ( is_null( self::$_instance ) ) {
				self::$_instance = new self();
			}
			return self::$_instance;
		}

		/**
		 * Upgrades (if needed)
		 */
		function upgrade() {
			if ( $this->get_option( 'version' ) < $this->version ) {
				//Upgrade
				$this->debug_log( 'Upgrade to '.$this->version.' started' );
				//Upgrade on the database - Risky?
				$temp = get_option( 'woocommerce_'.$this->id.'_settings', '' );
				if ( !is_array($temp) ) $temp = array();
				$temp['version'] = $this->version;
				update_option( 'woocommerce_'.$this->id.'_settings', $temp );
				$this->debug_log( 'Upgrade to '.$this->version.' finished' );
			}
		}

		/**
		 * WPML compatibility
		 */
		function register_wpml_strings() {
			//These are already registered by WooCommerce Multilingual
			/*$to_register=array(
				'title',
				'description',
			);*/
			$to_register = apply_filters( 'multicaixa_proxypay_wpml_strings', array(
				'extra_instructions'
			) );
			foreach( $to_register as $string ) {
				icl_register_string( $this->id, $this->id.'_'.$string, $this->settings[$string] );
			}
		}

		/**
		 * Initialise Gateway Settings Form Fields
		 * 'setting-name' => array(
		 *		'title' => __( 'Title for setting', 'woothemes' ),
		 *		'type' => 'checkbox|text|textarea',
		 *		'label' => __( 'Label for checkbox setting', 'woothemes' ),
		 *		'description' => __( 'Description for setting' ),
		 *		'default' => 'default value'
		 *	),
		 */
		function init_form_fields() {
		
			$this->form_fields = array(
				'enabled' => array(
								'title' => __( 'Enable/Disable', 'woo-multicaixa' ), 
								'type' => 'checkbox', 
								'label' => __( 'Enable "Pagamento por Referência no Multicaixa" (using ProxyPay)', 'woo-multicaixa' ), 
								'default' => 'no'
							),
				'ent' => array(
								'title' => __( 'Entity', 'woo-multicaixa' ), 
								'type' => 'number',
								'description' => __( 'Entity provided by your bank.', 'woo-multicaixa' ), 
								'default' => '',
								'css' => 'width: 80px;',
								'custom_attributes' => array(
									'maxlength'	=> 5,
									'size' => 5,
									'max' => 99999
								)
							),
			);
			//if( strlen( $this->get_option( 'ent' ) ) == 5 && intval( $this->get_option( 'ent' ) )>0 && trim( $this->api_key ) !='' ) {
				$this->form_fields = array_merge( $this->form_fields, array(
					'api_key' => array(
									'title' => __( 'API key', 'woo-multicaixa' ), 
									'type' => 'text', 
									'description' => __( 'The API key provided by ProxyPay.', 'woo-multicaixa' ).'<br/>'.(
										Multicaixa_WooCommerce()->test_mode
										?
										'<strong class="multicaixa_error">'.__( 'Test mode enabled.', 'woo-multicaixa' ).'</strong>'
										:
										'<strong class="multicaixa_ok">'.__( 'Live mode enabled.', 'woo-multicaixa' ).'</strong>'
									),
								),
					'title' => array(
									'title' => __( 'Title', 'woo-multicaixa' ), 
									'type' => 'text', 
									'description' => __( 'This controls the title which the user sees during checkout.', 'woo-multicaixa' )
													.( Multicaixa_WooCommerce()->wpml_active ? ' '.__( 'You should translate this string in <a href="admin.php?page=wpml-string-translation%2Fmenu%2Fstring-translation.php">WPML - String Translation</a> after saving the settings', 'woo-multicaixa' ) : '' ), 
									'default' => __( 'Pagamento por Referência no Multicaixa', 'woo-multicaixa' )
								),
					'description' => array(
									'title' => __( 'Description', 'woo-multicaixa' ), 
									'type' => 'textarea',
									'description' => __( 'This controls the description which the user sees during checkout.', 'woo-multicaixa' )
													.( Multicaixa_WooCommerce()->wpml_active ? ' '.__( 'You should translate this string in <a href="admin.php?page=wpml-string-translation%2Fmenu%2Fstring-translation.php">WPML - String Translation</a> after saving the settings', 'woo-multicaixa' ) : '' ), 
									'default' => $this->get_method_description()
								),
					'extra_instructions' => array(
									'title' => __( 'Extra instructions', 'woo-multicaixa' ), 
									'type' => 'textarea',
									'description' => __( 'This controls the text which the user sees below the payment details on the "Thank you" page and "New order" email.', 'woo-multicaixa' )
													.( Multicaixa_WooCommerce()->wpml_active ? ' '.__( 'You should translate this string in <a href="admin.php?page=wpml-string-translation%2Fmenu%2Fstring-translation.php">WPML - String Translation</a> after saving the settings', 'woo-multicaixa' ) : '' ), 
									'default' => __( 'The receipt issued by the ATM machine is a proof of payment. Keep it.', 'woo-multicaixa' )
								),
					'send_to_admin' => array(
									'title' => __( 'Send instructions to admin?', 'woo-multicaixa' ), 
									'type' => 'checkbox', 
									'label' => __( 'Should the payment details also be sent to admin?', 'woo-multicaixa' ), 
									'default' => 'yes'
								),
					'debug' => array(
									'title' => __( 'Debug Log', 'woo-multicaixa' ),
									'type' => 'checkbox',
									'label' => __( 'Enable logging', 'woo-multicaixa' ),
									'default' => 'yes',
									'description' => sprintf(
														__( 'Log plugin events in %s', 'woo-multicaixa' ),
														( defined( 'WC_LOG_HANDLER' ) && 'WC_Log_Handler_DB' === WC_LOG_HANDLER )
														?
														'<a href="admin.php?page=wc-status&tab=logs&source='.esc_attr( $this->id ).'" target="_blank">'.__( 'WooCommerce &gt; Status &gt; Logs', 'woo-multicaixa' ).'</a>'
														:
														'<code>'.wc_get_log_file_path( $this->id ).'</code>'
													),
								),
					'debug_email' => array(
									'title' => __( 'Debug to email', 'woo-multicaixa' ),
									'type' => 'email',
									'label' => __( 'Enable email logging', 'woo-multicaixa' ),
									'default' => '',
									'description' => __( 'Send plugin events to this email address.', 'woo-multicaixa' ),
								)
				)	);
			//}
			$this->form_fields = array_merge( $this->form_fields , array(
				'settings_saved' => array(
								'title' => '', 
								'type' => 'hidden',
								'default' => 0
							),
			) );

			//Allow other plugins to add settings fields
			$this->form_fields = array_merge( $this->form_fields , apply_filters( 'multicaixa_proxypay_multicaixa_settings_fields', array( ) ) );
		
		}
		public function admin_options() {
			$title = esc_html( $this->get_method_title() );
			?>
			<div id="multicaixa_leftbar">
				<?php Multicaixa_WooCommerce()->admin_right_bar(); ?>
				<div id="multicaixa_leftbar_settings">
					<h2>
						<?php
						if ( apply_filters( 'multicaixa_proxypay_use_svg', true ) ) {
							?>
							<img src='data:image/svg+xml;base64,<?php echo base64_encode( file_get_contents( $this->banner_svg ) ); ?>' alt="<?php echo esc_attr( $title ); ?>" title="<?php echo esc_attr( $title ); ?>" width="200"/>
							<?php
						} else {
							?>
							<img src="<?php echo esc_attr( $this->banner  ); ?>" alt="<?php echo esc_attr( $title ); ?>" title="<?php echo esc_attr( $title ); ?>"/>
							<?php
						}
						?>
						<br/>
						<?php echo $title; ?>
						<small>v.<?php echo $this->version; ?></small>
						<?php if ( function_exists('wc_back_link') ) echo wc_back_link( __( 'Return to payments', 'woocommerce' ), admin_url( 'admin.php?page=wc-settings&tab=checkout' ) ); ?>
					</h2>
					<?php echo wp_kses_post( wpautop( $this->get_method_description() ) ); ?>
					<p><strong><?php _e( 'In order to use this plugin you <u>must</u>:', 'woo-multicaixa' ); ?></strong></p>
					<ul class="multicaixa_leftbar_list">
						<li><?php printf( __( 'Set WooCommerce currency to <strong>Angolan Kwanza</strong> %1$s', 'woo-multicaixa' ), '<a href="admin.php?page=wc-settings&amp;tab=general">&gt;&gt;</a>.' ); ?></li>
						<li><?php printf( __( 'Sign a contract with a bank and %1$s. To know more about this service, please go to %2$s.', 'woo-multicaixa' ), '<strong><a href="https://proxypay.co.ao/'.esc_attr( Multicaixa_WooCommerce()->out_link_utm ).'" target="_blank">ProxyPay</a></strong>', '<a href="https://proxypay.co.ao/'.esc_attr( Multicaixa_WooCommerce()->out_link_utm ).'" target="_blank">https://proxypay.co.ao/</a>' ); ?></li>
						<li><?php _e( 'Fill out all the details (Entity and API key) provided by your bank and <strong>ProxyPay</strong> in the fields below.', 'woo-multicaixa' ); ?>
					</ul>
					<?php
					$hide_extra_fields=false;
					if(
						trim( strlen( $this->ent ) ) == 5
						&&
						intval( $this->ent ) > 0
						&&
						trim( $this->api_key ) != ''
					) {
						//OK
					} else {
						$hide_extra_fields = true;
						if ( $this->settings_saved == 1 ) {
							?>
							<div id="message" class="error">
								<p><strong><?php _e( 'Invalid Entity (exactly 5 numeric characters) and/or API key.', 'woo-multicaixa' ); ?></strong></p>
							</div>
							<?php
						} else {
							?>
							<div id="message" class="error">
								<p><strong><?php _e( 'Set the Entity and API key and Save changes to set other plugin options.', 'woo-multicaixa' ); ?></strong></p>
							</div>
							<?php
						}
					}
					?>
					<hr/>
					<script type="text/javascript">
					jQuery( document ).ready( function() {
						<?php if ( $hide_extra_fields ) { ?>
							//Hide extra fields if there are errors on Entity or Subentity
							jQuery( '#multicaixa_leftbar_settings table.form-table tr:nth-child(n+4)' ).hide();
							jQuery( '#multicaixa_leftbar_settings .mb_hide_extra_fields' ).hide();
						<?php } ?>
						//Settings saved
						jQuery( '#woocommerce_<?php echo $this->id; ?>_settings_saved' ).val( '1' );
					});
					</script>
					<table class="form-table">
					<?php
					if ( trim( get_woocommerce_currency() ) == Multicaixa_WooCommerce()->currency ) {
						$this->generate_settings_html();
					} else {
						?>
						<p><strong><?php _e( 'ERROR!', 'woo-multicaixa' ); ?> <?php printf( __( 'Set WooCommerce currency to <strong>Angolan Kwanza</strong> %1$s', 'woo-multicaixa' ), '<a href="admin.php?page=wc-settings&amp;tab=general">'.__( 'here', 'woo-multicaixa' ).'</a>.' ); ?></strong></p>
						<?php
					}
					?>
					</table>
				</div>
			</div>
			<div class="clear"></div>
			<style type="text/css">
				#multicaixa_rightbar {
					display: none;
				}
				@media (min-width: 961px) {
					#multicaixa_leftbar {
						height: auto;
						overflow: hidden;
					}
					#multicaixa_leftbar_settings {
						width: auto;
						overflow: hidden;
					}
					#multicaixa_rightbar {
						display: block;
						float: right;
						width: 200px;
						max-width: 20%;
						margin-left: 20px;
						padding: 15px;
						background-color: #fff;
					}
					#multicaixa_rightbar h4:first-child {
						margin-top: 0px;
					}
					#multicaixa_rightbar p {
					}
					#multicaixa_rightbar p img {
						max-width: 100%;
						height: auto;
					}
				}
				.multicaixa_leftbar_list {
					list-style-type: disc;
					list-style-position: inside;
				}
				.multicaixa_leftbar_list li {
					margin-left: 1.5em;
				}
				.multicaixa_error {
					color: #dc3232;
				}
				.multicaixa_ok {
					color: #46b450;
				}
			</style>
			<?php
			do_action( 'multicaixa_proxypay_after_settings' );
		}

		/**
		 * Icon HTML
		 */
		public function get_icon() {
			$alt = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_title', $this->title ) : $this->title );
			if ( apply_filters( 'multicaixa_proxypay_use_svg', true ) ) {
				$icon_html = '<img src=\'data:image/svg+xml;base64,'.base64_encode( file_get_contents( $this->icon_svg ) ).'\' alt="'.esc_attr( $alt ).'" width="24" height="24">';
			} else {
				$icon_html = '<img src="'.esc_attr( $this->icon ).'" alt="'.esc_attr( $alt ).'" width="24" height="24"/>';
			}
			return apply_filters( 'woocommerce_gateway_icon', $icon_html, $this->id );
		}

		/**
		 * Thank you page
		 */
		function thankyou( $order_id ) {
			if ( is_object( $order_id ) ) {
				$order = $order_id;
				$order_id = $order->get_id();
			} else {
				$order = wc_get_order( $order_id );
			}
			if ( $this->id === $order->get_payment_method() ) {
				if ( $order->has_status( 'on-hold' ) || $order->has_status( 'pending' ) ) {
					$ref = Multicaixa_WooCommerce()->multicaixa_get_ref( $order_id );
					if ( is_array( $ref ) ) {
						echo $this->thankyou_instructions_table_html( $ref['ent'], $ref['ref'], $ref['val'], $ref['end_datetime'], $order_id );
					} else {
						?>
						<p><strong><?php _e( 'Error getting Multicaixa payment details', 'woo-multicaixa' ); ?>.</strong></p>
						<?php
						if ( is_string( $ref ) ) {
							?>
							<p><?php echo $ref; ?></p>
							<?php
						}
					}
				} else {
					//Processing - Not needed
					//if ( $order->has_status( 'processing' ) && !is_wc_endpoint_url( 'view-order') ) {
					//	echo $this->email_instructions_payment_received( $order_id );
					//}
				}
			}
		}
		function thankyou_instructions_table_html( $ent, $ref, $order_total, $end_datetime, $order_id ) {
			$alt = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_title', $this->title ) : $this->title );
			$extra_instructions = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_extra_instructions', $this->extra_instructions ) : $this->extra_instructions );
			ob_start();
			?>
			<style type="text/css">
				table.<?php echo $this->id; ?>_table {
					width: auto !important;
					margin: auto;
					margin-top: 2em;
					margin-bottom: 2em;
				}
				table.<?php echo $this->id; ?>_table td,
				table.<?php echo $this->id; ?>_table th {
					background-color: #FFFFFF;
					color: #000000;
					padding: 10px;
					vertical-align: middle;
					white-space: nowrap;
				}
				@media only screen and (max-width: 450px)  {
					table.<?php echo $this->id; ?>_table td,
					table.<?php echo $this->id; ?>_table th {
						white-space: normal;
					}
				}
				table.<?php echo $this->id; ?>_table th {
					text-align: center;
					font-weight: bold;
				}
				table.<?php echo $this->id; ?>_table th img {
					margin: auto;
					margin-top: 10px;
					max-width: 200px;
				}
			</style>
			<table class="<?php echo $this->id; ?>_table" cellpadding="0" cellspacing="0">
				<tr>
					<th colspan="2">
						<?php _e( 'Payment instructions', 'woo-multicaixa' ); ?>
						<br/>
						<?php
						if ( apply_filters( 'multicaixa_proxypay_use_svg', true ) ) {
							?>
							<img src='data:image/svg+xml;base64,<?php echo base64_encode( file_get_contents( $this->banner_svg ) ); ?>' alt="<?php echo esc_attr( $alt ); ?>" title="<?php echo esc_attr( $alt ); ?>"/>
							<?php
						} else {
							?>
							<img src="<?php echo esc_attr( $this->banner  ); ?>" alt="<?php echo esc_attr( $alt ); ?>" title="<?php echo esc_attr( $alt ); ?>"/>
							<?php
						}
						?>
					</th>
				</tr>
				<tr>
					<td><?php _e( 'Entity', 'woo-multicaixa' ); ?>:</td>
					<td><?php echo $ent; ?></td>
				</tr>
				<tr>
					<td><?php _e( 'Reference', 'woo-multicaixa' ); ?>:</td>
					<td><?php echo Multicaixa_WooCommerce()->format_multicaixa_ref( $ref ); ?></td>
				</tr>
				<tr>
					<td><?php _e( 'Value', 'woo-multicaixa' ); ?>:</td>
					<td><?php echo $order_total; ?> Kz</td>
				</tr>
				<tr>
					<td><?php _e( 'Validity', 'woo-multicaixa' ); ?>:</td>
					<td><?php echo Multicaixa_WooCommerce()->format_multicaixa_validity_date( $end_datetime ); ?></td>
				</tr>
				<tr>
					<td colspan="2" style="font-size: small;"><?php echo nl2br( $extra_instructions ); ?></td>
				</tr>
			</table>
			<?php
			return apply_filters( 'multicaixa_proxypay_thankyou_instructions_table_html', ob_get_clean(), $ent, $ref, $order_total, $end_datetime, $order_id );
		}
		function order_details_after_order_table( $order ) {
			if( is_wc_endpoint_url( 'view-order') ) {
				$this->thankyou( $order );
			}
		}
		



		/**
		 * Email instructions
		 */
		function email_instructions_1( $order, $sent_to_admin, $plain_text ) { //"Hyyan WooCommerce Polylang" Integration removes "email_instructions" so we use "email_instructions_1"
			$this->email_instructions( $order, $sent_to_admin, $plain_text );
		}
		function email_instructions( $order, $sent_to_admin, $plain_text ) {
			//Avoid duplicate email instructions on some edge cases
			$send = false;
			if ( ( $sent_to_admin ) ) {
				$send = true;
			} else {
				if ( ( !$sent_to_admin ) ) {
					$send = true;
				}
			}
			//Send
			if ( $send ) {
				$order_id = $order->get_id();
				//Go
				if ( $this->id === $order->get_payment_method() ) {
					$show = false;
					if ( !$sent_to_admin ) {
						$show = true;
					} else {
						if ( $this->send_to_admin ) {
							$show = true;
						}
					}
					if ( $show ) {
						//WPML - Force correct language (?)
						if ( Multicaixa_WooCommerce()->wpml_active ) {
							global $sitepress;
							if ( $sitepress ) {
								$lang = $order->get_meta( 'wpml_language' );
								if( !empty( $lang ) ){
									Multicaixa_WooCommerce()->change_email_language( $lang );
								}
							}
						}
						//On Hold or pending
						if ( $order->has_status( 'on-hold' ) || $order->has_status( 'pending' ) || $order->has_status( 'partially-paid' ) ) {
							//if ( Multicaixa_WooCommerce()->wc_deposits_active && $order->get_status() == 'partially-paid' ) {
								//WooCommerce deposits - No instructions
							//} else {
								$ref = Multicaixa_WooCommerce()->multicaixa_get_ref( $order_id );
								if ( is_array( $ref) ) {
									if ( apply_filters( 'multicaixa_proxypay_email_instructions_pending_send', true, $order_id ) ) {
										echo $this->email_instructions_table_html( $ref['ent'], $ref['ref'], $ref['val'], $ref['end_datetime'], $order_id );
									}
								} else {
									?>
									<p><strong><?php _e( 'Error getting Multicaixa payment details', 'woo-multicaixa' ); ?>.</strong></p>
									<?php
									if ( is_string( $ref ) ) {
										?>
										<p><?php echo $ref; ?></p>
										<?php
									}
								}
							//}
						} else {
							//Processing
							if ( $order->has_status( 'processing' ) ) {
								if ( apply_filters( 'multicaixa_proxypay_email_instructions_payment_received_send', true, $order_id ) ) {
									echo $this->email_instructions_payment_received( $order_id );
								}
							}
						}
					}
					//$this->debug_log( 'Email instructions show: '.( $show ? 'true' : 'false' ) );
				}
			}
		}
		function email_instructions_table_html( $ent, $ref, $order_total, $end_datetime, $order_id ) {
			$alt = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_title', $this->title ) : $this->title );
			$extra_instructions = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_extra_instructions', $this->extra_instructions ) : $this->extra_instructions );
			ob_start();
			?>
			<table cellpadding="10" cellspacing="0" align="center" border="0" style="margin: auto; margin-top: 2em; margin-bottom: 2em; border-collapse: collapse; border: 1px solid #0B3258; border-radius: 4px !important; background-color: #FFFFFF;">
				<tr>
					<td style="border: 1px solid #0B3258; border-top-right-radius: 4px !important; border-top-left-radius: 4px !important; text-align: center; color: #000000; font-weight: bold;" colspan="2">
						<?php _e( 'Payment instructions', 'woo-multicaixa' ); ?>
						<br/>
						<img src="<?php echo esc_attr( $this->banner  ); ?>" alt="<?php echo esc_attr( $alt ); ?>" title="<?php echo esc_attr( $alt ); ?>" style="margin-top: 10px; max-width: 200px; height: auto;"/>
					</td>
				</tr>
				<tr>
					<td style="border: 1px solid #0B3258; color: #000000;"><?php _e( 'Entity', 'woo-multicaixa' ); ?>:</td>
					<td style="border: 1px solid #0B3258; color: #000000; white-space: nowrap;"><?php echo $ent; ?></td>
				</tr>
				<tr>
					<td style="border: 1px solid #0B3258; color: #000000;"><?php _e( 'Reference', 'woo-multicaixa' ); ?>:</td>
					<td style="border: 1px solid #0B3258; color: #000000; white-space: nowrap;"><?php echo Multicaixa_WooCommerce()->format_multicaixa_ref( $ref ); ?></td>
				</tr>
				<tr>
					<td style="border: 1px solid #0B3258; color: #000000;"><?php _e( 'Value', 'woo-multicaixa' ); ?>:</td>
					<td style="border: 1px solid #0B3258; color: #000000; white-space: nowrap;"><?php echo $order_total; ?> Kz</td>
				</tr>
				<tr>
					<td style="border: 1px solid #0B3258; color: #000000;"><?php _e( 'Validity', 'woo-multicaixa' ); ?>:</td>
					<td style="border: 1px solid #0B3258; color: #000000; white-space: nowrap;"><?php echo Multicaixa_WooCommerce()->format_multicaixa_validity_date( $end_datetime ); ?></td>
				</tr>
				<tr>
					<td style="font-size: x-small; border: 1px solid #0B3258; border-bottom-right-radius: 4px !important; border-bottom-left-radius: 4px !important; color: #000000; text-align: center;" colspan="2"><?php echo nl2br( $extra_instructions ); ?></td>
				</tr>
			</table>
			<?php
			return apply_filters( 'multicaixa_proxypay_email_instructions_table_html', ob_get_clean(), $ent, $ref, $order_total, $end_datetime, $order_id );
		}
		function email_instructions_payment_received( $order_id ) {
			$alt = ( Multicaixa_WooCommerce()->wpml_active ? icl_t( $this->id, $this->id.'_title', $this->title ) : $this->title );
			ob_start();
			?>
			<p style="text-align: center; margin: auto; margin-top: 2em; margin-bottom: 2em;">
				<img src="<?php echo esc_attr( $this->banner  ); ?>" alt="<?php echo esc_attr( $alt ); ?>" title="<?php echo esc_attr( $alt ); ?>" style="margin-top: 10px; max-width: 200px; height: auto;"/>
				<br/>
				<strong><?php _e( 'Multicaixa payment received.', 'woo-multicaixa' ); ?></strong>
				<br/>
				<?php _e( 'We will now process your order.', 'woo-multicaixa' ); ?>
			</p>
			<?php
			return apply_filters( 'multicaixa_proxypay_email_instructions_payment_received', ob_get_clean(), $order_id );
		}

		/**
		 * Process it
		 */
		function process_payment( $order_id ) {
			$order = wc_get_order( $order_id );
			$ref = Multicaixa_WooCommerce()->multicaixa_get_ref( $order_id );
			if ( is_array( $ref ) ) {
				// Mark as on-hold
				if ( apply_filters( 'multicaixa_proxypay_set_on_hold', true, $order_id ) ) $order->update_status( 'on-hold', __( 'Awaiting Multicaixa payment.', 'woo-multicaixa' ) );
				// Run action
				do_action( 'multicaixa_proxypay_process_payment', $order_id ); //To allow pro plugin do his stuff here, like reducing stock for example
				// Remove cart
				WC()->cart->empty_cart();
				// Empty awaiting payment session
				if ( isset( $_SESSION['order_awaiting_payment'] ) ) unset($_SESSION['order_awaiting_payment'] );
				// Return thankyou redirect
				return array(
					'result' => 'success',
					'redirect' => $this->get_return_url( $order )
				);
			} else {
				wc_add_notice( (string) $ref , 'error' );
			}
		}

		/**
		 * Just for Kz
		 */
		function disable_if_currency_not_kwanza( $available_gateways ) {
			return Multicaixa_WooCommerce()->disable_if_currency_not_kwanza( $available_gateways, $this->id );
		}

		/* Debug / Log - MOVED TO Multicaixa_WooCommerce with gateway id as first argument */
		public function debug_log( $message, $level = 'debug', $to_email = false, $email_message = '' ) {
			if ( $this->debug ) {
				Multicaixa_WooCommerce()->debug_log(
					$this->id,
					$message,
					$level,
					( trim( $this->debug_email ) != '' && $to_email ? $this->debug_email : false ),
					$email_message
				);
			}
		}

		/* For PRO */
		public function generate_multicaixa_validity_html( $value ) {
			if ( function_exists( 'Multicaixa_WooCommerce_Pro' ) )
				return Multicaixa_WooCommerce_Pro()->generate_multicaixa_validity_html( $value );
			return null;
		}
		public function generate_multicaixa_webhook_html( $value ) {
			if ( function_exists( 'Multicaixa_WooCommerce_Pro' ) )
				return Multicaixa_WooCommerce_Pro()->generate_multicaixa_webhook_html( $value );
			return null;
		}

		/* Process PRO fields */
		public function process_admin_options_pro() {
			if ( function_exists( 'Multicaixa_WooCommerce_Pro' ) ) Multicaixa_WooCommerce_Pro()->process_admin_options_pro();
		}

	}
}