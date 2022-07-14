using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float killTime = 2f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 9;
        var collider = gameObject.AddComponent<SphereCollider>();
        collider.center = Vector3.zero;
        collider.radius = 0.25f;
        //collider.isTrigger = true;

        Destroy(gameObject, killTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) return;

        //Debug.Log("AM COLLIDE!! " + collision.collider.name);

        if (collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemy))
        {
            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject, killTime);
    }
}
