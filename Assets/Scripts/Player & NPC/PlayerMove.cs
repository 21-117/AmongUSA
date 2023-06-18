using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Animator animator;
    private SpriteRenderer spriteRenderer;
    float playerSpeed = 0f;

    private bool _isMove = false;

    private void Start()
    {
        player = this.transform.gameObject;
        animator = this.transform.GetComponent<Animator>();
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>(); 
        playerSpeed = 2.0f;

        //Camera cam = Camera.main;
        //cam.transform.SetParent(transform);
        //cam.transform.localPosition = new Vector3(0f, 0f, -2.5f);
        //cam.orthographicSize = 1.5f;
    }

    private void FixedUpdate()
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
        }

        if (Input.GetMouseButton(0))
        {
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
