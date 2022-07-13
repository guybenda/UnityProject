using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    test
}

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] viruses;

    // Start is called before the first frame update
    void Start()
    {
        viruses = Resources.LoadAll<GameObject>("Viruses");
        Debug.Log(viruses.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
