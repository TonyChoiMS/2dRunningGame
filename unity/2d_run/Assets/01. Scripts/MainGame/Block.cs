using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Vector2 _velocity = Vector2.zero;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = _velocity;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 playerVelociy = GameManager.Instance.GetPlayer().GetVelocity();
        if(playerVelociy != _velocity)
        {
            _velocity = playerVelociy;
            gameObject.GetComponent<Rigidbody2D>().velocity = -_velocity;
        }

        // 자기 자신의 위치가 화면을 벗어났을 정도의 x 좌표라면
        // 자기 자신을 파괴
        if(transform.position.x < -15)
        {
            GameManager.Destroy(gameObject, 1.0f);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player" == collision.tag)
        {
            collision.gameObject.SendMessage("ResetSpeed");
        }
    }
}
