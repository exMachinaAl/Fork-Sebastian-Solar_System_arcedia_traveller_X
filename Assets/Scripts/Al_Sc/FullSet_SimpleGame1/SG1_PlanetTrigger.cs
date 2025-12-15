using UnityEngine;

public class SG1_PlanetTrigger : MonoBehaviour
{
    public SG1_SO_Planet planet;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SG1_PlanetManager.Instance.SetCurrentPlanet(planet);
    }
}
