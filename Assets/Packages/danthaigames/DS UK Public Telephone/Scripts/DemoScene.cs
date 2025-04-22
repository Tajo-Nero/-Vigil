using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScene : MonoBehaviour {

	public KeyCode toggleDoorKey = KeyCode.E;
	public PhoneBox_Ctrl phoneBox;

	void Update () {

		if (Input.GetKeyUp (toggleDoorKey)) {
			phoneBox.ToggleDoor ();
		}

	}
}
