<?php

/**
 * Clase para la conexion a la
 * base de datos
 */
class DB_CONNECT {

    // constructor
    function __construct() {
        // Conexion a la base
        $this->connect();
    }

    // destructor
    function __destruct() {
        // cerrar la conexion
        $this->close();
    }

    /**
     * Funcion para conectar con la base de datos
     */
    function connect() {
        // importart las variables para la conexion
        require("db_config.php");

        // Conectando con MySql
        $con = mysql_connect(DB_SERVER, DB_USER, DB_PASSWORD) or die(mysql_error());

        // Seleccionando la BD
        $db = mysql_select_db(DB_DATABASE) or die(mysql_error()) or die(mysql_error());

        // regresa la conexion
        return $con;
    }

    /**
     * Funcion para cerrar la conexion
     */
    function close() {
        // Cerrando la conexion
        mysql_close();
    }

}

?>
