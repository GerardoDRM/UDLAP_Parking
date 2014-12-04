using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;
/*
 * Esta clase se encarga de verificar
 * si un moderador se encuentra registrado 
 * en la base de datos
 */
public class Button1 : MonoBehaviour {
	public main CurrentMenu;
	public main nextMenu;
	public InputField[] user;

	//Metodo de llamada para el boton al OnClick()
	public void ClickTest(){
		user = GetComponentsInChildren<InputField>();
		string name = user [0].text;
		string password = user [1].text;
		string URL = "http://localhost/www8/UdlapParking/login.php";
		
		//Proceso en background
		StartCoroutine(HandleLogin(name, password,URL));
	}//ClickTest

	// Este metodo realizara la verificacion del usuario 
	// a travez de un servicio web
	IEnumerator HandleLogin(string name, string password, string URL){
		name = "'"+ name +"'";
		password = "'"+ password +"'";
		//Se crea el URL
		string loginURL = URL + "?username=" + name + "&password=" + password;
		WWW loginReader = new WWW (loginURL);
		//Obtenermos la respuesta del servicio
		yield return loginReader;
		Debug.Log(loginReader.text);
		//Se crea un objeto JSON
		JsonData jsonExercise = JsonMapper.ToObject(loginReader.text); // convert json data to object. 
		//Verificamos la respuesta del objeto JSON
		var a = jsonExercise["success"].ToString();
		if (loginReader.error != null) {
			a = "Error al conectar";
			Debug.Log(a);
		}//if 
		else {
			if(jsonExercise["success"].ToString() == "1"){
				a = "Exito";
				//Si se realzia correctamente se pasa a la 
				//siguiente pantalla
				if (CurrentMenu == true) {
					CurrentMenu.IsOpen = false;
					nextMenu.IsOpen = true;
				}

				foreach(var p in user){
					p.text = "";
				}
			}//if	
			else{
				a = "Usuario invalido";
				Debug.Log(a);
				
			}//else
		}//else
	
	}// HnadleLogin		
}//Button1
