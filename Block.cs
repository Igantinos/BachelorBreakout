using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        collisionInfo.gameObject.GetComponent<BallBehavior>().upCount();
        // Destroy the whole Block
        Destroy(gameObject);
    }
}