using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManagerScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selectMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
