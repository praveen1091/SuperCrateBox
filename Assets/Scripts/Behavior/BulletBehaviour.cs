using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour {

	float damage;

	void DestroyBullet () {
		Destroy ( gameObject );
	}

	// Use this for initialization
	void OnCollisionEnter ( Collision collision ) {
		if ( collision.collider.tag != "Bullet" ) {
			if (collision.collider.tag == "Monster")
				collision.gameObject.GetComponent<EnemyMovement> ().Damage (damage);

			DestroyBullet ();
		}
	}

	public void Shoot ( Vector3 direction, float duration, float dmg ) {
		damage = dmg;
		if ( duration > 0f )
			Invoke ( "DestroyBullet", duration );

		rigidbody.AddForce ( direction, ForceMode.Impulse );
	}
}
