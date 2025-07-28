using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    private PlayerController player;
    public float playerMaxDist = 0;
    public List<GameObject> Hazards = new List<GameObject>();
    public float SpawnDist = 25;
    public int intensity = 1;
    public float ClosestSpawn = 25;
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMaxDist < player.transform.position.y)
        {
            playerMaxDist = player.transform.position.y;
        }
        if (SpawnDist > ClosestSpawn - playerMaxDist)
        {
            intensity = Mathf.CeilToInt(playerMaxDist / 50f);
            buildChunk();
        }
    }
    public void buildChunk()
    {
        //Debug.Log("Build");
        if (intensity > 4)
        {
            ClosestSpawn += Random.Range(0, 10+(5/intensity));

        }
        else
        {
            ClosestSpawn += Random.Range(5, 20);

        }
        GameObject HazardToRepeat = Hazards[Random.Range(0, Hazards.Count)];
        for (int i = 0; i < Random.Range(1,intensity+1); i++)
        {
            GameObject hazard = Instantiate(HazardToRepeat, new Vector3(0, ClosestSpawn, 0), Quaternion.identity);
            ClosestSpawn = hazard.transform.position.y+hazard.transform.localScale.y;
        }
            
    }
}
