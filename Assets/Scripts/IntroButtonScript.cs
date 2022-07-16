using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroButtonScript : MonoBehaviour
{
    public void NextLevel()
    {
        GameManagerScript.Instance.NextLevel();
    }
}
