using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;
using System;

public class createButton : MonoBehaviour
{
	public GameObject itemName;
	public main CurrentMenu;
	public main NextMenu;
	string URL_INFO = "http://localhost/www8/UdlapParking/getInfo.php";

	public GameObject itemPrefab;
		//string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
		// Use this for initialization
		string[] nombres;
		int itemCount;
		int columnCount = 1;

		void Start ()
		{
				StartCoroutine (HandleInfo ());

		
		}

		IEnumerator HandleInfo ()
		{
				string url = "http://localhost/www8/UdlapParking/info_estacionamiento.php";
				// Start a download of the given URL
				var estacionamientos = new WWW (url);
				// wait until the download is done
				yield return estacionamientos;
				Debug.Log (estacionamientos.text);

				JsonData jsonExercise = JsonMapper.ToObject (estacionamientos.text); // convert json data to object. 
		
				var a = jsonExercise ["success"].ToString ();
				if (estacionamientos.error != null) {
						a = "Error al conectar";
						Debug.Log (a);
				} else {
						if (jsonExercise ["success"].ToString () == "1") {
								itemCount = jsonExercise ["estacionamiento"].Count;
								nombres = new string[itemCount];
								for (int i=0; i<itemCount; i++) {
										nombres [i] = (string)jsonExercise ["estacionamiento"] [i];
								}
	
						} else {
								a = "No hay conexion";
								Debug.Log (a);
						}
				}

				create ();
		}

		public void create ()
		{
				RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform> ();
				RectTransform container = gameObject.GetComponent<RectTransform> ();
		
				//calculate the width and height of each child item.
				float width = container.rect.width / columnCount;
				float ratio = width / rowRectTransform.rect.width;
				float height = rowRectTransform.rect.height * ratio;
				int rowCount = itemCount / columnCount;
				if (itemCount % rowCount > 0)
						rowCount++;
		
				//adjust the height of the container so that it will just barely fit all its children
				float scrollHeight = height * rowCount;
				container.offsetMin = new Vector2 (container.offsetMin.x, -scrollHeight / 2);
				container.offsetMax = new Vector2 (container.offsetMax.x, scrollHeight / 2);
				int j = 0;
				for (int i=0; i<itemCount; i++) {
						//this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
						if (i % columnCount == 0)
								j++;

						HandleImage(itemPrefab,nombres[i]);

						//create a new item, name it, and set the parent
						GameObject newItem = Instantiate (itemPrefab) as GameObject;
						newItem.name = gameObject.name + " item at (" + i + "," + j + ")";
						newItem.transform.parent = gameObject.transform;
			
						//move and size the new item
						RectTransform rectTransform = newItem.GetComponent<RectTransform> ();
			
						float x = -container.rect.width / 2 + width * (i % columnCount);
						float y = container.rect.height / 2 - height * j;
						rectTransform.offsetMin = new Vector2 (x, y);
			
						x = rectTransform.offsetMin.x + width;
						y = rectTransform.offsetMin.y + height;
						rectTransform.offsetMax = new Vector2 (x, y);
						Button button = newItem.GetComponent<Button>();

						//Add your new event
			AddListener(button, nombres[i]); // Using the iterator as argument will capture the variable
			

				}
		}

	
	public void HandleImage (GameObject b, string nombre)
		{
				Text t = b.GetComponentInChildren<Text> ();
				t.text = nombre;
				t.fontSize = 30;
				t.fontStyle = FontStyle.Bold;
				t.color = Color.black;
				
				/*Image i = b.GetComponentInChildren<Image> ();

		// Start a download of the given URL
		var www = new WWW(url);
		// wait until the download is done
		yield return www;
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		Sprite sprite = new Sprite();
		sprite = Sprite.Create(www.texture, new Rect(0, 0, 170, 170),new Vector2(0, 0),100.0f);
		i.sprite = sprite;*/
	
		}

	IEnumerator HandleInfo(String nom){
		
		string infoURL = URL_INFO + "?pid=" + nom;
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
				tt[0].text= nom;
				int cap = int.Parse(jsonExercise["estacionamiento"][0]["cap"].ToString());
				int cup = int.Parse(jsonExercise["estacionamiento"][0]["cupo"].ToString());
				int c = cap-cup;
				tt[3].text = c+"";		
				tt[4].text = cap+"";
			}		
			else{
				a = "Usuario invalido";
				Debug.Log(a);
				
			}
		}
		
	}
	// Outside of method running the above
	void AddListener(Button b, string value)
	{
		b.onClick.AddListener(() => checkInfo(value));
	}
	void checkInfo(string val){
		StartCoroutine(HandleInfo(val));
		if (CurrentMenu == true) {
			CurrentMenu.IsOpen = false;
			NextMenu.IsOpen = true;
		}

		Debug.Log (val);
	}
}