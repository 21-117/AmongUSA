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

        //�ӵ�, ȸ����
        addForceVel = Random.Range(-0.1f, 0.1f);
        addForceAngle = Random.Range(-0.2f, 0.2f);

        // ������
        chaScale = Random.Range(0.3f, 0.6f);
        
        // ���̽�
        num = Random.Range(0, 3);

        //���� ��ġ ����
        xx = Random.Range(900f, -900f);
        //xy = Random.Range(540, -540f);
        yy = Random.Range(500f, -500f);
        //yz = Random.Range(540, -540f);
        rectTransform.localPosition = new Vector3(xx, yy, rectTransform.position.z);

        //������ ����
        rectTransform.localScale = new Vector3(chaScale, chaScale, chaScale);
        
        switch (num)
        {
            // 4�и� ���
            case 0:
                //rectTransform.localScale = new Vector3(chaScale,chaScale,chaScale);
                break;
            // ���ϰ�
            case 1:
                break;
            // ���ϰ�
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

    // ĳ���� ���ӽð�
    IEnumerator ChaStartEnd()
    {
        yield return new WaitForSeconds(30f);
        Destroy(this.rectTransform.gameObject);
    }
}
