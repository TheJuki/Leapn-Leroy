using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagePlayer : MonoBehaviour {

	private int health;
	private const int MAX_HEALTH = 6;

	public RawImage livesDisplay;
	public Texture lives0;
	public Texture lives1;
	public Texture lives2;
	public Texture lives3;
	public Texture lives4;
	public Texture lives5;
	public Texture lives6;

	public GameObject deathPanel;

	public SkinnedMeshRenderer bodyRender;

	private AudioSource petalPickupSource; 
	public AudioClip petalPickupSoundClip;

	private AudioSource hurtSoundSource; 
	public AudioClip hurtSoundClip;

	private bool touchedEnemy = false;
	private bool isFlashingPlayer = false;

	public Transform playerSpawnPoint;   

	// Use this for initialization
	void Start () {
		livesDisplay.texture = lives6;
		health = MAX_HEALTH;
		touchedEnemy = false;
		isFlashingPlayer = false;

		petalPickupSource = gameObject.AddComponent( typeof(AudioSource) ) as AudioSource;
		petalPickupSource.playOnAwake = false;
		petalPickupSource.clip = petalPickupSoundClip;

		hurtSoundSource = gameObject.AddComponent( typeof(AudioSource) ) as AudioSource;
		hurtSoundSource.playOnAwake = false;
		hurtSoundSource.clip = hurtSoundClip;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy") && !touchedEnemy && !isFlashingPlayer) {

			health--;
			touchedEnemy = true;

			hurtSoundSource.Play ();

			updatePetalDisplay ();

			if (health <= 0) {
				StartCoroutine(Die ());
			}

			StartCoroutine (flashColorPlayer());
		}
		if (other.gameObject.CompareTag ("Petal")) {

			StartCoroutine (PetalCooldown(other.gameObject));

			petalPickupSource.Play ();

			if (health != MAX_HEALTH) {
				health += 1;
				updatePetalDisplay ();
			}
		}
	}

	void updatePetalDisplay()
	{
		switch (health) {
		case(1): 
			livesDisplay.texture = lives1; 
			break;
		case(2):
			livesDisplay.texture = lives2;
			break;
		case(3):
			livesDisplay.texture = lives3;
			break;
		case(4):
			livesDisplay.texture = lives4;
			break;
		case(5):
			livesDisplay.texture = lives5;
			break;
		case(6):
			livesDisplay.texture = lives6;
			break;
		default : livesDisplay.texture = lives0;
			break;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			touchedEnemy = false;
		}
	}
		
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PetalCooldown(GameObject petal)
	{
		petal.SetActive(false);
		yield return new WaitForSeconds (15f); 
		petal.SetActive(true);
	}

	IEnumerator Die()
	{
		// Go to death position
		transform.Rotate(new Vector3(0,0,90));

		yield return new WaitForSeconds (0.5f); 
		// Display death screen
		deathPanel.SetActive(true);

		// Wait before respawning
		yield return new WaitForSeconds (3f); 
		livesDisplay.texture = lives6;
		health = 6;
		transform.position = playerSpawnPoint.position;
		transform.eulerAngles = new Vector3(0, 180, 0);
		touchedEnemy = false;
		deathPanel.SetActive(false);
	}

	IEnumerator flashColorPlayer()
	{   
		Material m = bodyRender.material;
		Color32 c = bodyRender.material.color;

		Color redColor = new Color();
		ColorUtility.TryParseHtmlString ("#c10303", out redColor);

		isFlashingPlayer = true;

		for (int i = 0; i < 3; i++) {
			bodyRender.material.color = redColor;    
			yield return new WaitForSeconds (0.6f); 
			bodyRender.material.color = c;  
			yield return new WaitForSeconds (0.6f); 
		}

		bodyRender.material.color = c;
		isFlashingPlayer = false;
	}
}
