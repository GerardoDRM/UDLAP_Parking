using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Esta clase permite guardar el nuevo valor
 *del cupo de un estacionamiento
 */
public class StorePlaces : MonoBehaviour {
	public GameObject nom;
	public GameObject lugares;
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
	}//HandleSend

}//StorePlaces
