using UnityEngine;

public class FogDisableScript : MonoBehaviour
{
    bool enableFog = false;
    bool previousFogState;

    void OnPreRender()
    {
        previousFogState = RenderSettings.fog;
        RenderSettings.fog = enableFog;
    }
    void OnPostRender()
    {
        RenderSettings.fog = previousFogState;
    }

}