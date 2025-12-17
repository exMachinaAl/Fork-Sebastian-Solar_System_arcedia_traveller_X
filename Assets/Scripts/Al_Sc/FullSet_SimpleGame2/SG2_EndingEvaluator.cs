using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SG2_EndingEvaluator : MonoBehaviour
{
    public SG2_PlanetManager planetManager; // Menyambungkan ke Planet Manager
    public SG2_EndingUI endingUI;           // Referensi ke Ending UI
    public List<SG2_SOPlanetReason> planetReasons; // Daftar ScriptableObject alasan

    public void Evaluate()
    {
        // Manager_UI.Instance.ShowUI();

        // var results = planetManager.GetResults();
        // int correct = results.Count(p =>
        //     p.playerDecision.HasValue &&
        //     p.playerDecision.Value == p.data.isHabitable);

        // // Menentukan jenis ending
        // string endingType = correct >= results.Count * 0.7f ? "Good Ending" : "Bad Ending";

        // endingUI.ShowEnding(endingType, results, correct, planetReasons);
        StartCoroutine(StartEnd());
    }

    IEnumerator StartEnd()
    {
        // Manager_UI.Instance.ShowUI();
        // yield return An_ScreenFade.Instance.FadeIn(new Color(0f, 0f, 0f), 5f); // Fade-in untuk akhir game
        bool waitFadeDark = false;
        An_ScreenFade.Instance.FadeIn(new Color(0f, 0f, 0f), 5f, () =>
        {
            waitFadeDark = true;
        }); // Fade-in untuk akhir game

        yield return new  WaitUntil(() => waitFadeDark);


        List<SG2_PlanetRuntime> results = planetManager.GetResults();
        Debug.LogWarning($"[Ending Eval] isi dari GetResult planetManager = {results.Count}");
        int correct = results.Count(p =>
            p.playerDecision.HasValue &&
            p.playerDecision.Value == p.data.isHabitable);

        // Menentukan jenis ending
        string endingType = correct >= results.Count * 0.7f ? "Good Ending" : "Bad Ending";

        endingUI.ShowEnding(endingType, results, correct, planetReasons);
    }
}










// public class SG2_EndingEvaluator : MonoBehaviour
// {
//     public SG2_EndingUI endingUI;

//     public void Evaluate()
//     {
//         var results = SG2_PlanetManager.Instance.GetResults();
//         int correct = results.Count(p =>
//             p.playerDecision.HasValue &&
//             p.playerDecision.Value == p.data.isHabitable);

//         An_ScreenFade.Instance.FadeIn(new Color(0f, 0f, 0f), 5f); // Fade-in untuk akhir game

//         if (correct >= results.Count * 0.7f)
//             endingUI.ShowEnding("Good Ending", results, correct);
//         else
//             endingUI.ShowEnding("Bad Ending", results, correct);
//     }
// }











// using UnityEngine;
// using System.Linq;

// public class SG2_EndingEvaluator : MonoBehaviour
// {
//     public void Evaluate()
//     {
//         var results = SG2_PlanetManager.Instance.GetResults();

//         int correct = results.Count(p =>
//             p.playerDecision.HasValue &&
//             p.playerDecision.Value == p.data.isHabitable);

//         Debug.Log("Correct: " + correct + "/" + results.Count);
//         An_ScreenFade.Instance.FadeIn(new Color(0f, 0f, 0f), 5f); // Ending Game

//         if (correct >= results.Count * 0.7f)
//             Debug.Log("[Ending] GOOD ENDING");
//         else
//             Debug.Log("[Ending] BAD ENDING");
//     }

// }
