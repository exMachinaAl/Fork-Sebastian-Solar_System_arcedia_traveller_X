using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG2_PlanetColliderEnter : MonoBehaviour
{
    // public string planetId;
    public SG2_PlanetDataSO planet;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ship")) return;

        Debug.Log("[Quest] Player spaceShip menyentuh planet");

        // foreach (var o in GetComponentsInChildren<Outline>())
        //     o.enabled = true;

        SG2_PlanetManager.Instance.FocusPlanet(planet.planetId);
        // SG2_PlanetManager.Instance.FocusPlanet(planetId);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ship")) return;

        foreach (var o in transform.parent.GetComponentsInChildren<Outline>())
            o.enabled = false;

        // UI_NavigatorSystem.Instance.ClearAll();
        SG2_PlanetManager.Instance.ClearCurrentPlanet();
        UI_PlayerInTheInformation.RefreshPIH?.Invoke();
    }
}
