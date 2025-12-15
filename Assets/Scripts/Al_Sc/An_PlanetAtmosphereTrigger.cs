using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class An_PlanetAtmosphereTrigger : MonoBehaviour
{
    [Header("Atmosphere Settings")]
    public Color atmosphereColor = new Color(0.4f, 0.65f, 1f);
    public float transitionTime = 2.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ship")) return;

        An_CameraAtmosphereController.Instance
            ?.SetAtmosphere(atmosphereColor, transitionTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ship")) return;

        An_CameraAtmosphereController.Instance
            ?.ResetToSpace(transitionTime);
    }
}
