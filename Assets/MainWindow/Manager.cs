using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public main CurrentMenu;

	public void Start(){
		ShowMenu (CurrentMenu);
	}

	public void ShowMenu(main m){
		if (CurrentMenu != null) {
						CurrentMenu.IsOpen = false;
				}
		CurrentMenu = m;
		CurrentMenu.IsOpen = true;

	}

}
