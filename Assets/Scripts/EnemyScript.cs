using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    attacking
}

public class EnemyScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            return true;
        }

        return false;
    }
}
