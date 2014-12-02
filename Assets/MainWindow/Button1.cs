using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;

public class Button1 : MonoBehaviour {
	public main CurrentMenu;

	public void ClickTest(main current){
		InputField[] _user = GetComponentsInChildren<InputField>();
		string name = _user [0].text;
		string password = _user [1].text;
		string URL = "http://192.168.1.79/www8/UdlapParking/login.php";

		Debug.Log(_user[0].text);
		Debug.Log(_user[1].text);

		StartCoroutine(HandleLogin(name, password,URL,current));


	}

	IEnumerator HandleLogin(string name, string password, string URL,main m){
		name = "'"+ name +"'";
		password = "'"+ password +"'";
		string loginURL = URL + "?username=" + name + "&password=" + password;
		WWW loginReader = new WWW (loginURL);
		yield return loginReader;
		Debug.Log(loginReader.text);
		
		JsonData jsonExercise = JsonMapper.ToObject(loginReader.text); // convert json data to object. 
		
		var a = jsonExercise["success"].ToString();
		if (loginReader.error != null) {
			a = "Error al conectar";
			Debug.Log(a);
		} 
		else {
			if(jsonExercise["success"].ToString() == "1"){
				a = "Exito";
				if (CurrentMenu!= null) {
					CurrentMenu.IsOpen = false;
				}
				CurrentMenu = m;
				CurrentMenu.IsOpen = true;
				Debug.Log(a);
				
			}		
			else{
				a = "Usuario invalido";
				Debug.Log(a);
				
			}
		}
	
	}

		
}
