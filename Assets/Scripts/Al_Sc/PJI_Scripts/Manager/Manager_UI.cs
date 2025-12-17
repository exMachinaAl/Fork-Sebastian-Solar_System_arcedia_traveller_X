using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;

public class Manager_UI : MonoBehaviour
{
    [Header("UI Manager")]
    public static Manager_UI Instance;

    [Header("UI Subtitle")]
    public GameObject UI_Subtitle;
    public GameObject UI_Main_Subtitle;
    public TMP_Text subtitleText;

    [Header("UI Information interupt")]
    public GameObject UI_InteruptInf;
    public GameObject UI_Main_Interupt;
    public GameObject prefabUIInter;
    public Transform rootInterTransf;
    // public TMP_Text informText;
    // public Button informBtn;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        HideUI();
    }

    public void DisplayInterruptMessage(string msg, Action callback)
    {
        ShowUI();
        UI_Main_Interupt.SetActive(true);
        GameObject objD = Instantiate(prefabUIInter, rootInterTransf);
        objD.GetComponentInChildren<TMP_Text>().text = msg;

        AddEventSingleButtonDestroy buttonScript = objD.GetComponent<AddEventSingleButtonDestroy>();
        if (buttonScript != null)
        {
            buttonScript.FunctionAdder += HideUI;  // Menambahkan event HideUI ke prefab
            buttonScript.FunctionAdder += callback;  // Menambahkan event HideUI ke prefab
        }
    }

    // Fungsi untuk menampilkan subtitle dengan delay
    public void ShowSubtitleWithCallback(string text, float duration, Action onHideCallback = null)
    {
        // UI_Main_Subtitle.SetActive(true);
        // Tampilkan UI dan set text
        UI_Main_Subtitle.SetActive(true);
        subtitleText.text = text;

        // Mulai coroutine untuk menunggu durasi
        StartCoroutine(HideSubtitleAfterDelay(duration, onHideCallback));
    }

    private IEnumerator HideSubtitleAfterDelay(float duration, Action onHideCallback)
    {
        yield return new WaitForSeconds(duration);

        // Sembunyikan subtitle setelah durasi
        UI_Main_Subtitle.SetActive(false);

        // Jika ada callback, jalankan
        onHideCallback?.Invoke();
    }

    // Fungsi tambahan jika ingin langsung sembunyikan tanpa callback
    public void HideSubtitle()
    {
        UI_Main_Subtitle.SetActive(false);
    }

    public void ShowUI()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Membuka kursor
        // UI_Main_.SetActive(true);
    }
    void HideUI()
    {
        Time.timeScale = 1;
        // #########
        var shipSc = FindObjectOfType<Ship>();

        // Periksa apakah pemain mengendarai pesawat
        if (shipSc != null && !shipSc.IsThePlayerPiloting())
        {
            // Cari PlayerController hanya jika pesawat tidak dikendarai oleh pemain
            var playerController = FindObjectOfType<PlayerController>();

            // Periksa jika PlayerController ditemukan dan apakah aktif
            if (playerController != null && playerController.ShouldBeActive)
            {
                playerController.OnResumeGame();
            }
        }
        // #########
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Mengunci kursor di tengah
        UI_Main_Subtitle.SetActive(false);
        UI_Main_Interupt.SetActive(false);
        // UI_Main_.SetActive(false);
    }
}
