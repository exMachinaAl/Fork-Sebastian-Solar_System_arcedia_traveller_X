using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class SG2_PlanetManager : MonoBehaviour
{
    public static SG2_PlanetManager Instance;

    public List<SG2_PlanetDataSO> allPlanets;

    public CelestialBody celestialBody;

    [SerializeField]private List<SG2_PlanetRuntime> runtimePlanets = new();
    [SerializeField]private SG2_PlanetRuntime currentPlanet;

    void Awake()
    {
        Instance = this;
        GeneratePlanets();
    }

    void Start()
    {
        foreach (var cb in FindObjectsOfType<CelestialBody>())
        {
            cb.SetUpQuestSystemEvent();
            // if (t == player) continue;
            // if (t.parent != null) continue; // hanya root world objects
            // sceneObjects.Add(t);
        }
        // celestialBody.SetUpQuestSystemEvent();
        
        UI_PlayerInTheInformation.RefreshPIH?.Invoke();
    }


    void GeneratePlanets()
    {
        runtimePlanets.Clear();

        if (allPlanets != null && allPlanets.Count > 0)
        {
            foreach (var p in allPlanets)
            {
                runtimePlanets.Add(new SG2_PlanetRuntime(p));
            }

            // Debug log untuk memverifikasi
            Debug.Log($"Generated {runtimePlanets.Count} planets.");
        }
        else
        {
            Debug.LogWarning("All planets list is empty. Ensure allPlanets contains valid data.");
        }
    }

    // void GeneratePlanets()
    // {
    //     runtimePlanets.Clear();

    //     foreach (var p in allPlanets)
    //         runtimePlanets.Add(new SG2_PlanetRuntime(p));
    // }

    // public void FocusPlanet(string planetId)
    // {
    //     currentPlanet = runtimePlanets.Find(p => p.data.planetId == planetId);
    //     SG2_UI_PlanetPanel.Instance.ShowPlanet(currentPlanet);
    //     Game_TriggerEventStatic.OutlineActive?.Invoke();
    // }

    public void FocusPlanet(string planetId)
    {
        currentPlanet = runtimePlanets
            .FirstOrDefault(p => p.data.planetId == planetId);

        if (currentPlanet == null)
        {
            Debug.LogWarning($"Planet dengan id {planetId} tidak ditemukan");
            return;
        }

        SG2_UI_PlanetPanel.Instance.ShowPlanet(currentPlanet);
        Game_TriggerEventStatic.OutlineActive?.Invoke();
        UI_PlayerInTheInformation.RefreshPIH?.Invoke();

        UI_NavigatorSystem.Instance.ClearAll();

        foreach (var reveal in FindObjectsOfType<UI_NavigatorTarget>())
        {
            if (currentPlanet.revealedFacts.ContainsKey(reveal.factId))
                reveal.ShowNavigator();
        }
    }



    // public void RevealFact(string factId)
    // {
    //     if (currentPlanet == null) return;

    //     if (currentPlanet.revealedFacts.ContainsKey(factId))
    //     {
    //         currentPlanet.revealedFacts[factId] = true;
    //         SG2_UI_PlanetPanel.Instance.Refresh();
    //     }
    // }
    
    public void ClearCurrentPlanet()
    {
        currentPlanet = null;

        // bersihin UI & sistem terkait
        // SG2_UI_PlanetPanel.Instance.HideUI(); // atau Close()
        UI_NavigatorSystem.Instance.ClearAll();

        // Game_TriggerEventStatic.OutlineDeactive?.Invoke();
    }


    public void RevealFact(string factId)
    {
        if (currentPlanet == null) return;

        // if (!currentPlanet.revealedFacts.ContainsKey(factId)) return;

        // currentPlanet.revealedFacts[factId] = true;
        if (currentPlanet.revealedFacts.ContainsKey(factId))
        {
            currentPlanet.revealedFacts[factId] = true;
            SG2_UI_PlanetPanel.Instance.Refresh();
        }

        var target = FindObjectsOfType<UI_NavigatorTarget>()
            .FirstOrDefault(t => t.factId == factId);

        if (target != null)
            target.ShowNavigator();
        // target.EnableNavigator();
    }

    // void RevealFact(string factId)
    // {
    //     if (!currentPlanet.revealedFacts.ContainsKey(factId)) return;

    //     var target = FindObjectsOfType<UI_NavigatorTarget>()
    //         .FirstOrDefault(t => t.factId == factId);

    //     if (target != null)
    //         target.ShowNavigator();
    // }



    public void MakeDecision(bool decision)
    {
        if (currentPlanet == null)
            return;

        currentPlanet.playerDecision = decision;

        UI_PlayerInTheInformation.RefreshPIH?.Invoke();

        // 🔴 INI KUNCINYA
        if (AreAllPlanetsAnswered())
        {
            TriggerEnding();
        }
    }

    bool AreAllPlanetsAnswered()
    {
        return runtimePlanets.All(p => p.playerDecision.HasValue);
    }

    void TriggerEnding()
    {
        Debug.Log("ALL PLANETS ANSWERED → EVALUATE ENDING");

        FindObjectOfType<SG2_EndingEvaluator>().Evaluate();
    }

    // public bool GetCurrentRuntime(string factId)
    // {
    //     return currentPlanet.revealedFacts.ContainsKey(factId) ? true : false;
    // }
    public SG2_PlanetRuntime GetCurrentRuntime() => currentPlanet;
    public bool IsRightPlanetFact(string factId)
    {
        if (currentPlanet == null)
            return false;

        return currentPlanet.revealedFacts.ContainsKey(factId);
    }


    public List<SG2_PlanetRuntime> GetResults() => runtimePlanets;
}
