<?php
/**
 * Plugin Name: Payment Multicaixa (ProxyPay gateway) for WooCommerce
 * Plugin URI: https://multicaixa-woocommerce.com
 * Description: This plugin allows customers with an Angolan bank account to pay WooCommerce orders in Kwanzas using Multicaixa (Pagamentos por Referência), through ProxyPay’s payment gateway.
 * Version: 1.0.9
 * Author: Webdados
 * Author URI: https://www.webdados.pt
 * Text Domain: woo-multicaixa
 * Domain Path: /lang
 * Requires at least: 4.9
 * Requires PHP: 5.6
 * WC requires at least: 3.0.1
 * WC tested up to: 4.1.0
**/

/* WooCommerce CRUD ready */

if ( ! defined( 'ABSPATH' ) ) exit; // Exit if accessed directly

/* Localization */
add_action( 'plugins_loaded', 'multicaixa_load_textdomain', 0 );
function multicaixa_load_textdomain() {
	load_plugin_textdomain( 'woo-multicaixa' );
}

/* Check if WooCommerce is active - Get active network plugins - "Stolen" from Novalnet Payment Gateway */
function multicaixa_active_nw_plugins() {
	if ( !is_multisite() )
		return false;
	$multicaixa_activePlugins = ( get_site_option('active_sitewide_plugins' ) ) ? array_keys( get_site_option('active_sitewide_plugins' ) ) : array();
	return $multicaixa_activePlugins;
}
if ( in_array( 'woocommerce/woocommerce.php', (array) get_option( 'active_plugins' ) ) || in_array( 'woocommerce/woocommerce.php', (array) multicaixa_active_nw_plugins() ) ) {


	/* Our own order class and the main classes */
	add_action( 'plugins_loaded', 'multicaixa_init', 1 );
	function multicaixa_init() {
		if ( class_exists( 'WooCommerce' ) && version_compare( WC_VERSION, '3.0.1', '>=' ) ) { //We check again because WooCommerce could have "died"
			require_once( dirname( __FILE__ ) . '/includes/class-multicaixa-woocommerce.php' );
			require_once( dirname( __FILE__ ) . '/includes/class-wc-multicaixa-woocommerce.php' );
			$GLOBALS['Multicaixa_WooCommerce'] = Multicaixa_WooCommerce();
			/* Add settings links - This is here because inside the main class we cannot call the correct plugin_basename( __FILE__ ) */
			add_filter( 'plugin_action_links_'.plugin_basename( __FILE__ ), array( Multicaixa_WooCommerce(), 'add_settings_link' ) );
		} else {
			add_action( 'admin_notices', 'admin_notices_multicaixa_woocommerce_not_active' );
		}
	}

	/* Main class */
	function Multicaixa_WooCommerce() {
		return Multicaixa_WooCommerce::instance(); 
	}

	/* API class */
	function Multicaixa_ProxyPay_API() {
		if ( ! isset( $GLOBALS['Multicaixa_ProxyPay_API'] ) ) {
			require_once( dirname( __FILE__ ) . '/includes/class-multicaixa-proxypay-api.php' );
			$GLOBALS['Multicaixa_ProxyPay_API'] = Multicaixa_ProxyPay_API::instance();
			$GLOBALS['Multicaixa_ProxyPay_API']->url = Multicaixa_WooCommerce()->url;
			$GLOBALS['Multicaixa_ProxyPay_API']->api_key = Multicaixa_WooCommerce()->get_multicaixa_setting('api_key');
		}
		return Multicaixa_ProxyPay_API::instance(); 
	}

	
} else {


	add_action( 'admin_notices', 'admin_notices_multicaixa_woocommerce_not_active' );


}


function admin_notices_multicaixa_woocommerce_not_active() {
	?>
	<div class="notice notice-error is-dismissible">
		<p><?php _e( '<strong>Payment Multicaixa (ProxyPay gateway) for WooCommerce</strong> is installed and active but <strong>WooCommerce (3.0.1 or above)</strong> is not.', 'woo-multicaixa' ); ?></p>
	</div>
	<?php
}

/* If you're reading this you must know what you're doing ;-) Greetings from sunny Portugal! */

