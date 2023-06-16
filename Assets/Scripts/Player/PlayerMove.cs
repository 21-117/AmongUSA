using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject player;
    Animator animator;
    float playerSpeed = 0f;

    private void Start()
    {
        player = this.transform.gameObject;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        playerSpeed = 2.0f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + playerSpeed * Time.deltaTime, transform.position.z);
            MoveAnimation(true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.localEulerAngles = new Vector3(0, 180, 0);
            MoveAnimation(true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - playerSpeed * Time.deltaTime, transform.position.z);
            MoveAnimation(true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.localEulerAngles = new Vector3().normalized;
            MoveAnimation(true);
        }
        else
        {
            MoveAnimation(false);
        }
    }

    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }
}
