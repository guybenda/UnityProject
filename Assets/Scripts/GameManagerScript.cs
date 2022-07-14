using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
    test
}

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public GameObject[] viruses;
    public int selectedVirus = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        viruses = Resources.LoadAll<GameObject>("Viruses");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetVirus()
    {
        if (selectedVirus == -1)
        {
            selectedVirus = Random.Range(0, viruses.Length);
        }

        return viruses[selectedVirus];
    }

    public GameObject GetRandomVirus()
    {
        return viruses[Random.Range(0, viruses.Length)];
    }

    public void RestartLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
    /*
    public void StartLevel1()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Level1Scene");
    }*/
}
