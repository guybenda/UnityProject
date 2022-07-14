using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public GameObject virusModelContainer;

    // Start is called before the first frame update
    void Start()
    {
        if (virusModelContainer == null)
        {
            Debug.Log("NO VIRUS CONTAINER CONNECTED TO PLAYER!");
            return;
        }

        GameManagerScript.Instance.GetVirus().transform.parent = virusModelContainer.transform;
        virusModelContainer.transform.GetChild(0).transform.localPosition = Vector3.zero;
        virusModelContainer.transform.GetChild(0).transform.localRotation = Quaternion.identity;

        var action = new InputAction(binding: "<Mouse>/leftButton");
        action.performed += _ => Shoot();

        action.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot()
    {
        //TODO
        Debug.Log("Shoot!");
    }
}
