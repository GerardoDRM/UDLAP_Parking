<?php

/*
 * Codigo para obtener los detalles
 * de un estacionamiento seleccionado
 */

// JSON array
$response = array();


// Class de la conexion a la BD
require("db_connect.php");

// Conectando a la BD
$db = new DB_CONNECT();
$val=true;
// Checa la informacion obtenida
if (isset($_GET["pid"])){
    $pid = $_GET["pid"];

    // Se obtiene los detalles del estacionamiento
    $result = mysql_query("SELECT nombre,capacidad,cupo FROM estacionamiento as e WHERE e.nombre = '$pid'");
    if (!empty($result)) {
        // Checa resultados vacios
        if (mysql_num_rows($result) > 0) {

            $result = mysql_fetch_array($result);

            $product = array();
            $product["nom"] = $result["nombre"];
            $product["cap"] = $result["capacidad"];
            $product["cupo"] = $result["cupo"];


            // Exito
            $response["success"] = 1;

            // nodo estacionamiento
            $response["estacionamiento"] = array();

            array_push($response["estacionamiento"], $product);

            // echoing JSON
            echo json_encode($response);
        } else {
            // Nada encontrado
            $response["success"] = 0;
            $response["message"] = "Not product found";

            // echo JSON
            echo json_encode($response);
        }
    } else {
        // Nada encontrado
        $response["success"] = 0;
        $response["message"] = "Not product found";

        // echo JSON
        echo json_encode($response);
    }
} else {
    // Campos requeridos faltantes
    $response["success"] = 0;
    $response["message"] = "Required field(s) is missing";

    // echoing JSON
    echo json_encode($response);
}
?>
