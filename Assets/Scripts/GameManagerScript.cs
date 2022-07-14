using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    test
}

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public GameObject[] viruses;
    public int selectedVirus;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        viruses = Resources.LoadAll<GameObject>("Viruses");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
