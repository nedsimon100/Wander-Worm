using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public AudioSource stretch, squash;
    private bool streching = false;
    private bool shrinking = false;
    private int StrechLoops = 0;
    public float strechSpeed = 0.3f;
    public Rigidbody2D rb;
    public TrailRenderer tr;
    private float maxLength=0;
    private bool colliding = false;

    public float strechMult = 8;

    public float minWidth = 0.4f;

    public GameObject Head, Tail;
    public bool Standable = true;
    public string BestDistKey;

    public bool gameOver = false;
    void Update()
    {
        tr.startWidth = this.transform.localScale.x;
        
        if (!shrinking && !streching)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
            maxLength = (mousePosition - rb.position).magnitude;
            //tr.Clear();
        }

        if (Input.GetMouseButtonDown(0)&&!shrinking)
        {
            squash.Stop();
            streching = true;
            stretch.Play();
            
        }
        if (Input.GetMouseButtonUp(0)&&Standable&&!shrinking)
        {
            stretch.Stop();
            shrinking = true;
            streching = false;
            StartCoroutine(Squash());
            squash.Play();

        }
        else if(Input.GetMouseButtonUp(0) && !Standable)
        {
            stretch.Stop();
            shrinking = true;
            streching = false;
            StartCoroutine(Retract());
            squash.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grow();
        }


        Head.transform.position = transform.position + (transform.up * transform.localScale.y/2);
        Head.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.x, 1);

        Tail.transform.position = transform.position - (transform.up * transform.localScale.y/2);
        Tail.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.x, 1);
    }
    private void FixedUpdate()
    {
        if (streching&& transform.localScale.x>minWidth)
        {
            Elongate();
        }
    }
    public IEnumerator Squash()
    {
        while (StrechLoops > 0)
        {
            StrechLoops--;
            yield return new WaitForSeconds(0.01f);
            this.transform.position += transform.up / 2 * strechSpeed;
            this.transform.localScale -= new Vector3(-strechSpeed/strechMult,strechSpeed,0);
        }
        shrinking = false;
        
    }
    public IEnumerator Retract()
    {
        while (StrechLoops > 0)
        {
            StrechLoops--;
            yield return new WaitForSeconds(0.01f);
            this.transform.position -= transform.up / 2 * strechSpeed;
            this.transform.localScale -= new Vector3(-strechSpeed / strechMult, strechSpeed, 0);
        }
        shrinking = false;
        
    }
    void Elongate()
    {
        if(this.transform.localScale.y < maxLength&&!colliding)
        {
            StrechLoops++;
            this.transform.position += transform.up / 2 * strechSpeed;
            this.transform.localScale += new Vector3(-strechSpeed / strechMult, strechSpeed, 0);
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        coll(collision);
    }
    public void coll(Collision2D collision)
    {
        colliding = true;
        if (collision.gameObject.tag == "Enemy")
        {
            
            Death();
            
        }
        else
        {
            StartCoroutine(Retract());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }
    void Grow()
    {
        transform.localScale += new Vector3(1,1,0);
    }
    public void Death()
    {
        if (Mathf.FloorToInt(this.transform.position.y) > PlayerPrefs.GetInt(BestDistKey, 0))
        {
            PlayerPrefs.SetInt(BestDistKey, Mathf.FloorToInt(this.transform.position.y));
            PlayerPrefs.Save();
            
        }
        gameOver = true;
    }
}
