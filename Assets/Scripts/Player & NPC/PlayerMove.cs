using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    GameObject player;
    Animator animator;
    private SpriteRenderer spriteRenderer;
    private float playerSpeed = 0f;
    private bool _isMove = false;
    public bool _isButtonPressed = false, _isKill = false;
    Vector3 velocity, next_velocity;
    public Transform targetTransform;

    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        player = this.transform.gameObject;
        animator = this.transform.GetComponent<Animator>();
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>(); 
        playerSpeed = 2.0f;
        velocity = Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        MovingPlayer();

    }

    public void MovingPlayer()
    {
        _isMove = false;

        if (!Input.GetMouseButton(0))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + playerSpeed * Time.deltaTime, transform.position.z);
                _isMove = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                spriteRenderer.flipX = true;
                _isMove = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - playerSpeed * Time.deltaTime, transform.position.z);
                _isMove = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                spriteRenderer.flipX = false;
                _isMove = true;
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                playerSpeed = 1.5f;
            }
            else
            {
                playerSpeed = 2f;
            }
        }


        if (Input.GetMouseButton(0) && !_isButtonPressed)
        {
            next_velocity = Input.mousePosition;
            if (velocity.x - next_velocity.x < -450) { spriteRenderer.flipX = false; }
            else if (velocity.x - next_velocity.x > -450) { spriteRenderer.flipX = true; }

            Debug.Log(velocity.x - next_velocity.x);

            Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
            transform.position += dir * playerSpeed * Time.deltaTime;
            _isMove = dir.magnitude != 0f;


        }

        MoveAnimation(_isMove);
    }

    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }

}
