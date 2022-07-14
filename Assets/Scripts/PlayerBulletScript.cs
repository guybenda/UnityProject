using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float killTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, killTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject, killTime);
    }
}
