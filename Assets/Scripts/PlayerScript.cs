using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public GameObject virusModelContainer;
    public float shootVelocity = 30f;
    //GameObject projectile;

    InputAction m1;
    InputAction m2;

    // Start is called before the first frame update
    void Start()
    {
        if (virusModelContainer == null)
        {
            Debug.Log("NO VIRUS CONTAINER CONNECTED TO PLAYER!");
            return;
        }

        Instantiate(GameManagerScript.Instance.GetVirus()).transform.parent = virusModelContainer.transform;
        virusModelContainer.transform.GetChild(0).transform.localPosition = Vector3.zero;
        virusModelContainer.transform.GetChild(0).transform.localRotation = Quaternion.identity;

        //projectile = GameManagerScript.Instance.GetVirus();

        m1 = new InputAction(binding: "<Mouse>/leftButton");
        m1.performed += _ => Shoot();
        m1.Enable();

        m2 = new InputAction(binding: "<Mouse>/rightButton");
        m2.performed += _ => Shoot2();
        m2.Enable();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        m1.Disable();
        m2.Disable();
    }

    void Shoot()
    {
        GameObject proj = Instantiate(GameManagerScript.Instance.GetRandomVirus(), virusModelContainer.transform.position,
                                                     virusModelContainer.transform.rotation);
        proj.transform.localScale *= 0.3f;

        var bullet = proj.AddComponent<PlayerBulletScript>();
        bullet.damage = 20;

        var direction = (Camera.main.transform.forward  + Camera.main.transform.right * 0.05f + Camera.main.transform.up * 0.15f) * shootVelocity;
        var rigidbody = proj.AddComponent<Rigidbody>();
        rigidbody.AddTorque(Random.insideUnitSphere * 80f, ForceMode.VelocityChange);
        rigidbody.AddForce(direction + Random.insideUnitSphere * 0.8f, ForceMode.VelocityChange);

    }

    void Shoot2()
    {
        //TODO
    }

    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("OW! " + damage);
        //TODO die
    }
}
