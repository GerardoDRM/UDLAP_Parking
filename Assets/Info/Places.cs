using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*Esta clase permite aumentar o disminur 
 * la capacidad de un estacionamiento
 */
public class Places : MonoBehaviour {
	public GameObject plus;
	public GameObject minus;
	public GameObject cap;

	//Metodo para aumentar los espacios libres
	public void Plus(GameObject text){
		Text t = text.GetComponent<Text> ();
		Text c = cap.GetComponent<Text> ();
		int available = int.Parse (c.text);
		int places = int.Parse(t.text);
		if (places < available) {
			places = places + 1;
			t.text = places.ToString ();
		}
	}//Plus
	//Metodo para disminuir espacios libres
	public void Minus(GameObject text){
		Text t = text.GetComponent<Text> ();
		int places = int.Parse(t.text);
		if (places > 0) {
			places = places - 1;
			t.text = places.ToString ();
		}
	}//Minus
}//Places
