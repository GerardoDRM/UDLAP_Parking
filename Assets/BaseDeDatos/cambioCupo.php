<?php
/*
 * Codigo para cambiar los datos
 * de cupo de un estacionamiento
 */

// JSON array
$response = array();


// Checando datos POST
if (isset($_POST['nom']) && isset($_POST['cap'])) {

    $nombre = $_POST['nom'];
    $capacidad = $_POST['cap'];

    // Class de la conexion a la BD
    require("db_connect.php");

    // Conectando a la BD
    $db = new DB_CONNECT();

    // Realizando un UPDATE
    $result = mysql_query("UPDATE estacionamiento SET estacionamiento.cupo=$capacidad WHERE estacionamiento.nombre='$nombre'");

    // Checar si se realizo el cambio
    if ($result) {
        // Exito
        $response["success"] = 1;
        $response["message"] = "Cambio.";

        // echoing JSON
        echo json_encode($response);
    } else {
        // falla
        $response["success"] = 0;
        $response["message"] = "error.";

        // echoing JSON response
        echo json_encode($response);
    }
} else {
    // Campos requeridos faltantes
    $response["success"] = 0;
    $response["message"] = "Campos faltantes";

    // echoing JSON
    echo json_encode($response);
}
?>
