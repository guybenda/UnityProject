using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1ColliderWallScript : MonoBehaviour
{
    public int health = 30;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            health--;
            if (health <= 0) Destroy(gameObject);
        }
    }
}
