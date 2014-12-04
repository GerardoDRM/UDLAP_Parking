using UnityEngine;
using System.Collections;

/*
 * Accion del buton para
 * regresar a la pantalla precendete
 * 
 */
public class ButtonReturn : MonoBehaviour {
	public main CurrentMenu;
	/* Accion del buton al obtener el evento click
	 * @args main Class main
	 */
	public void ClickReturn (main m) {
		//Ocultamos el menu actual
		if (CurrentMenu!= null) {
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = m;
		CurrentMenu.IsOpen = true;
	}// ClickReturn
}//ButtonReturn

