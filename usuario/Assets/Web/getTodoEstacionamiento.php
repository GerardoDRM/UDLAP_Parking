<?php

/*
 * Codigo para la obtencion de
 * todos los estacionamientos
 */

// JSON array
$response = array();

// Clas de la conexion a la BD
require("db_connect.php");

// Conectando a la BD
$db = new DB_CONNECT();

// Obtenemos todos los estacionamientos
$result = mysql_query("SELECT * FROM estacionamiento") or die(mysql_error());

//  Checa resultados vacios
if (mysql_num_rows($result) > 0) {
    // ciclo sobre los resultados
    // nodo estacionamiento
    $response["estacionamiento"] = array();

    while ($row = mysql_fetch_array($result)) {
        // array temporal
        $lugar = array();
        $lugar["id"] = $row["idEstacionamiento"];
        $lugar["nombre"] = $row["nombre"];
        $lugar["tipo"] = $row["tipo"];
        $lugar["capacidad"] = $row["capacidad"];
        $lugar["cupo"] = $row["cupo"];
        $lugar["imagen"] = $row["imagen"];

        // agrega valor al arreglo
        array_push($response["estacionamiento"], $lugar);
    }
    // Exito
    $response["success"] = 1;

    // echoing JSON
    echo json_encode($response);
} else {
    // nada encontrado
    $response["success"] = 0;
    $response["message"] = "No se encontro nada";

    // echo JSON
    echo json_encode($response);
}
?>
