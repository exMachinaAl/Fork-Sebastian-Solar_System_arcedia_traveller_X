using UnityEngine;
using System.Collections;

public class SG1_PlanetManager : MonoBehaviour
{
    public static SG1_PlanetManager Instance;

    private SG1_PlanetRuntime currentPlanet;

    void Awake() => Instance = this;

    public void SetCurrentPlanet(SG1_SO_Planet planet)
    {
        // Cari runtime planet
        currentPlanet = SG1_GameFlowManager.Instance.runtimePlanets
            .Find(p => p.data == planet);

        SG1_UI_PlanetPanel.Instance.ShowOverview(planet);
    }

    // Dipanggil setelah player menekan tombol "Analyze"
    public void StartRevealSequence()
    {
        StartCoroutine(RevealRoutine());
    }

    IEnumerator RevealRoutine()
    {
        var data = currentPlanet.data.revealData;

        foreach (var info in data)
        {
            bool waiting = true;
            SG1_UI_RevealPanel.Instance.Show(info, () => waiting = false);

            yield return new WaitUntil(() => waiting == false);
        }

        AskHabitability();
    }

    void AskHabitability()
    {
        SG1_UI_YesNoPrompt.Instance.Show(
            "Apakah planet ini layak huni?",
            onYes: () => OnPlayerAnswer(true),
            onNo: ()  => OnPlayerAnswer(false)
        );
    }

    void OnPlayerAnswer(bool answer)
    {
        var truth = currentPlanet.data.isHabitable;

        SG1_GameFlowManager.Instance.EvaluateAnswer(answer, truth);

        currentPlanet.answered = true;

        if (SG1_GameFlowManager.Instance.AllPlanetsFinished())
        {
            SG1_UI_EndingPanel.Instance.ShowEnding();
        }
        else
        {
            SG1_UI_PlanetPanel.Instance.ShowNextPlanetMessage();
        }
    }
}
