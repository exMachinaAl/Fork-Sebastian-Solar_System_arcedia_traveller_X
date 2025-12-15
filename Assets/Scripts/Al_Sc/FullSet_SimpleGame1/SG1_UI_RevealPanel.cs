using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SG1_UI_RevealPanel : MonoBehaviour
{
    public static SG1_UI_RevealPanel Instance;

    public GameObject panel;
    public TMP_Text infoText;
    public Button continueButton;

    private Action callback;

    void Awake() => Instance = this;

    public void Show(string info, Action onContinue)
    {
        callback = onContinue;
        infoText.text = info;

        panel.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            callback?.Invoke();
        });
    }
}
