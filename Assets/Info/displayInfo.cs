using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class displayInfo : MonoBehaviour {

	public main CurrentMenu;
	public GameObject item;
	public GameObject itemName;

	string URL = "http://localhost/www8/UdlapParking/getInfo.php";

	// Use this for initialization
	public void ClickInfo (main m) {

		Text t = item.GetComponentInChildren<Text> ();
		StartCoroutine(HandleInfo(t,m));
		
	}
	
	IEnumerator HandleInfo(Text t,main m){

		string infoURL = URL + "?pid=" + t.text;
		WWW infoReader = new WWW (infoURL);
		yield return infoReader;
		Debug.Log(infoReader.text);
		
		JsonData jsonExercise = JsonMapper.ToObject(infoReader.text); // convert json data to object. 
		
		var a = jsonExercise["success"].ToString();
		if (infoReader.error != null) {
			a = "Error al conectar";
			Debug.Log(a);
		} 
		else {
			if(jsonExercise["success"].ToString() == "1"){
				Text[] tt = itemName.GetComponentsInChildren<Text>();
				tt[0].text= t.text;
				int cap = int.Parse(jsonExercise["estacionamiento"][0]["cap"].ToString());
				int cup = int.Parse(jsonExercise["estacionamiento"][0]["cupo"].ToString());
				int c = cap-cup;
				tt[3].text = c+"";
				if (CurrentMenu!= null) {
					CurrentMenu.IsOpen = false;
				}
				CurrentMenu = m;
				CurrentMenu.IsOpen = true;

			}		
			else{
				a = "Usuario invalido";
				Debug.Log(a);
				
			}
		}
		
	}

}
