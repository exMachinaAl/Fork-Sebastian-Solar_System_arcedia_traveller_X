using UnityEngine;
using TMPro;

public class SG1_UI_EndingPanel : MonoBehaviour
{
    public static SG1_UI_EndingPanel Instance;

    public GameObject panel;
    public TMP_Text endingText;

    void Awake() => Instance = this;

    public void ShowEnding()
    {
        panel.SetActive(true);

        int correct = SG1_GameFlowManager.Instance.correctAnswer;
        int wrong = SG1_GameFlowManager.Instance.wrongAnswer;

        bool goodEnding = correct > wrong;

        endingText.text = goodEnding
            ? "GOOD ENDING\nKamu menilai planet dengan sangat akurat!"
            : "BAD ENDING\nKamu salah menilai terlalu banyak planet...";
    }
}
