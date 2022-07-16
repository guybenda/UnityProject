using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum TutorialState
{
    start,
    walked,
    shot,
    collected
}

public class TutorialScript : MonoBehaviour
{
    TutorialState state = TutorialState.start;
    public GameObject walkText;
    public GameObject shotText;
    public GameObject collectedText;
    public GameObject powerup;

    // Start is called before the first frame update
    void Start()
    {
        walkText.SetActive(true);
        shotText.SetActive(false);
        collectedText.SetActive(false);
        powerup.SetActive(false);

        var moved = new InputAction(binding: "<keyboard>/w");
        moved.performed += _ =>
        {
            moved.Disable();
            StartCoroutine(Walked());
        };
        moved.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Walked()
    {
        yield return new WaitForSeconds(2f);

        walkText.SetActive(false);
        shotText.SetActive(true);

        var shot = new InputAction(binding: "<mouse>/leftButton");
        shot.performed += _ =>
        {
            shot.Disable();
            StartCoroutine(Shot());
        };
        shot.Enable();
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(5f);

        shotText.SetActive(false);
        collectedText.SetActive(true);
        powerup.SetActive(true);

        var player = GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>();
        while(player.tripleShotTimer <= 0)
        {
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(Collected());
    }

    IEnumerator Collected()
    {
        yield return new WaitForSeconds(1f);

        var shot = new InputAction(binding: "<mouse>/leftButton");
        shot.performed += _ =>
        {
            shot.Disable();
            StartCoroutine(Win());
        };
        shot.Enable();
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);

        GameObject.FindGameObjectWithTag("PlayerContainer").GetComponent<PlayerScript>().Win();
    }
}
