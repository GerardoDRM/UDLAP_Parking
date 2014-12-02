<?php

/*
 * Following code will get the user and the password
 */

// array for JSON response
$response = array();


// include db connect class
require("db_connect.php");

// connecting to db
$db = new DB_CONNECT();

// check for post data
if (isset($_GET["user"]) && isset($_GET["password"])) {
    $userName = $_GET['user'];
	$password = $_GET['password'];
	
    // get a user from employe table
    $result = mysql_query("SELECT idEmpleado,Nombre, Password FROM empleado WHERE empleado.idEmpleado = $userName and empleado.Password=$password");

    if (!empty($result)) {
        // check for empty result
        if (mysql_num_rows($result) > 0) {

            $result = mysql_fetch_array($result);

            $employe = array();
            $employe["id"] = $result["idEmpleado"];
            $employe["userName"] = $result["Nombre"];
            $employe["password"] = $result["Password"];
           
            // success
            $response["success"] = 1;

            // user node
            $response["employe"] = array();

            array_push($response["employe"], $employe);

            // echoing JSON response
            echo json_encode($response);
        } else {
            // no product found
            $response["success"] = 0;
            $response["message"] = "Not employe found";

            // echo no users JSON
            echo json_encode($response);
        }
    } else {
        // no product found
        $response["success"] = 0;
        $response["message"] = "Not employe found";

        // echo no users JSON
        echo json_encode($response);
    }
} else {
    // required field is missing
    $response["success"] = 0;
    $response["message"] = "Required field(s) is missing";

    // echoing JSON response
    echo json_encode($response);
}
?>