=== Payment Multicaixa (ProxyPay gateway) for WooCommerce ===
Contributors: webdados
Tags: woocommerce, payment, pagamentos, gateway, multicaixa, angola, multibanco, atm, debit card, credit card, bank, ecommerce, e-commerce, emis, proxypay, webdados, php7, cartão de débito, cartão de crédito, cartões, referência, reference
Author URI: https://www.webdados.pt
Plugin URI: https://multicaixa-woocommerce.com
Requires at least: 4.9
Tested up to: 5.5
Stable tag: 1.0.9

This plugin allows customers with an Angolan bank account to pay WooCommerce orders in Kwanzas using Multicaixa (Pagamentos por Referência), through ProxyPay’s payment gateway.

== Description ==

“Pagamento por Referência” (payment by reference) on Multicaixa (Angolan ATM network), or Home Banking services, is one of the most popular and secure ways to pay for services and (online) purchases in Angola.

This plugin will allow you to generate a payment Reference the customer can then use to pay for his WooCommerce order in Kwanzas, through an ATM or Home Banking service.

This plugin works with the [ProxyPay](https://proxypay.co.ao/) gateway, and a contract with an Angolan bank and [TimeBoxed](http://timeboxed.co.ao/) (the company behind ProxyPay) is required. Technical support is provided by [Webdados](https://www.webdados.pt).

= Features: =

* Generates a Multicaixa Reference for simple payment on the Angolan ATM network or Home Banking service;
* 24 hours reference validity;
* WPML tested and compatible (for multilingual shops);
* Polylang tested;
* PHP7 tested and compatible;

= PRO add-on features: =

* Automatically changes the order status to “Processing” (or “Completed” if the order only contains virtual downloadable products) and notifies both the customer and the store owner, if the Webhook upon payment is activated;
* Automatic Webhook call can be activated upon request to ProxyPay, via the plugin settings screen;
* Configuration of reference validity in hours, days, weeks or months;
* Shop owner can set minimum and maximum order totals for this payment gateway to be available;
* Payment method can be deactivated if the client doesn’t have an Angolan address;
* Ability to reduce stock when the order is created or paid;
* Automatically update the payment reference, and notify the client, if the order total is changed on a backend order modification;
* Allows searching orders (in the admin area) by Multicaixa Reference;
* Automatic cancellation of orders with expired references;
* Integration with ProxyPay’s own SMS system;
* ProxyPay Sandbox support;
* Other planned PRO features:
	* Integration for 3rd party SMS notification plugins:
		* WooCommerce - APG SMS Notifications;
		* Twilio SMS Notifications;
		* YITH WooCommerce SMS Notification;
		* Other providers can be added upon request (under quotation);
	* WooCommerce Subscriptions integration;
	* WooCommerce Deposits integration;

[Know more](https://multicaixa-woocommerce.com) or [get the plugin PRO add-on now](https://shop.webdados.com/product/payment-multicaixa-proxypay-gateway-for-woocommerce-pro-add-on/).

== Installation ==

* Use the included automatic install feature on your WordPress admin panel and search for “Multicaixa WooCommerce”.
* Go to WooCoomerce > Settings > Payments > Pagamento por Referência no Multicaixa and fill in the details provided by ProxyPay and your bank in order to use this payment method. A contract with TimeBoxed (the company behind ProxyPay) and a bank is mandatory.
* Start receiving payments :-)

== Frequently Asked Questions ==

= Can I start receiving payments right away? Show me the money! =

Nop! You have to sign a contract with ProxyPay and an Angolan bank in order to activate this service. Go to [https://proxypay.co.ao/](https://proxypay.co.ao/) for more information. They can help you out with all the subscription process with them and the bank.

= [WPML] My website is multilingual. Will I be able to use this plugin? =

Yes. This plugin is WPML compatible. You will need the WPML and WPML String Translation Plugins (alongside WooCommerce Multilingual, which is mandatory for any WooCommerce + WPML install). 

= [WPML] How can I translate the payment method title and description the client sees in the checkout page to secondary languages? =

Go to WPML > String Translation > Search and translate the `multicaixa_proxypay_gateway_title` and `multicaixa_proxypay_gateway_description`, strings in the `woocommerce` domain. Don’t forget to check the “Translation is complete” checkbox and click “Save”. You should also translate the “Extra instructions” strings by searching the `multicaixa_proxypay_extra_instructions` string on the `woo_multicaixa`.

= [SMS] How to include the Multicaixa payment instructions in the SMS sent by “WooCommerce - APG SMS Notifications”, “Twilio SMS Notifications” or “YITH WooCommerce SMS Notification”? =

You’ll need the [plugin PRO add-on](https://multicaixa-woocommerce.com).

= [SMS] How to trigger the ProxyPay SMS system? =

You’ll need the [plugin PRO add-on](https://multicaixa-woocommerce.com).

= [Advanced] The order is set “On Hold”, can I make it “Pending” by default? =

I don’t know why on earth you would want to do this, but… yes, you can. Just return `false` to the `multicaixa_proxypay_set_on_hold` filter.
Be advised that no “new order” email, with payment instructions, will be sent to the client unless you use some plugin or custom code to force it.

= [Advanced] Can I prevent the plugin from adding the payment instructions and/or the payment received message to emails? =

You can use the `multicaixa_proxypay_email_instructions_pending_send` filter: return false and the payment instructions won’t be included in the “new order” email – we do not recommend you to do it, though.
You can use the `multicaixa_proxypay_email_instructions_payment_received_send` filter: return false and the payment received message won’t be included in the “processing” email.

= [Advanced] How can I use the plugin with the ProxyPay sandbox, for tests? =

You should return true to the `multicaixa_proxypay_test_mode` filter, or use the [plugin PRO add-on](https://multicaixa-woocommerce.com) to activate the test mode with a simple checkbox.

= I need technical support. Who should I contact, ProxyPay or Webdados? =

Development and (premium) support is provided by [Webdados](https://www.webdados.pt).
We do not provide free support for this free plugin, but you can submit bug reports here at WordPress.org
You'll get included support if you buy the [plugin PRO add-on](https://multicaixa-woocommerce.com).
For premium support or custom developments, you should contact [Webdados](https://www.webdados.pt/contactos/). Charges may (and most certainly will) apply.

= Can I contribute with a translation? =

Sure. Go to [GlotPress](https://translate.wordpress.org/projects/wp-plugins/woo-multicaixa) and help us out.

== Changelog ==

= 1.0.9 =
* Better debugging
* Tested with WordPress 5.5-alpha-47574 and WooCommerce 4.1.0-beta.2

= 1.0.8 =
* Small visual tweaks on the settings page
* Allow PRO add-on to add extra content to the settings page
* Fix SVG banner on the payment instructions
* Tested with WordPress 5.3.1-alpha-46798 and WooCommerce 3.9.0-beta.1

= 1.0.7 =
* Tested with WordPress 5.2.4-alpha-46074 and WooCommerce 3.8.0-beta.1

= 1.0.6 =
* Tested with WordPress 5.2.3-alpha-45552 and WooCommerce 3.7.0-beta.1
* WordPress 4.9 minimum requirement
* PHP 5.6 minimum requirement

= 1.0.5 =
* readme.txt tweaks
* Tested with WordPress 5.2.3-alpha and WooCommerce 3.6.5

= 1.0.4 =
* Fix minimum and maximum payment values
* Tested with WordPress 5.1.1 and WooCommerce 3.6.0 (RC 2)

= 1.0.3.1 =
* Fix version number

= 1.0.3 =
* Fix SVG icon
* Tested with WordPress 5.1 and WooCommerce 3.5.6

= 1.0.2 =
* Tested with WordPress 5.0.2 and WooCommerce 3.5.2

= 1.0.1 =
* readme.txt improvements
* Small fixes

= 1.0 =
* Initial release.