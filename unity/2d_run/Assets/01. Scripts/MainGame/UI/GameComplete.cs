using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour {

    public GameObject GameCompleteText;
    public Text SuccessFailText;

    // Use this for initialization
    void Start()
    {
        GameCompleteText.SetActive(false);
        SuccessFailText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetPlayer().IsComplete())
        {
            GameCompleteText.SetActive(true);

            float currentWeight = GameManager.Instance.GetPlayer().GetCurrentWeight();
            float goalWeight = GameManager.Instance.GetPlayer().GetGoalWeight();
            if (goalWeight-5.0f <= currentWeight && currentWeight <= goalWeight + 5.0f)
            {     
                SuccessFailText.text = "SUCCESS";
                SuccessFailText.color = Color.yellow;
            }
            else
            {
                SuccessFailText.text = "FAILED";
                SuccessFailText.color = Color.red;
            }
            SuccessFailText.gameObject.SetActive(true);
           
        }
    }
}
