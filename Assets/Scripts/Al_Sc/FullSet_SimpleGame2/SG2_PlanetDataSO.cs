using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Planet/Planet Data V2")]
public class SG2_PlanetDataSO : ScriptableObject
{
    public string planetId;
    public string planetName;

    [TextArea]
    public string description;

    // Data yang bisa di-reveal
    public List<PlanetFact> facts;

    // Jawaban sebenarnya
    public bool isHabitable;
}

[System.Serializable]
public class PlanetFact
{
    public string factId;
    [TextArea]
    public string factText;
    public bool revealedByDefault;
}
