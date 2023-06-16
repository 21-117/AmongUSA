using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewStats = new bool[12];
    private float timer = 0.5f;
    private float distance = 800;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingcrew((ChaColors)i, Random.Range(0f, distance));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnFloatingcrew((ChaColors)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    public void SpawnFloatingcrew(ChaColors chaColor, float dist)
    {
        if (!crewStats[(int)chaColor])
        {
            crewStats[(int)chaColor] = true;

            float angle = Random.Range(0f, 360f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;
            Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            float fSpeed = Random.Range(50f, 200f);
            float rSpeed = Random.Range(-2f, 2f);

            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCharacter>();
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], chaColor, dir, fSpeed, rSpeed, Random.Range(25f, 75f));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCharacter>();
        if(crew != null)
        {
            crewStats[(int)crew.chaColor] = false;
            Destroy(crew.gameObject);
        }   
    }
}
