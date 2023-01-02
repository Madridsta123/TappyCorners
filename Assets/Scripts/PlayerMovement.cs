using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private bool reversed=false;

    [SerializeField]
    private float incrementSpeed;

    private Touch touch;

    [SerializeField]
    private float startspeed;

    private float speed;

    [SerializeField]
    private int speedDirectionIndex = -1;

    private const float randomRangeLimit = 0.6f;
    private const float initialSpeed = 0.05f;

    private void Awake()
    {
        speed = startspeed;
        speedDirectionIndex = 0;
        incrementSpeed = 0f;
    }   
    void Update()
    {
        speed += incrementSpeed * Time.deltaTime;
        PlayerInput();
        Movement();
    }
    private void PlayerInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!reversed)
                {
                    speedDirectionIndex++;
                }
                else
                {
                    speedDirectionIndex--;
                }

                if (speedDirectionIndex < 0)
                {
                    speedDirectionIndex = 3;
                }
                else if (speedDirectionIndex == 4)
                {
                    speedDirectionIndex = 0;
                }
            }
        }
    } 
    void Movement()
    {
        transform.position += speed * Time.deltaTime * (Vector3)ChangeDirection(speedDirectionIndex).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            GameManager.Instance.EndGame();
        }
        else if (collision.gameObject.CompareTag("Score"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.collectibleSpawnCheck = true;
            GameManager.Instance.UpdateScore();
            incrementSpeed += initialSpeed;
            if (Random.Range(0f, 1f) > randomRangeLimit)
            {
                 speedDirectionIndex=OppositeDirection(speedDirectionIndex);
                reversed = !reversed;
            }
        }
    }

    /*
     * 0=>Up
     * 1=>Left
     * 2=>Down
     * 3=>Right
     * movement order 0>1>2>3>0.... and vice versa in opposite
     */

    //Change to a specific direction of index
    private Vector2 ChangeDirection(int index)
    {
        switch (index)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.down;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    //Go in direction opposite to current
    private int OppositeDirection(int index)
    {
        switch(index)
        {
            case 0:
                return 2;

            case 1:
                return 3;

            case 2:
                return 0;

            case 3:
                return 1;

            default:
                return index;
        }
    }
}
