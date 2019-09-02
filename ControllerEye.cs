using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class ControllerEye : MonoBehaviour {
    public float speed = 150;
    public GameObject racket;
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(racket.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        float h = 0;
        float hitty = 0;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(TobiiAPI.GetGazePoint().Screen);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100000))
        {
            if (hit.point.x > 0)
                hitty = hit.point.x;
            else
                hitty = hit.point.x-2;



            if (Mathf.Abs((hitty - this.transform.position.x)) > 20)
            {
                speed = 150;
                if ((hitty - this.transform.position.x) > 0) 
                    h = 1;

                else
                    h = -1;
            }
            else {
                speed = (Mathf.Abs(hitty - this.transform.position.x))*6;
                    //Debug.Log(Mathf.Abs(hitty - this.transform.position.x));
                if ((hitty - this.transform.position.x) > 0)
                    h = 1;

                else
                    h = -1;
            }
        }

        //float h = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
    }
}
