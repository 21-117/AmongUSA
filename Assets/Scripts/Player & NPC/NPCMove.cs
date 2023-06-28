using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC가 정해진 웨이포인트 리스트를 순차적으로 돌기
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
            // 죽기전까진 이동하기
            if (!isDead)
            {
                Transform wp = waypoints[_currentWaypointIndex];

                // 이동 후 대기시간을 갖고 그 다음 웨이포인트로 이동
                if (_waiting)
                {
                    MoveAnimation(false);
                    _waitCounter += Time.deltaTime;
                    if (_waitCounter < _waitTime)
                        return;
                    _waiting = false;

                    velocity = transform.position;
                    next_velocity = wp.transform.position;

                    // 걷는 방향에 맞게 스프라이트 랜더러 바꿔주기
                    if (velocity.x - next_velocity.x < 0) { _turn = true; spriteRenderer.flipX = false; }
                    else if (velocity.x - next_velocity.x > 0) { _turn = false; spriteRenderer.flipX = true; }
                }
                else
                {
                    MoveAnimation(true);
                }

                // 웨이포인트에 다다랐다면 갖고 있는 웨이포인트 리스트 인덱스 증가
                if (Vector3.Distance(transform.position, wp.position) < 0.01f)
                {
                    transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
                }
                else
                {
                    // 지정된 웨이포인트로 이동하기
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        wp.position,
                        speed * Time.deltaTime);
                }
            }
            // 죽었다면
            else if (isDead)
            {
                DyingAnimation(isDead);
                isMoving = false;
                this.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                sceneM.NPC_DEADCOUNT++;
            }
        }
    }

    // 걷기
    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }
    // 죽는 애니메이션
    private void DyingAnimation(bool value)
    {
        animator.SetBool("isDead", value);
    }

}
