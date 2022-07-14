using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public GameObject virusModelContainer;
    public float shootVelocity = 30f;
    //GameObject projectile;

    InputAction m1;
    InputAction m2;

    GameObject hud;

    Text healthText;
    Text equipmentText;
    Image deathScreen;

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

        hud = GameObject.FindGameObjectWithTag("HUD");
        healthText = GameObject.FindGameObjectWithTag("HUDHealth").GetComponent<Text>();
        deathScreen = GameObject.FindGameObjectWithTag("HUDDeathScreen").GetComponent<Image>();

        var texts = deathScreen.GetComponentsInChildren<Button>(true);

        texts[0].onClick.AddListener(() => GameManagerScript.Instance.RestartLevel());
        texts[1].onClick.AddListener(() => GameManagerScript.Instance.Quit());

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"\u2665 {health} / {maxHealth}";
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

        var direction = (Camera.main.transform.forward + Camera.main.transform.right * 0.05f + Camera.main.transform.up * 0.15f) * shootVelocity;
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

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        m1.Disable();
        m2.Disable();

        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        bool done = false;
        Color color = deathScreen.color;

        while (!done)
        {
            color.a += 0.02f;
            deathScreen.color = color;
            done = color.a >= 1;
            yield return new WaitForFixedUpdate();
        }

        var texts = deathScreen.gameObject.GetComponentsInChildren<Text>(true);

        foreach (var text in texts)
        {
            yield return new WaitForSeconds(0.5f);
            text.gameObject.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        Destroy(gameObject);

        new GameObject("camera", typeof(Camera)).tag = "MainCamera";
    }
}
