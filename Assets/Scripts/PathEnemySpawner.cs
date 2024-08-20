using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEnemySpawner : MonoBehaviour
{
    public int SpawnSide;
    public float minSpawnSpeed = 2f, maxSpawnSpeed = 10f;
    public float EnemySpeed;
    private PlayerController player;
    public float spawnDist = 38f;
    public List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        SpawnSide = Random.Range(0, 2);
        EnemySpeed = 5 + Random.Range(-2f, this.transform.position.y / 50f);
        StartCoroutine(spawnTimer());

    }

    IEnumerator spawnTimer()
    {
        while (true)
        {
            
            if (SpawnSide == 0)
            {
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(-spawnDist, this.transform.position.y, 0), Quaternion.identity);
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(EnemySpeed, 0);
                enemy.GetComponent<AudioSource>().pitch = (EnemySpeed/2) - 1f;
            }
            if(SpawnSide == 1)
            {
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(spawnDist, this.transform.position.y, 0), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().flipX = true;
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-EnemySpeed, 0);
                enemy.GetComponent<AudioSource>().pitch = (EnemySpeed / 2) - 1f;
            }


            yield return new WaitForSeconds(Random.Range(minSpawnSpeed, maxSpawnSpeed));
        }
    }
    void Update()
    {
        if(this.transform.position.y - player.transform.position.y < -40)
        {
            Destroy(this.gameObject);
        }
    }
}
