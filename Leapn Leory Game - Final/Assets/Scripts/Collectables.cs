using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectables : MonoBehaviour {

	public static int orangeCount;
	public static int lemurCount;
	private int lastOrangeCount;
	private int lastLemurCount;

	public const int MAX_ORANGES = 100;
	public const int MAX_LEMURS = 1;

	public Text orangeCountText;
	public Text orangeCountTextMenu;
	public Text lemurCountText;

	void Awake()
	{
		orangeCount = 0;
		lemurCount = 0;
		lastOrangeCount = 0;
		lastLemurCount = 0;
	}

	// Use this for initialization
	void Start () {
		orangeCountText.text = 0 + "/" + MAX_ORANGES;
		orangeCountTextMenu.text = 0 + "/" + MAX_ORANGES;
		lemurCountText.text = 0 + "/" + MAX_LEMURS;
	}
	
	// Update is called once per frame
	void Update () {

		if (orangeCount != lastOrangeCount) {
			orangeCountText.text = orangeCount + "/" + MAX_ORANGES;
			orangeCountTextMenu.text = orangeCount + "/" + MAX_ORANGES;
			lastOrangeCount = orangeCount;
		}
		if (lemurCount != lastLemurCount) {
			lemurCountText.text = lemurCount + "/" + MAX_LEMURS;
			lastLemurCount = lemurCount;
		}
	}
}
