using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어에 메인 카메라를 러프하게 트래킹 하기
public class CameraTracking : MonoBehaviour
{
    public Transform player;

    [Range(0, 10)]
    public float smoothPosFactor = 5f;
    void FixedUpdate()
    {
        var curPos = transform.position;

        Vector3 xPos = Vector3.Lerp(curPos, player.transform.position, Time.deltaTime * smoothPosFactor);
        transform.position = new Vector3(xPos.x, xPos.y, transform.position.z);

    }
}
