using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outofbounds : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ball") {
            other.transform.position.Set(0f, -82.9f, 0f);
            other.GetComponent<BallBehavior>().Start();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
