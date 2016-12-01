using UnityEngine;
using System.Collections;

public class TurnToPlayer : MonoBehaviour {

	//values that will be set in the Inspector
	public Transform Target;
	public float RotationSpeed;

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Update is called once per frame
	void Update()
	{
		Vector3 targetDir = Target.position - transform.position;
		targetDir.y = 0;
		targetDir.z = 0;
		float step = RotationSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}
