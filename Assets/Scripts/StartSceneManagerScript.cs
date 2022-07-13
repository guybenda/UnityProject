using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManagerScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectMenu;
    public GameObject viruses;

    public int currentVirus = 0;
    public const int maxVirus = 9;
    public const float virusGap = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            NextVirus();
        }

        if (Input.GetKey(KeyCode.A))
        {
            PrevVirus();
        }
    }

    void NextVirus()
    {
        currentVirus = (currentVirus + 1) % maxVirus;

        Vector3 currentPosition = viruses.transform.position;

        viruses.transform.GetChild(currentVirus).sc
    }

    void PrevVirus()
    {
        currentVirus = (currentVirus - 1) % maxVirus;
    }

    public void PressStart()
    {
        mainMenu.SetActive(false);
        selectMenu.SetActive(true);
    }

    public void PressExit()
    {
        Application.Quit();
    }
}
