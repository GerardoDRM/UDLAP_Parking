using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount, columnCount = 1;
	string URL_INFO = "http://localhost/www8/UdlapParking/getTodoEstacionamiento.php";
	string [] id,nombre,tipo,capacidad,cupo,imagen;

    public void Start()
    {
		StartCoroutine (HandleInfo ());

    }
	
	IEnumerator HandleInfo ()
	{
		// Start a download of the given URL
		var estacionamientos = new WWW (URL_INFO);
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
				id = new string[itemCount];
				nombre = new string[itemCount];
				tipo = new string[itemCount];
				capacidad = new string[itemCount];
				cupo = new string[itemCount];
				imagen = new string[itemCount];
				for (int i=0; i<itemCount; i++) {
					id [i]= jsonExercise ["estacionamiento"][i]["id"].ToString();	
					nombre [i]= jsonExercise ["estacionamiento"][i]["nombre"].ToString();				
					tipo [i]= jsonExercise ["estacionamiento"][i]["tipo"].ToString();				
					capacidad [i]= jsonExercise ["estacionamiento"][i]["capacidad"].ToString();				
					cupo [i] = jsonExercise ["estacionamiento"][i]["cupo"].ToString();				
					imagen [i]= jsonExercise ["estacionamiento"][i]["imagen"].ToString();
				}
				create();
			} else {
				a = "No hay conexion";
				Debug.Log (a);
			}
		}

	}

	void create(){

		RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		float width = containerRectTransform.rect.width / columnCount;
		float ratio = width / rowRectTransform.rect.width;
		float height = rowRectTransform.rect.height * ratio;
		int rowCount = itemCount / columnCount;
		if (itemCount % rowCount > 0)
			rowCount++;
		
		//adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
		
		int j = 0;
		for (int i = 0; i < itemCount; i++)
		{
			//this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
			if (i % columnCount == 0)
				j++;

			HandleInfo(itemPrefab,id[i],nombre[i],tipo[i],capacidad[i],cupo[i]);

			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = gameObject.name + " item at (" + i + "," + j + ")";
			newItem.transform.parent = gameObject.transform;
			
			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();
			
			float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
			float y = containerRectTransform.rect.height / 2 - height * j;
			rectTransform.offsetMin = new Vector2(x, y);
			
			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x, y);
			StartCoroutine(HandleImage(newItem,imagen[i]));

		}

	}

	void HandleInfo(GameObject b, string i, string n,string t,string cap, string cup){
		Text[] estacionamiento = b.GetComponentsInChildren<Text> ();
		estacionamiento [0].text = n;
		estacionamiento [1].text = i;
		estacionamiento [2].text = t;
		//Calcular el porcentaje
		int ca = int.Parse (cap);
		int cu = int.Parse (cup);
		int disponibles = ca - cu;
		float porcentaje = (disponibles * 100) / ca;
		estacionamiento [3].text = porcentaje.ToString ();



	}
	IEnumerator HandleImage (GameObject b, string img)
	{

		Text[] estacionamiento = b.GetComponentsInChildren<Text> ();
		int val = int.Parse(estacionamiento [3].text);
		Image[] i = b.GetComponentsInChildren<Image> ();
		if(val > 70 && val<=100)
			i[1].color = new Color(0,  0.6F, 0, 1);
		if(val > 40 && val<=69)
			i[1].color = new Color(0.8F, 0.8F, 0, 1);
		if(val >= 0 && val<=39)
			i[1].color = new Color(0.8F, 0, 0.1F, 1);
		// Start a download of the given URL
		var www = new WWW(img);
		// wait until the download is done
		yield return www;
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		Sprite sprite = new Sprite();
		sprite = Sprite.Create(www.texture, new Rect(0, 0, 500, 300),new Vector2(0, 0),100.0f);
		i[1].sprite = sprite;
	}

}
