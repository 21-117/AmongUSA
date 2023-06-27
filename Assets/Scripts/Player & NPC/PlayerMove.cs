using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �÷��̾� �̵� ���� (Ű����, ���콺) ���� ó��
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

    // �ȴ� ���� ���
    private void FixedUpdate()
    {
        MovingPlayer();
        if (_isMove && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

    }

    // Input.GetAxis() �� ó�� �� �÷��̾ �̲������� ������ �߻��Ͽ� �ΰ��Ӱ� �ٸ�
    // ���� Ʈ���������� �÷��̾� �̵� ����

    public void MovingPlayer()
    {
        _isMove = false;

        // Ű���� �Է� ����
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

            // 2�� �̻� Ű ���� �� ���ǵ� ����? ó��..
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                playerSpeed = 1.5f;
            }
            else
            {
                playerSpeed = 2f;
            }
        }

        // ���콺 ��ư ����
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

    // �̵� �ִϸ��̼�
    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }

}
