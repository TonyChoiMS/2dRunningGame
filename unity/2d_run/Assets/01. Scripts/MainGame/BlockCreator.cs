using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    // Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (false == _isCreate)
            return;

        // 일정 거리로 블럭을 생성
        float distance = transform.position.x - _lastBlockObject.transform.position.x;
        //if(20 <= distance)
        if (15 <= distance)
        {
            _lastBlockObject = CreateBlock();
        }
    }

    // Create

    public GameObject BlockPrefabs;
    public GameObject VegetablePrefabs;
    public GameObject MeatPrefabs;

    GameObject _lastBlockObject;

    GameObject CreateBlock()
    {
        GameObject blockObject = GameObject.Instantiate(BlockPrefabs);
        blockObject.transform.position = transform.position;

        GameObject coin01;
        GameObject coin02;
        int selectCoin = Random.Range(0, 1000);
        if (selectCoin < 500)
        {
            coin01 = GameObject.Instantiate(VegetablePrefabs);
            coin02 = GameObject.Instantiate(MeatPrefabs);
        }
        else
        {
            coin01 = GameObject.Instantiate(MeatPrefabs);
            coin02 = GameObject.Instantiate(VegetablePrefabs);
        }

        // 코인을 생성 -> 2층에 배치

        coin01.transform.position = new Vector2(transform.position.x,
                                                transform.position.y + 3.5f);

        int randValue = Random.Range(0, 1000);
        if(randValue < 300)
        {
            // 2층
            blockObject.transform.position = new Vector2(blockObject.transform.position.x,
                blockObject.transform.position.y + 3.5f);

            // 코인을 1층 배치로 변경
            coin01.transform.position = transform.position;
        }

        // 3층
        {
            coin02.transform.position = new Vector2(transform.position.x,
                                                    transform.position.y + 6.5f);
        }

        return blockObject;
    }


    bool _isCreate = false;

    public void StartCreate()
    {
        _isCreate = true;
        _lastBlockObject = CreateBlock();
    }
}
