using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text;

public class UI_PlayerInTheInformation : MonoBehaviour
{
    public TMP_Text tmpInf;  // Drag Scroll Rect ke sini di Inspector

    public static UI_PlayerInTheInformation Instance;
    public static Action RefreshPIH;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        RefreshPIH += Refresh;
    }
    void OnDisable()
    {
        RefreshPIH -= Refresh;
    }


    public void Refresh()
    {
        var runtimePlanets = SG2_PlanetManager.Instance.GetResults();
        var currRuntimePla = SG2_PlanetManager.Instance.GetCurrentRuntime();

        StringBuilder sb = new();

        // sb.AppendLine(runtime.data.description);

        foreach (var planet in runtimePlanets)
        {
            // bool revealed = runtime.revealedFacts[fact.factId];
            bool alreadyAnswered = planet.playerDecision.HasValue;
            bool isCurrent = planet == currRuntimePla;

            sb.AppendLine(
                $"{planet.data.planetName} [ {(alreadyAnswered ? "+" : " ")} ] {(isCurrent ? "<=" : "")}"
            );

            sb.AppendLine("");
            // sb.AppendLine(revealed ? fact.factText : "missing information ???");
        }

        tmpInf.text = sb.ToString();
    }
}
