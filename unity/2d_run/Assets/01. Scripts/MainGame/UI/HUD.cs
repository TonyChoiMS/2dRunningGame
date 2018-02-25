using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text RemainDistanceText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float maxDistance = GameManager.Instance.GetPlayer().GetMaxDistance();
        float currentDistance = GameManager.Instance.GetPlayer().GetCurrentDistance();
        float remain = maxDistance - currentDistance;
        RemainDistanceText.text = "Remain " + remain;
	}
}
