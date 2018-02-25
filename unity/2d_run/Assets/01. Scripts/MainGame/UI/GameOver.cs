using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverText;

	// Use this for initialization
	void Start ()
    {
        GameOverText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (GameManager.Instance.GetPlayer().IsDead())
        {
            GameOverText.SetActive(true);
        }
	}
}
