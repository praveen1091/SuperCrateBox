using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour {
	
	public float MoveSpeed = 3f, JumpForce = 12f, gravity = 10f, DecaySpeed;

	Transform DSlot, OSlot, D1Slot, D2Slot;
	GameObject DWeapon, OWeapon, D1Weapon, D2Weapon;

	CharacterController CC;
	Animator TreeAnimation;
	public static float direction = 0f, recoil = 0f;
	public static Vector3 velocity = new Vector3 (0, 0, 0);
	bool moving = false, disabled = false;

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (  hit.collider.tag == "Monster" )
			Death ();
		else
			velocity.y = -gravity;
	}

	// Use this for initialization
	void Start () {
		TreeAnimation = GetComponent<Animator> ();
		TreeAnimation.SetInteger ("HandedWeapon", 2);
		CC = GetComponent<CharacterController> ();
		DSlot = transform.Find ("DoubleSlot");
		DWeapon = Instantiate (Resources.Load ("Weapons/MachineGun"), DSlot.position, Quaternion.Euler (270, 90, 0)) as GameObject;
		DWeapon.transform.rotation = Quaternion.Euler (270, 270, 0);
		DWeapon.transform.parent = DSlot;

	}

	void Movement () {
		velocity.x = direction * MoveSpeed;
	}

	void Death () {
		transform.position += new Vector3 (0, 0, 5f);
		disabled = true;
		TreeAnimation.SetInteger ( "HandedWeapon", 0 );
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (CC.isGrounded && InputController.GetInputJump ())
			velocity.y = JumpForce;


		if (disabled) direction = 0f;
		else direction = InputController.GetInputDirrection ();

		if (direction != 0) {
			if ( direction > 0 && transform.rotation.y != 90 )
				transform.rotation = Quaternion.Euler ( 0, 90f, 0 );
			else if (direction < 0 && transform.rotation.y != 270)
				transform.rotation = Quaternion.Euler ( 0, 270f, 0 );


			if ( moving == false ) {
				TreeAnimation.SetBool ( "IsMoving", true );
				InvokeRepeating ( "Movement", 0f, 0.25f );
				moving = true;
			}
		}
		else {
			TreeAnimation.SetBool ( "IsMoving", false );
			CancelInvoke ( "Movement" );
			moving = false;
			velocity.x = 0;
		}

		velocity.y -= gravity;
		if (velocity.y < -gravity && CC.isGrounded)
			velocity.y = -gravity;

		//recoil
		if ( recoil != 0f ) {
			velocity.x += recoil;
			recoil = 0f;
		}

		CC.Move ( velocity * Time.deltaTime );

		velocity.x -= direction * DecaySpeed;
		if ( !(direction * velocity.x >= 0) )
			velocity.x = 0;

	}
}
