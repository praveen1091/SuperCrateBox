using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Animator TreeAnimation;
	public float speed = 0.3f, HP = 2f;
	float dirrection = 1f;

	// Use this for initialization
	void Start () {
		TreeAnimation = GetComponent<Animator> ();
		TreeAnimation.SetFloat ("Speed", 0.3f);

		if ( dirrection == 1f ) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 1f);
			transform.rotation = Quaternion.Euler ( 0, 0, 0 );
		}
		else {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -1f);
			transform.rotation = Quaternion.Euler ( 0, 180f, 0 );
		}
	}

	void OnCollisionEnter ( Collision collision ) {
		if ( collision.collider.transform.parent != null && collision.collider.transform.parent.tag == "Margin" ) {
			if ( dirrection == 1f ) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, -1f);
				transform.rotation = Quaternion.Euler ( 0, 0, 0 );
			}
			else {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 1f);
				transform.rotation = Quaternion.Euler ( 0, 180f, 0 );
			}
			dirrection *= -1f;
		}
	}

	public void Damage ( float dmg ) {
		HP -= dmg;
		if ( HP <= 0f )
			Destroy ( gameObject );
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (speed * Time.deltaTime, 0, 0) * dirrection;
	}
}
