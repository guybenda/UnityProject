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

        return Instantiate(viruses[selectedVirus], Vector3.zero, Quaternion.identity);
    }
}
