using UnityEngine;
using System.Collections;

public class WeaponShoot : MonoBehaviour {

	public float Speed = 3f, fweapon;
	int weapon;
	float direction, d, yoffset, mcoffset = 4.4f, decayoffset = 120.0f, mcrecoil = 0.1f;
	Transform BulletSlot, Player;
	CharacterController PCC;

	void Shoot ( float scale, float yoffset, float duration, float damage ) {
		scale /= 5;
		GameObject x = Instantiate ( Resources.Load ("Weapons/Bullet"), BulletSlot.position, Quaternion.Euler ( 0, 0, 0 ) ) as GameObject;
		Physics.IgnoreCollision (x.collider, Player.gameObject.collider);
		x.transform.localScale = new Vector3 ( scale, scale, scale );
		x.GetComponent<BulletBehaviour>().Shoot ( new Vector3 ( direction * Speed, yoffset, 0), duration, damage );
	}

	void MachineGunShoot () {
		yoffset -= d * decayoffset * Time.deltaTime;
		if ( yoffset < -mcoffset || yoffset > mcoffset ) {
			yoffset = d * -1 * mcoffset;
			d *= -1f;
		}

		Shoot ( 1, yoffset, 0, 1 );
		PCC.Move ( new Vector3 ( mcrecoil * direction * -1, 0, 0 ) );
	}

	void MachineGunPrepare () {
		if (Input.GetKeyDown (KeyCode.X)) {
			d = -1f; yoffset = mcoffset;
			InvokeRepeating ( "MachineGunShoot", 0.2f, 0.05f );
		} else if (Input.GetKeyUp (KeyCode.X)) {
			CancelInvoke ( "MachineGunShoot" );
		}
	}

	void RevolverShoot () {
		if (Input.GetKeyDown (KeyCode.X)) {
			Shoot ( 2, 0, 0, 6 );
		}
	}

	void PistolShoot () {
		if (Input.GetKeyDown (KeyCode.X)) {
			Shoot ( 1, 0, 0, 1 );
		}
	}
	
	// Update is called once per frame
	void ShotgunShoot () {
		if (Input.GetKeyDown (KeyCode.X)) {
			Shoot ( 0.5f, 0.1f, 1, 3 );
			Shoot ( 0.5f, -0.1f, 1, 3 );
			Shoot ( 0.3f, 0.3f, 1, 3 );
			Shoot ( 0.3f, -0.3f, 1, 3 );
		}
	}

	void Start () {
		BulletSlot = transform.Find ("BulletSlot");
		Player = GameObject.Find ("player").transform;
		PCC = Player.GetComponent<CharacterController> ();
	}

	void Update () {
		if (BulletSlot.position.x < Player.position.x)
			direction = -1f;
		else
			direction = 1f;

		weapon = (int)fweapon;
		switch (weapon) {
		case 1:
			PistolShoot ();
			break;
		case 2:
			RevolverShoot ();
			break;
		case 3:
			MachineGunPrepare ();
			break;
		case 4:
			ShotgunShoot ();
			break;
		}
	}
}
