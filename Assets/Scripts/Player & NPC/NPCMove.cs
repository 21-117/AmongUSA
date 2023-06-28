using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC�� ������ ��������Ʈ ����Ʈ�� ���������� ����
public class NPCMove : MonoBehaviour
{
    public float speed = 1.0f;
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private int _currentWaypointIndex = 0;
    private bool _waiting = false, _turn = false;
    public bool isDead = false, isMoving = false;
    SceneManager_MainMap sceneM;

    private SpriteRenderer spriteRenderer;
    public List<Transform> waypoints;
    Animator animator;

    Vector3 velocity, next_velocity;

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>();
        sceneM = FindAnyObjectByType<SceneManager_MainMap>();
        sceneM.NPC_COUNT++;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            // �ױ������� �̵��ϱ�
            if (!isDead)
            {
                Transform wp = waypoints[_currentWaypointIndex];

                // �̵� �� ���ð��� ���� �� ���� ��������Ʈ�� �̵�
                if (_waiting)
                {
                    MoveAnimation(false);
                    _waitCounter += Time.deltaTime;
                    if (_waitCounter < _waitTime)
                        return;
                    _waiting = false;

                    velocity = transform.position;
                    next_velocity = wp.transform.position;

                    // �ȴ� ���⿡ �°� ��������Ʈ ������ �ٲ��ֱ�
                    if (velocity.x - next_velocity.x < 0) { _turn = true; spriteRenderer.flipX = false; }
                    else if (velocity.x - next_velocity.x > 0) { _turn = false; spriteRenderer.flipX = true; }
                }
                else
                {
                    MoveAnimation(true);
                }

                // ��������Ʈ�� �ٴٶ��ٸ� ���� �ִ� ��������Ʈ ����Ʈ �ε��� ����
                if (Vector3.Distance(transform.position, wp.position) < 0.01f)
                {
                    transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
                }
                else
                {
                    // ������ ��������Ʈ�� �̵��ϱ�
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        wp.position,
                        speed * Time.deltaTime);
                }
            }
            // �׾��ٸ�
            else if (isDead)
            {
                DyingAnimation(isDead);
                isMoving = false;
                this.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                sceneM.NPC_DEADCOUNT++;
            }
        }
    }

    // �ȱ�
    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }
    // �״� �ִϸ��̼�
    private void DyingAnimation(bool value)
    {
        animator.SetBool("isDead", value);
    }

}
