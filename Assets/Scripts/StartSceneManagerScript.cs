using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManagerScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectMenu;
    public GameObject viruses;

    public int currentVirus = 0;
    public const int maxVirus = 10;
    public const float virusGap = 1.5f;

    public float smoothTime = 0.001F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 originalPosition = Vector3.zero;

    private GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerScript>();

        targetPosition = viruses.transform.position;
        originalPosition = viruses.transform.position;

        viruses.transform.GetChild(currentVirus).localScale += Vector3.one * 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            NextVirus();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PrevVirus();
        }

        Vector3 currentPosition = viruses.transform.position;

        viruses.transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);

    }

    void NextVirus()
    {
        viruses.transform.GetChild(currentVirus).localScale -= Vector3.one * 0.4f;

        currentVirus = (currentVirus + 1) % maxVirus;
        Vector3 direction = -viruses.transform.right;
        targetPosition = originalPosition + currentVirus * virusGap * direction;

        viruses.transform.GetChild(currentVirus).localScale += Vector3.one * 0.4f;
    }

    void PrevVirus()
    {
        viruses.transform.GetChild(currentVirus).localScale -= Vector3.one * 0.4f;

        currentVirus = (currentVirus + maxVirus - 1) % maxVirus;
        Vector3 direction = -viruses.transform.right;
        targetPosition = originalPosition + currentVirus * virusGap * direction;

        viruses.transform.GetChild(currentVirus).localScale += Vector3.one * 0.4f;
    }

    public void PressStart()
    {
        mainMenu.SetActive(false);
        selectMenu.SetActive(true);
        viruses.SetActive(true);

    }

    public void PressBegin()
    {
        gameManager.selectedVirus = currentVirus;
        SceneManager.LoadScene("Level1Scene");
    }

    public void PressBack()
    {
        mainMenu.SetActive(true);
        selectMenu.SetActive(false);
        viruses.SetActive(false);

    }

    public void PressExit()
    {
        Application.Quit();
    }
}
