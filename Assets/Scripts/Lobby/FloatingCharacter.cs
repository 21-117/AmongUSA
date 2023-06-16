using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCharacter : MonoBehaviour
{
    public ChaColors chaColor;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private float floatingSpeed;
    private float rotateSpeed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFloatingCrew(Sprite sprite, ChaColors characterColor, Vector3 dir, float fSpeed, float rSpeed, float size)
    {
        this.chaColor = characterColor;
        this.direction = dir;
        this.floatingSpeed = fSpeed;
        this.rotateSpeed = rSpeed;

        spriteRenderer.sprite = sprite;
        spriteRenderer.material.SetColor("_ChaColor", CharacterColors.GetColor(characterColor));

        transform.localScale = new Vector3(size, size, 1);
        spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, 32767, size);
    }

    void Update()
    {
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, rotateSpeed));
    }
}
