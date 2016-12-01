using UnityEngine;
using System.Collections;

public class Player_Pickup : MonoBehaviour {

	private AudioSource orangePickupSource; 
	public AudioClip orangePickupSoundClip;

	private AudioSource lemurPickupSource; 
	public AudioClip lemurPickupSoundClip;

	public ParticleSystem lemurEffect;

	// Use this for initialization
	void Start () {

		lemurEffect.Stop (true);

		orangePickupSource = gameObject.AddComponent( typeof(AudioSource) ) as AudioSource;
		orangePickupSource.playOnAwake = false;
		orangePickupSource.clip = orangePickupSoundClip;

		lemurPickupSource = gameObject.AddComponent( typeof(AudioSource) ) as AudioSource;
		lemurPickupSource.playOnAwake = false;
		lemurPickupSource.clip = lemurPickupSoundClip;
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag ("Orange")) {

			other.gameObject.SetActive (false);
			orangePickupSource.Play ();

			if (Collectables.orangeCount != Collectables.MAX_ORANGES) {
				Collectables.orangeCount += 1;
			}
		}

		if (other.gameObject.CompareTag ("Lemur")) {

			if (Collectables.lemurCount != Collectables.MAX_LEMURS) {
				Collectables.lemurCount += 1;

				StartCoroutine(PlayBabyLemurEffect(other.gameObject));
			}
		}
	}

	IEnumerator PlayBabyLemurEffect(GameObject lemur) {
		lemurEffect.Play (true);
		yield return new WaitForSeconds(.5f);
		lemurPickupSource.Play ();
		yield return new WaitForSeconds(1f);
		lemurEffect.Stop (true);
		lemur.SetActive (false);
	}
}
