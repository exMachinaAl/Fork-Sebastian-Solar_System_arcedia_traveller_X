
using UnityEngine;
using System.Collections.Generic;

public class SG1_PlanetRuntime
{
    public SG1_SO_Planet data;
    public bool answered;
}

public class SG1_GameFlowManager : MonoBehaviour
{
    public static SG1_GameFlowManager Instance;

    public List<SG1_SO_Planet> allPlanets = new();
    public List<SG1_PlanetRuntime> runtimePlanets = new();

    public int correctAnswer = 0;
    public int wrongAnswer = 0;

    void Awake()
    {
        Instance = this;

        foreach (var p in allPlanets)
        {
            runtimePlanets.Add(new SG1_PlanetRuntime
            {
                data = p,
                answered = false
            });
        }
    }

    public bool AllPlanetsFinished()
    {
        foreach (var rt in runtimePlanets)
            if (!rt.answered) return false;

        return true;
    }

    public void EvaluateAnswer(bool playerAnswer, bool correct)
    {
        if (playerAnswer == correct)
            correctAnswer++;
        else
            wrongAnswer++;
    }
}
