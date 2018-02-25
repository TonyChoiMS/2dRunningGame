using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 유니티가 기본 제공 함수

    void Awake()
    {
        ChangeState(eState.IDLE);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 2.0f, layerMask);
        if (null != distanceFromGround.transform)
        {
            _isGround = true;
            gameObject.GetComponent<Animator>().SetBool("isGround", _isGround);
        }
        else
        {
            _isGround = false;
            gameObject.GetComponent<Animator>().SetBool("isGround", _isGround);
        }

        if (eState.RUN == _state)
        {
            if (_velocity.x < _maxSpeed)
            {
                _velocity.x += _addSpeed;
            }
            else
            {
                _velocity.x = _maxSpeed;
            }
            gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);

            // Update HP
            _currentHP -= _decreaseHP;
            if (_currentHP < 0)
            {
                _currentHP = 0;
                ChangeState(eState.DEAD);
            }

            // Update Weight
            AddWeight(-_decreaseWeight);

            // Update Distance
            float deltaDistance = _velocity.x * Time.deltaTime;
            _distance += deltaDistance;
            if (_maxDistance < _distance)
            {
                ChangeState(eState.COMPLETE);
            }
        }
    }

    public enum eState
    {
        IDLE,
        RUN,
        DEAD,
        COMPLETE,
    }
    eState _state = eState.IDLE;

    // Distance
    float _maxDistance = 50.0f;
    float _distance = 0.0f;

    public float GetMaxDistance()
    {
        return _maxDistance;
    }

    public float GetCurrentDistance()
    {
        return _distance;
    }

    public bool IsComplete()
    {
        return (_distance > _maxDistance);
    }

    // HP
    float _maxHP = 100.0f;
    float _currentHP = 100.0f;
    float _decreaseHP = 0.1f;

    void IncreaseHP(int addHP)
    {
        _currentHP += addHP;
        if (_maxHP < _currentHP)
        {
            _currentHP = _maxHP;
        }
    }

    public float GetMaxHP()
    {
        return _maxHP;
    }

    public float GetCurrentHP()
    {
        return _currentHP;
    }

    // Weight
    float _currentWeight = 80.0f;
    float _decreaseWeight = 0.1f;
    float _minWeight = 40.0f;
    float _maxWeight = 120.0f;
    float _goalWeight = 40.0f;

    public float GetCurrentWeight()
    {
        return _currentWeight;
    }

    public float GetMaxWeight()
    {
        return _maxWeight;
    }

    public float GetGoalWeight()
    {
        return _goalWeight;
    }

    void AddWeight(float addWeight)
    {
        _currentWeight += addWeight;
        if (_currentWeight < _minWeight)
            _currentWeight = _minWeight;

        if (_currentWeight > _maxWeight)
            _currentWeight = _maxWeight;

        // Update Max Speed
        if (110.0f < _currentWeight)
            _maxSpeed = 7.0f;
        else if (90.0f < _currentWeight)
            _maxSpeed = 9.0f;
        else if (80.0f < _currentWeight)
            _maxSpeed = 11.0f;
        else if (65.0f < _currentWeight)
            _maxSpeed = 12.0f;
        else if (40.0f < _currentWeight)
            _maxSpeed = 15.0f;

        // Update Jump Level
    }


    public bool IsDead()
    {
        return (eState.DEAD == _state);
    }

    // Speed
    //float _maxSpeed = 30.0f;
    //float _addSpeed = 0.05f;
    float _maxSpeed = 15.0f;
    float _addSpeed = 0.05f;

    public void ChangeState(eState state)
    {
        _state = state;
        switch (state)
        {
            case eState.IDLE:
                // 아이들 애니메이션 : 애니메이터에 isGround 패러미터를 true 만들어 주면된다.
                _velocity.x = 0.0f;
                _velocity.y = 0.0f;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.RUN:
                // 달리기 애니메이션
                _velocity.x = 0.0f;
                _velocity.y = 0.0f;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.DEAD:
                _velocity = Vector2.zero;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.COMPLETE:
                _velocity = Vector2.zero;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
        }
    }
        

    // Move

    Vector2 _velocity = Vector2.zero;

    bool _isGround = false;
    bool _canDoubleJump = false;

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    void Jump()
    {
        if(_isGround)
        {
            if (_currentWeight < 60.0f)
                _canDoubleJump = true;
            else
                _canDoubleJump = false;

            JumpAction();
        }
        else if(true == _canDoubleJump)
        {
            _canDoubleJump = false;
            JumpAction();
        }
    }

    void JumpAction()
    {
        //float jumpPower = 500.0f;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        float jumpSpeed = 30.0f;
        Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        velocity.y = jumpSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;

        gameObject.GetComponent<Animator>().SetTrigger("Jump");
    }

    void ResetSpeed()
    {
        _velocity.x = 0.0f;
        _currentHP -= 10.0f;
        if (_currentHP < 0.0f)
        {
            _currentHP = 0;
        }

    }
}

