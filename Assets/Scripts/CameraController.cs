using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController Player;
    void Start()
    {
        Player = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0, Player.Head.transform.position.y + 3, -10);
        Camera.main.orthographicSize = 10+((Player.Head.transform.position - Player.Tail.transform.position).magnitude/1.5f);
    }
}
