using UnityEngine;
using TMPro;

public class SG1_UI_PlanetPanel : MonoBehaviour
{
    public static SG1_UI_PlanetPanel Instance;

    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text summaryText;

    void Awake() => Instance = this;

    public void ShowOverview(SG1_SO_Planet p)
    {
        panel.SetActive(true);
        titleText.text = p.planetName;
        summaryText.text = p.planetSummary;
    }

    public void OnAnalyzePressed()
    {
        panel.SetActive(false);
        SG1_PlanetManager.Instance.StartRevealSequence();
    }

    public void ShowNextPlanetMessage()
    {
        panel.SetActive(true);
        titleText.text = "Planet berikutnya siap dianalisis!";
        summaryText.text = "Pergilah ke planet lain.";
    }
}
