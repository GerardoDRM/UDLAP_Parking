using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 * Accion del buton para
 * regresar a la pantalla precendete
 * 
 */
public class ButtonReturn : MonoBehaviour {
	public main CurrentMenu;
	public main lastMenu;
	/* Accion del buton al obtener el evento click
	 * @args main Class main
	 */
	public void ClickReturn () {
		//Ocultamos el menu actual
		if (CurrentMenu.IsOpen == true) {
			CurrentMenu.IsOpen = false;
			lastMenu.IsOpen = true;
		}

	}// ClickReturn

	public void ClickReturnClean(GameObject t){
		Text texto = t.GetComponent<Text>();
		texto.text = "";
	}
}//ButtonReturn

