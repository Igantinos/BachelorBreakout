using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class BallBehavior : MonoBehaviour
{
    // Movement Speed
    public float speed = 100.0f;
    public GameObject racket;
    private bool move;
    public float maxSpeed = 100f;//Replace with your max speed
    private Rigidbody2D riggy;
    public GameObject ball;
    public string txtName;
    private string lastHit = "Start" + DateTime.Now.ToShortTimeString();
    private int count = 0;
    // Use this for initialization
    public void Start()
    {
        Physics2D.IgnoreCollision(ball.GetComponent  <Collider2D>(), GetComponent<Collider2D>());
        move = false;
        riggy = GetComponent<Rigidbody2D>();
        riggy.velocity = Vector2.up * speed;

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.name == "racket")
        {
            // Calculate hit Factor
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            riggy.velocity = dir * speed;
            stats();
            lastHit = col.gameObject.tag;

        }
    }
    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }
    private void Update()
    {
        if (transform.position.y <= -160f)
        {
            transform.position = new Vector3(racket.transform.position.x, -82.9f, racket.transform.position.z);
            move = false;
            dropped();


        }
        if (!move)
        {
            transform.position = new Vector3(racket.transform.position.x, -82.9f, 0f);
        }
        if (Input.GetKeyDown("space") && move == false)
        {
            move = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }
    } 
    private void FixedUpdate()
    {
        if (Mathf.Abs(riggy.velocity.x)>40)
        {
            if(riggy.velocity.x >0)
                riggy.velocity = new Vector2(40, riggy.velocity.y);
            else
                riggy.velocity = new Vector2(-40, riggy.velocity.y);
        }
    }
    private void stats()
    {
        string path = @"C:\temp\"+ txtName+".txt";
        if (!File.Exists(path))
        {
            File.Create(path);
            TextWriter tw = new StreamWriter(path);
            tw.WriteLine(txtName + " hit " + count + " bricks from " + lastHit);
            tw.Close();
            count = 0;
        }
        else if (File.Exists(path))
        {
            using(var tw = new StreamWriter(path,true))
            tw.WriteLine(txtName + " hit " + count + " bricks from " + lastHit);
            count = 0;
        }
    }
    public void upCount()
    {
        count++;
    }
    private void dropped()
    {
        string path = @"C:\temp\" + txtName + ".txt";
        if (!File.Exists(path))
        {
            File.Create(path);
            TextWriter tw = new StreamWriter(path);
            tw.WriteLine(txtName+ "was dropped by " + lastHit + " after hitting  " + count);
            tw.Close();
            count = 0;
        }
        else if (File.Exists(path))
        {
            using (var tw = new StreamWriter(path, true))
                tw.WriteLine(txtName + "was dropped by " + lastHit + " after hitting  " + count);
            count = 0;
        }
    }

}