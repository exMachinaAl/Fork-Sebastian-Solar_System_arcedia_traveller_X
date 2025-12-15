using System.Collections.Generic;

[System.Serializable]
public class SG2_PlanetRuntime
{
    public SG2_PlanetDataSO data;

    public Dictionary<string, bool> revealedFacts = new();
    public bool? playerDecision = null; // null = belum jawab

    public SG2_PlanetRuntime(SG2_PlanetDataSO data)
    {
        this.data = data;

        foreach (var fact in data.facts)
            revealedFacts[fact.factId] = fact.revealedByDefault;
    }
}
