using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerD : MonoBehaviour {
    public float speed= 150;
    public GameObject racket;
	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(racket.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = 0;
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Ray ray;
        ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100000))
        {
            Debug.Log(hit.point.x + "  " + this.transform.position.x);

            if (Mathf.Abs((hit.point.x - this.transform.position.x)) > 3.5)
            {
                if ((hit.point.x - this.transform.position.x) > 0)
                    h = 1;
                else
                    h = -1;
            }
            else
                h = 0;
        }
            
        //float h = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
    }

}
