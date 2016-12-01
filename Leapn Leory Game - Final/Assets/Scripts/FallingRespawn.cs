using UnityEngine;
using System.Collections;

public class FallingRespawn : MonoBehaviour {

	float fallZone = 90f;
	public Transform playerSpawnPoint;   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.y < fallZone)
		{
			transform.position = playerSpawnPoint.position;
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
	
	}
}
