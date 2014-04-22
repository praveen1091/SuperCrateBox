using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	static float KeyboardDirection () {
		if (Input.GetKey (KeyCode.RightArrow))
			return 1f;
		else if (Input.GetKey (KeyCode.LeftArrow))
			return -1f;
		else
			return 0f;
	}

	static bool KeyboardJump () {
		if (Input.GetKey (KeyCode.Space))
			return true;
		return false;
	}

	public static float GetInputDirrection () {
		return KeyboardDirection ();
	}

	public static bool GetInputJump () {
		return KeyboardJump ();
	}
}
