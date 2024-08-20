using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private PlayerController player;
    public bool TouchingNonStandable = false;
    public bool TouchingStandable = false;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.coll(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("triggered");
        if(collision.gameObject.tag == "NonStandable")
        {
            TouchingNonStandable = true;
        }
        if(collision.gameObject.tag == "Standable")
        {
            TouchingStandable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NonStandable")
        {
            TouchingNonStandable = false;
        }
        if (collision.gameObject.tag == "Standable")
        {
            TouchingStandable = false;
        }
    }
    private void Update()
    {
        if (TouchingNonStandable && !TouchingStandable)
        {
            player.Standable = false;
        }
        else
        {
            player.Standable = true;
        }
    }
}
