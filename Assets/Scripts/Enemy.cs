using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    public Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyKillBox")
        {
            Destroy(this.gameObject);
        }
    }
  //  private void Start()
  //  {
  //      rb = this.GetComponent<Rigidbody2D>();
  //      speed = rb.velocity.x;
  //  }
  //
  //  private void Update()
  //  {
  //      rb.velocity = new Vector2(speed, 0);
  //  }
}
