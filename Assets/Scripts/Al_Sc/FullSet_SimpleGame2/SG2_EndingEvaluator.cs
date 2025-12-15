using UnityEngine;
using System.Linq;

public class SG2_EndingEvaluator : MonoBehaviour
{
    public void Evaluate()
    {
        var results = SG2_PlanetManager.Instance.GetResults();

        int correct = results.Count(p =>
            p.playerDecision.HasValue &&
            p.playerDecision.Value == p.data.isHabitable);

        Debug.Log("Correct: " + correct + "/" + results.Count);
        An_ScreenFade.Instance.FadeIn(new Color(0f, 0f, 0f), 5f); // Ending Game

        if (correct >= results.Count * 0.7f)
            Debug.Log("[Ending] GOOD ENDING");
        else
            Debug.Log("[Ending] BAD ENDING");
    }

}
