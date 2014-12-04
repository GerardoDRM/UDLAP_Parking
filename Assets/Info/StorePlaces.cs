using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
/*Esta clase permite guardar el nuevo valor
 *del cupo de un estacionamiento
 */
public class StorePlaces : MonoBehaviour {
	public GameObject nom;
	public GameObject lugares;
	public GameObject texto;
	/*Metodo que se encarga de llamar a un Handle para
	 *modificar en la tabla estacionamiento
	 */ 
	public void ChangeNumPl (GameObject text) {
		Text t = text.GetComponent<Text> ();
		Text nombre = nom.GetComponent<Text> ();
		Text lug = lugares.GetComponent<Text> ();
		StartCoroutine (HandleSend (t.text, nombre.text, lug.text));
	}//ChangeNumPl

	//Conexcion y envio de datos a la base de datos
	IEnumerator HandleSend(string val,string nombre, string lug){
		int v = int.Parse (val);
		int l = int.Parse (lug); 
		int newVal = l - v;
		//Se crea un WWWForm para pasar informacion en el metodo POST
		WWWForm form = new WWWForm();
		Debug.Log (nombre +" "+newVal.ToString());
		form.AddField("nom",nombre);
		form.AddField ("cap",newVal.ToString());
		//Creamos el URL
		string URL = "http://localhost/www8/UdlapParking/cambioCupo.php";
		WWW info = new WWW (URL, form);
		yield return info;
		Debug.Log (info.text);
		JsonData jsonExercise = JsonMapper.ToObject (info.text); // convert json data to object. 
		Text tag = texto.GetComponent<Text> ();
		var a = jsonExercise ["success"].ToString ();
		if (info.error != null) {
			a = "Error al conectar";
			tag.text = a;
			Debug.Log (a);
		} else {
			if (jsonExercise ["success"].ToString () == "1") {
				tag.text = "Cambios guardados";
				
			} else {
				a = "No hay conexion";
				tag.text = a;
				Debug.Log (a);
			}
		}
	}//HandleSend

}//StorePlaces
