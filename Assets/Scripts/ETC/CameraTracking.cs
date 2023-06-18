using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform player;

    [Range(0, 10)]
    public float smoothPosFactor = 5f;
    // Update is called once per frame
    void FixedUpdate()
    {
        var curPos = transform.position;

        Vector3 xPos = Vector3.Lerp(curPos, player.transform.position, Time.deltaTime * smoothPosFactor);
        transform.position = new Vector3(xPos.x, xPos.y, transform.position.z);

        //var cameraCenter = Camera.main.transform.position += Camera.main.transform.forward * distanceToPlayer;
    }
}
