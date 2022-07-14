using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public GameObject virusModelContainer;
    public float shootVelocity = 12f;
    GameObject projectile;

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

        projectile = GameManagerScript.Instance.GetVirus();
        projectile.transform.localScale *= 0.2f;

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
        //TODO
        Debug.Log("Shoot!");

        GameObject proj = Instantiate(projectile, virusModelContainer.transform.position,
                                                     virusModelContainer.transform.rotation);
        proj.AddComponent<PlayerBulletScript>();
        proj.AddComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shootVelocity, ForceMode.VelocityChange);

        // Camera.main.transform.rotation.

    }

    void Shoot2()
    {
        //TODO
        Debug.Log("Shoot 2!");
    }
}
