using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (위 + 아래, 좌 + 우) 랜덤 방향으로 패트롤 하기
// 벽과 부딪히는 경우 계산
public class NPCMove : MonoBehaviour
{
    public float speed = 1.0f;
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private int _currentWaypointIndex = 0;
    private bool _waiting = false, _turn = false;
    public bool isDead = false;

    private SpriteRenderer spriteRenderer;
    public List<Transform> waypoints;
    Animator animator;

    void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (!isDead)
        {
            if (_waiting)
            {
                MoveAnimation(false);
                _waitCounter += Time.deltaTime;
                if (_waitCounter < _waitTime)
                    return;
                _waiting = false;
                if (!_turn) { _turn = true; spriteRenderer.flipX = false; }
                else if (_turn) { _turn = false; spriteRenderer.flipX = true; }
            }
            else
            {
                MoveAnimation(true);
            }

            Transform wp = waypoints[_currentWaypointIndex];
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
        else if(isDead)
        {
            DyingAnimation(isDead);
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
