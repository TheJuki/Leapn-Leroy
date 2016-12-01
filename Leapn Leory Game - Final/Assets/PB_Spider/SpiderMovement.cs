using UnityEngine;
using System.Collections;

public class SpiderMovement : MonoBehaviour {

	public Animator anim; 
	public float speed;
	public float movementSpeed;

	public Transform Player;
	public float MoveSpeed;
	public float awayDistance;
	public float closeUpDistance;

	public bool nearPlayer = false;
	private bool playerOnMe = false;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.name == "Player" ) {
			
			playerOnMe = true;

		}
	}
	
	// Update is called once per frame
	void Update () {

		anim.SetFloat("Speed", speed);
		anim.SetBool("Near Player", nearPlayer);
		//transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

		if (!playerOnMe) {
			Vector3 lookAt = Player.position;
			lookAt.y = transform.position.y;
			transform.LookAt (lookAt);
		}

		//print(Vector3.Distance (transform.position, Player.position));
		
		if (Vector3.Distance (transform.position, Player.position) >= awayDistance) {

			speed = 1;

			transform.position += transform.forward * MoveSpeed * Time.deltaTime;

			nearPlayer = false;

		} else {

			speed = 0;
			nearPlayer = true;
		}
			
	}
}
