using UnityEngine;

[CreateAssetMenu(fileName = "PlanetReason_", menuName = "Game/PlanetReason")]
public class SG2_SOPlanetReason : ScriptableObject
{
    public string planetId;            // ID planet
    [TextArea]public string correctExplanation;  // Penjelasan jika pemain benar
    [TextArea]public string incorrectExplanation; // Penjelasan jika pemain salah
}
