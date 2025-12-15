using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class addComponentToChild : MonoBehaviour
{
    [Header("Settings")]
    public PlanetOrbit ScriptName; 
    public float timeToAddSc = 1.5f; 
    void Start()
    {
        StartCoroutine(StartIn(timeToAddSc));
    }

    IEnumerator StartIn(float timeD)
    {
        yield return new WaitForSeconds(timeD);

        transform.Find("Body Generator").gameObject.AddComponent<PlanetOrbit>();
    }
}
