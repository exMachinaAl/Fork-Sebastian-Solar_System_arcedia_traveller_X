using UnityEngine;

public class PlanetTriggerPlacer : MonoBehaviour
{
    public Transform planetCenter;
    public GameObject triggerPrefab;
    public int count = 10;
    public float surfaceOffset = 0.15f;
    public LayerMask terrainLayer;

    void OnEnable()
    {
        PlanetReadyWatcher.OnPlanetReady += Generate;
    }

    void OnDisable()
    {
        PlanetReadyWatcher.OnPlanetReady -= Generate;
    }

    void Generate()
    {
        for (int i = 0; i < count; i++)
        {
            PlaceOne();
        }
    }

    void PlaceOne()
    {
        Vector3 dir = Random.onUnitSphere;
        Vector3 rayOrigin = planetCenter.position + dir * 5000f;

        if (Physics.Raycast(
            rayOrigin,
            -dir,
            out RaycastHit hit,
            10000f,
            terrainLayer,
            QueryTriggerInteraction.Ignore))
        {
            // GameObject t = Instantiate(triggerPrefab, transform);
            GameObject t = gameObject;

            t.transform.position =
                hit.point + hit.normal * surfaceOffset;

            t.transform.up = hit.normal;

            // LOCK
            t.isStatic = true;
        }
    }
}
