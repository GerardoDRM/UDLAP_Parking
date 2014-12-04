<?php

/*
 * Codigo para la obtencion de
 * los datos del moderador
 */

// JSON array
$response = array();


// Class de la conexion a la BD
require("db_connect.php");

// Conectando a la BD
$db = new DB_CONNECT();

// Checamos los datos recibidos
if (isset($_GET["username"]) && isset($_GET["password"])) {
    $userName = $_GET['username'];
	  $password = $_GET['password'];

    // Obtener los datos del usuario
    $result = mysql_query("SELECT nombre, password FROM moderador WHERE moderador.nombre = $userName and moderador.password= $password");

    if (!empty($result)) {
        // Checa si los resultados son vacios
        if (mysql_num_rows($result) > 0) {

            $result = mysql_fetch_array($result);

            $moderador = array();
            $moderador["userName"] = $result["nombre"];
            $moderador["password"] = $result["password"];

            // Exito
            $response["success"] = 1;

            // nodo usuario
            $response["moderador"] = array();

            array_push($response["moderador"], $moderador);

            // echoing JSON
            echo json_encode($response);
        } else {
            // No se encontro
            $response["success"] = 0;
            $response["message"] = "No existe";

            // echo JSON
            echo json_encode($response);
        }
    } else {
        // No se encontro
        $response["success"] = 0;
        $response["message"] = "No existe";

        // echo JSON
        echo json_encode($response);
    }
} else {
    // Campo requerido faltante
    $response["success"] = 0;
    $response["message"] = "Valores faltantes";

    // echoing JSON
    echo json_encode($response);
}
?>
