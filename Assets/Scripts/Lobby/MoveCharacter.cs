using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    private float xx, xy, yy, yz;
    private float addForceAngle = 0f, addForceVel = 0.1f, chaScale = 0.5f;
    private RectTransform rectTransform;
    private int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //속도, 회전값
        addForceVel = Random.Range(-0.1f, 0.1f);
        addForceAngle = Random.Range(-0.2f, 0.2f);

        // 스케일
        chaScale = Random.Range(0.3f, 0.6f);
        
        // 케이스
        num = Random.Range(0, 3);

        //시작 위치 랜덤
        xx = Random.Range(900f, -900f);
        //xy = Random.Range(540, -540f);
        yy = Random.Range(500f, -500f);
        //yz = Random.Range(540, -540f);
        rectTransform.localPosition = new Vector3(xx, yy, rectTransform.position.z);

        //스케일 랜덤
        rectTransform.localScale = new Vector3(chaScale, chaScale, chaScale);
        
        switch (num)
        {
            // 4분면 경계
            case 0:
                //rectTransform.localScale = new Vector3(chaScale,chaScale,chaScale);
                break;
            // 약하게
            case 1:
                break;
            // 강하게
            case 2:
                break;
            case 3:
                break;
        }

        StartCoroutine(ChaStartEnd());
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x + addForceVel, rectTransform.localPosition.y + addForceVel, rectTransform.localPosition.z);
        rectTransform.localEulerAngles = new Vector3(rectTransform.localEulerAngles.x, rectTransform.localEulerAngles.y, rectTransform.localEulerAngles.z + addForceAngle);
    }

    // 캐릭터 지속시간
    IEnumerator ChaStartEnd()
    {
        yield return new WaitForSeconds(30f);
        Destroy(this.rectTransform.gameObject);
    }
}
