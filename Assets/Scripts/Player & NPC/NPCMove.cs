using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지정 포인트로 왕복 하기

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

            if (!isDead)
            {

                Transform wp = waypoints[_currentWaypointIndex];

                if (_waiting)
                {
                    MoveAnimation(false);
                    _waitCounter += Time.deltaTime;
                    if (_waitCounter < _waitTime)
                        return;
                    _waiting = false;

                    velocity = transform.position;
                    next_velocity = wp.transform.position;
                    if (velocity.x - next_velocity.x < 0) { _turn = true; spriteRenderer.flipX = false; }
                    else if (velocity.x - next_velocity.x > 0) { _turn = false; spriteRenderer.flipX = true; }
                }
                else
                {
                    MoveAnimation(true);
                }


                if (Vector3.Distance(transform.position, wp.position) < 0.01f)
                {
                    transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Count;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        wp.position,
                        speed * Time.deltaTime);
                }



            }
            else if (isDead)
            {
                DyingAnimation(isDead);
                isMoving = false;
                sceneM.NPC_DEADCOUNT++;
            }
        }


    }

    private void MoveAnimation(bool value)
    {
        animator.SetBool("isMove", value);
    }
    private void DyingAnimation(bool value)
    {
        animator.SetBool("isDead", value);
    }

}
