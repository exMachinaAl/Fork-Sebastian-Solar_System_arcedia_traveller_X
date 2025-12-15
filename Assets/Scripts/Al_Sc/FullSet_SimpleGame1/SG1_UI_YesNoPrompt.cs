using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SG1_UI_YesNoPrompt : MonoBehaviour
{
    public static SG1_UI_YesNoPrompt Instance;

    public GameObject panel;
    public TMP_Text questionText;
    public Button yesButton;
    public Button noButton;

    void Awake() => Instance = this;

    public void Show(string question, Action onYes, Action onNo)
    {
        panel.SetActive(true);
        questionText.text = question;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            onYes?.Invoke();
        });

        noButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            onNo?.Invoke();
        });
    }
}
