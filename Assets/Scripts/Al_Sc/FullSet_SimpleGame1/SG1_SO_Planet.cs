using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Planet_", menuName = "Planet/New Planet")]
public class SG1_SO_Planet : ScriptableObject
{
    public string planetId;
    public string planetName;

    [TextArea] public string planetSummary;

    // Data yang akan di-reveal satu per satu
    [TextArea] public List<string> revealData = new List<string>();

    // Truth table
    public bool isHabitable;  // Jawaban sebenarnya
}
