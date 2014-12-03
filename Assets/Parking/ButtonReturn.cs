using UnityEngine;
using System.Collections;

public class ButtonReturn : MonoBehaviour {
	public main CurrentMenu;
	// Use this for initialization
	public void ClickReturn (main m) {

		if (CurrentMenu!= null) {
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = m;
		CurrentMenu.IsOpen = true;
	}

}
