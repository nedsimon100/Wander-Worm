using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> Objs = new List<GameObject>();
    public int MinObjs = 7;
    public int MaxObjs = 15;
    void Start()
    {
        int ObjsToSpawn = Random.Range(MinObjs, MaxObjs);
        for(int i = 0; i < ObjsToSpawn; i++)
        {
            Instantiate(Objs[Random.Range(0, Objs.Count)], new Vector3(Random.Range(-20f,20f),Random.Range(this.transform.position.y-(this.transform.localScale.y/2)+0.5f, this.transform.position.y + (this.transform.localScale.y / 2) - 0.5f),0), Quaternion.identity);
        }
    }

}
