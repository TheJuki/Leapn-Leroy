using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignDialogEntrance : MonoBehaviour {

	public Text actionButtonText;
	public Text signDialogText;
	public RawImage signDialogImage;
	public Image signDialogPanel;

	private bool canTalk = false;
	private bool isSpeaking = false;

	private AudioSource nextDialogSoundSource; 
	public AudioClip nextDialogSoundClip;

	private int numDialog = 1;

	// Use this for initialization
	void Start () {
		canTalk = false;
		isSpeaking = false;
		numDialog = 1;

		nextDialogSoundSource = gameObject.AddComponent( typeof(AudioSource) ) as AudioSource;
		nextDialogSoundSource.playOnAwake = false;
		nextDialogSoundSource.clip = nextDialogSoundClip;
	}
	
	// Update is called once per frame
	void Update () {
		if (canTalk && !isSpeaking) {
			isSpeaking = TeamUtility.IO.InputManager.GetButton("Submit");
			numDialog = 1;
		}

		if (isSpeaking && numDialog == 1) {
			nextDialogSoundSource.Play ();
			actionButtonText.enabled = false;
			signDialogText.enabled = true;
			signDialogImage.enabled = true;
			signDialogPanel.enabled = true;
			signDialogText.text = "Welcome to the Jungle World Leroy! ... ";
			numDialog++;
			return;
		}

		if (canTalk && isSpeaking) {
			bool next = TeamUtility.IO.InputManager.GetButtonDown ("Submit");

			if (next && numDialog == 2) {
				nextDialogSoundSource.Play ();
				signDialogText.text = "There are 100 oranges hidden around this world. " +
					"You will need at least 50 of them to open the next world. ... ";
				numDialog = 3;
				return;
			}

			if (next && numDialog == 3) {
				nextDialogSoundSource.Play ();
				signDialogText.text = "Oh and due to time limitations the developers only finished making" +
					" one enemy. That spider...behind me. Have fun!";
				numDialog = 4;
				return;
			}

			if (next && numDialog == 4) {
				nextDialogSoundSource.Play ();
				signDialogText.enabled = false;
				signDialogImage.enabled = false;
				signDialogPanel.enabled = false;
				canTalk = false;
				return;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			actionButtonText.enabled = true;
			actionButtonText.text = "Press A to Talk";
			canTalk = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			actionButtonText.enabled = false;
			signDialogText.enabled = false;
			signDialogImage.enabled = false;
			signDialogPanel.enabled = false;
			canTalk = false;
			isSpeaking = false;

		}
	}
}
