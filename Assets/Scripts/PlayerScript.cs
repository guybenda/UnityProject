using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public GameObject virusModelContainer;
    public float shootVelocity = 30f;

    int tripleShotTimer = 0;
    int currentLevel = 0;
    bool wonOrDied = false;

    public int enemiesKilled = 0;
    public bool objective = false;

    InputAction m1;
    InputAction m2;
    InputAction backCamera;

    GameObject hud;

    Text healthText;
    Text equipmentText;
    Text objectiveText;
    RawImage backCameraImage;
    Camera minimapCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (virusModelContainer == null)
        {
            Debug.Log("NO VIRUS CONTAINER CONNECTED TO PLAYER!");
            return;
        }

        currentLevel = SceneManager.GetActiveScene().buildIndex;

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

        backCamera = new InputAction(binding: "<keyboard>/tab");
        // += _ => Shoot2();
        backCamera.Enable();

        hud = GameObject.FindGameObjectWithTag("HUD");
        healthText = GameObject.FindGameObjectWithTag("HUDHealth").GetComponent<Text>();
        objectiveText = GameObject.FindGameObjectWithTag("HUDObjective").GetComponent<Text>();
        minimapCamera = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<Camera>();
        backCameraImage = hud.GetComponentInChildren<RawImage>(true);
    }

    // Update is called once per frame
    void Update()
    {
        var cameraLocation = virusModelContainer.transform.position;
        switch (currentLevel)
        {
            case 1:
                //TODO
                break;
            case 2:
                cameraLocation.y = 150f;
                break;
            case 3:
                cameraLocation.y = 150f;
                break;
            default:
                break;
        }

        minimapCamera.transform.SetPositionAndRotation(cameraLocation, Quaternion.Euler(90f, 0, 0));

        healthText.text = $"\u2665 {health} / {maxHealth}";
        UpdateObjective();

        backCameraImage.enabled = backCamera.IsPressed();
    }

    void FixedUpdate()
    {
        tripleShotTimer = Mathf.Clamp(tripleShotTimer - 1, 0, 10000);
    }

    void OnDestroy()
    {
        m1.Disable();
        m2.Disable();
    }

    void Shoot()
    {
        if (tripleShotTimer > 0)
        {
            ShootProjectile((Camera.main.transform.forward + Camera.main.transform.right * -0.02f + Camera.main.transform.up * 0.12f) * shootVelocity + Random.insideUnitSphere * 1.6f);
            ShootProjectile((Camera.main.transform.forward + Camera.main.transform.right * 0.05f + Camera.main.transform.up * 0.18f) * shootVelocity + Random.insideUnitSphere * 1.6f);
            ShootProjectile((Camera.main.transform.forward + Camera.main.transform.right * 0.12f + Camera.main.transform.up * 0.12f) * shootVelocity + Random.insideUnitSphere * 1.6f);
        }
        else
        {
            var direction = (Camera.main.transform.forward + Camera.main.transform.right * 0.05f + Camera.main.transform.up * 0.15f) * shootVelocity + Random.insideUnitSphere * 1.2f;
            ShootProjectile(direction);
        }

    }

    void ShootProjectile(Vector3 direction)
    {
        GameObject proj = Instantiate(GameManagerScript.Instance.GetRandomVirus(), virusModelContainer.transform.position,
                                                     virusModelContainer.transform.rotation);
        proj.transform.localScale *= 0.3f;

        var bullet = proj.AddComponent<PlayerBulletScript>();
        bullet.damage = 20;

        var rigidbody = proj.AddComponent<Rigidbody>();
        rigidbody.AddTorque(Random.insideUnitSphere * 80f, ForceMode.VelocityChange);
        rigidbody.AddForce(direction, ForceMode.VelocityChange);
    }

    void Shoot2()
    {
        //TODO
    }

    public void Damage(int damage)
    {
        if (health <= 0) return;

        health -= damage;
        Debug.Log("OW! " + damage);

        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            StartCoroutine(HitRoutine());
        }
    }

    void Die()
    {
        if (wonOrDied) return;

        m1.Disable();
        m2.Disable();
        wonOrDied = true;
        StartCoroutine(DieRoutine());
    }

    public void Win()
    {
        if (wonOrDied) return;

        wonOrDied = true;
        StartCoroutine(VictoryRoutine());
    }

    public void AddPowerUp(PowerupType powerup)
    {
        switch (powerup)
        {
            case PowerupType.Tripleshot:
                tripleShotTimer = 600;
                break;
            case PowerupType.Health:
                break;
            default:
                break;
        }
    }

    public void UpdateObjective()
    {
        switch (currentLevel)
        {
            case 1:
                objectiveText.text = "Objective:\nEscape";
                return;
            case 2:
                objectiveText.text = $"Objective:\nKill 15 enemies - {15 - Mathf.Clamp(enemiesKilled, 0, 15)} left\nFind the power up in\nthe abandoned building";
                if (enemiesKilled >= 15 && objective) Win();
                return;
            default:
                break;
        }
    }

    IEnumerator DieRoutine()
    {
        Destroy(GameObject.FindGameObjectWithTag("HUDVictoryScreen"));
        Image deathScreen = GameObject.FindGameObjectWithTag("HUDDeathScreen").GetComponent<Image>();

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

        var texts1 = deathScreen.GetComponentsInChildren<Button>(true);

        texts1[0].onClick.AddListener(() => GameManagerScript.Instance.RestartLevel());
        texts1[1].onClick.AddListener(() => GameManagerScript.Instance.Quit());

        Destroy(gameObject);

        new GameObject("camera", typeof(Camera)).tag = "MainCamera";
    }

    IEnumerator VictoryRoutine()
    {
        Destroy(GameObject.FindGameObjectWithTag("HUDDeathScreen"));
        Image victoryScreen = GameObject.FindGameObjectWithTag("HUDVictoryScreen").GetComponent<Image>();

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        bool done = false;
        Color color = victoryScreen.color;

        while (!done)
        {
            color.a += 0.05f;
            victoryScreen.color = color;
            done = color.a >= 1;
            yield return new WaitForFixedUpdate();
        }

        var texts = victoryScreen.gameObject.GetComponentsInChildren<Text>(true);

        foreach (var text in texts)
        {
            yield return new WaitForSeconds(0.5f);
            text.gameObject.SetActive(true);
        }

        var texts2 = victoryScreen.GetComponentsInChildren<Button>(true);

        texts2[0].onClick.AddListener(() => GameManagerScript.Instance.NextLevel());
        texts2[1].onClick.AddListener(() => GameManagerScript.Instance.Quit());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Destroy(gameObject);

        new GameObject("camera", typeof(Camera)).tag = "MainCamera";
    }

    IEnumerator HitRoutine()
    {
        var image = hud.GetComponent<Image>();

        Color color = image.color;
        color.a += 0.30f;
        image.color = color;
        yield return new WaitForFixedUpdate();


        for (int i = 0; i < 10; i++)
        {
            Color newcolor = image.color;
            newcolor.a -= 0.03f;
            image.color = newcolor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
