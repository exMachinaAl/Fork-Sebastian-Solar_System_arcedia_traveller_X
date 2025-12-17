using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (Rigidbody))]
public class CelestialBody : GravityObject {

    public enum BodyType { Planet, Moon, Sun }
    public BodyType bodyType;
    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    Transform meshHolder;

    [Header("Al_custom")]
    public SO_QuestCreator PlanetStyQuest;
    public SG2_PlanetDataSO planetQInformation;
    public Transform rootSetFocusQuestTrigger;
    public GameObject prefabFocusQuestTrigger;
    public int offsetRadiusTrigger = 70;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    Rigidbody rb;

    void Awake () {

        rb = GetComponent<Rigidbody> ();
        velocity = initialVelocity;
        RecalculateMass ();
    }

    public void SetUpQuestSystemEvent()
    {
        if (radius == 0f || prefabFocusQuestTrigger == null || rootSetFocusQuestTrigger == null)
        {
            Debug.Log("Set up dulu semua kebutuhan prefab dan lain-lain");
            return;
        }

        // Instantiate prefabFocusQuestTrigger ke rootSetFocusQuestTrigger
        GameObject qstColl = Instantiate(prefabFocusQuestTrigger, rootSetFocusQuestTrigger);

        // Pastikan komponen SphereCollider ada dan atur radius
        SphereCollider collider = qstColl.GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.radius = radius + offsetRadiusTrigger;
        }
        else
        {
            Debug.LogWarning("SphereCollider tidak ditemukan pada prefab!");
        }

        qstColl.GetComponent<SG2_PlanetColliderEnter>().planet = planetQInformation;
        qstColl.GetComponent<SG2_PlanetColliderEnter>().InitStoryPlanet(PlanetStyQuest);
    }


    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach (var otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;

                Vector3 acceleration = forceDir * Universe.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }

    void OnValidate () {
        RecalculateMass ();
        if (GetComponentInChildren<CelestialBodyGenerator> ()) {
            GetComponentInChildren<CelestialBodyGenerator> ().transform.localScale = Vector3.one * radius;
        }
        gameObject.name = bodyName;
    }

    public void RecalculateMass () {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
        Rigidbody.mass = mass;
    }

    public Rigidbody Rigidbody {
        get {
            if (!rb) {
                rb = GetComponent<Rigidbody> ();
            }
            return rb;
        }
    }

    public Vector3 Position {
        get {
            return rb.position;
        }
    }

}