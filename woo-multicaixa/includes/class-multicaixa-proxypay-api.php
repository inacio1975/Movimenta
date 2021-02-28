<?php

if ( ! defined( 'ABSPATH' ) ) {
	exit;
}

final class Multicaixa_ProxyPay_API {

	/* Internal variables */
	public $url     = '';
	public $api_key = '';

	/* Single instance */
	protected static $_instance = null;

	/* Constructor */
	public function __construct() {
	}

	/* Ensures only one instance of this class is loaded or can be loaded */
	public static function instance() {
		if ( is_null( self::$_instance ) ) {
			self::$_instance = new self();
		}
		return self::$_instance;
	}

	/* Create the request arguments */
	function create_request_arguments( $method, $data = NULL ) {
		$data = json_encode( $data );
		return array(
			'method'      => $method,
			'httpversion' => '1.1',
			'timeout'     => 15,
			'headers'     => $this->create_headers( $data ),
			'body'        => $data,
		);
	}

	/* Create the headers */
	function create_headers( $data ) {
		return array(
			'Authorization'  => 'Token '.$this->api_key,
			'Accept'         => 'application/vnd.proxypay.v2+json',
			'Content-Type'   => 'application/json',
			'Content-Length' => strlen($data),
		);
	}

	/* Request */
	function request( $action, $args, $expected_http_code ) {
		Multicaixa_WooCommerce()->debug_log(
			Multicaixa_WooCommerce()->multicaixa_id,
			'ProxyPay API Request starting on URL: '.$this->url.'/'.$action.' / Args: '.serialize( $args ),
			'debug'
		);
		$response = wp_remote_request( $this->url.'/'.$action, $args );
		if ( is_wp_error( $response ) ) {
			return array(
				'success'   => false,
				'http_code' => null,
				'response'  => $response,
				'error'     => $response->get_error_messages(),
				'args'      => serialize( $args ),
			);
		} else {
			$http_code = wp_remote_retrieve_response_code( $response );
			if ( $http_code == $expected_http_code ) {
				return array(
					'success'   => true,
					'http_code' => $http_code,
					'response'  => $response,
					'error'     => null,
					'args'      => serialize( $args ),
				);
			} else {
				return array(
					'success'   => false,
					'http_code' => $http_code,
					'response'  => $response,
					'error'     => null,
					'args'      => serialize( $args ),
				);
			}
		}
	}

	/* PUT */
	function put( $action, $data ) {
		$args = $this->create_request_arguments( 'PUT', $data );
		return $this->request( $action, $args, 204 );
	}

	/* POST */
	function post( $action, $data ) {
		$args = $this->create_request_arguments( 'POST', $data );
		return $this->request( $action, $args, 200 );
	}

}