using UnityEngine;
using System.Collections;

public class PlanetReadyWatcher : MonoBehaviour
{
    public Transform planetRoot; // generator lama
    public float checkInterval = 0.2f;

    public static event System.Action OnPlanetReady;

    void Start()
    {
        StartCoroutine(CheckReady());
    }

    IEnumerator CheckReady()
    {
        GameObject meshPlanet;
        bool planetReady = false;
        while (!planetReady)
        {

            yield return new WaitForSeconds(checkInterval);
            GameObject bodyGen = planetRoot.Find("Body Generator").gameObject;
            if (bodyGen != null)
            {
                meshPlanet = bodyGen.transform.Find("Terrain Mesh").gameObject;
                if (meshPlanet != null)
                {
                    planetReady = true;
                }
            }

        }

        // Tunggu collider settle
        yield return new WaitForEndOfFrame();
        yield return new WaitForFixedUpdate();

        OnPlanetReady?.Invoke();
    }
}
