using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 플레이어 이동 제어 (키보드, 마우스) 사운드 처리
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    Animator animator;
    private SpriteRenderer spriteRenderer;
    private float playerSpeed = 0f;
    private bool _isMove = false;
    public bool _isButtonPressed = false, _isKill = false;
    Vector3 velocity, next_velocity;
    public Transform targetTransform;

    AudioSource audioSource;

    void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }


    private void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>(); 
        playerSpeed = 2.0f;
        velocity = Camera.main.transform.position;
        audioSource = this.transform.GetComponent<AudioSource>();
    }

    // 걷는 사운드 재생
    private void FixedUpdate()
    {
        MovingPlayer();
        if (_isMove && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

    }

    // Input.GetAxis() 로 처리 시 플레이어가 미끄러지는 느낌이 발생하여 인게임과 다름
    // 따라서 트랜스폼으로 플레이어 이동 제어

    public void MovingPlayer()
    {
        _isMove = false;

        // 키보드 입력 제어
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

            // 2개 이상 키 누를 시 스피드 보간? 처리..
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                playerSpeed = 1.5f;
            }
            else
            {
                playerSpeed = 2f;
            }
        }

        // 마우스 버튼 제어
        if (Input.GetMouseButton(0) && !_isButtonPressed)
        {
            next_velocity = Input.mousePosition;
            if (velocity.x - next_velocity.x < -450) { spriteRenderer.flipX = false; }
            else if (velocity.x - next_velocity.x > -450) { spriteRenderer.flipX = true; }

            Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
            transform.position += dir * playerSpeed * Time.deltaTime;
            _isMove = dir.magnitude != 0f;


        }

        MoveAnimation(_isMove);
    }

    // 이동 애니메이션
    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }

}
