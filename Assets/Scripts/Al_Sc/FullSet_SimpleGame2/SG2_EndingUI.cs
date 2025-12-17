using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SG2_EndingUI : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public TextMeshProUGUI mistakeText;
    public TextMeshProUGUI reasonText;
    public Button buttonNext;
    public Button buttonFinish;
    public Canvas fadeCanvas;
    public CanvasGroup canvasGroup;

    private int currentMistakeIndex = 0;
    private List<string> mistakesList = new List<string>();

    void Start()
    {
        fadeCanvas.sortingOrder = 0; // Sorting order paling tinggi
        canvasGroup.alpha = 0f; // Mulai dengan transparan
        // nextButton.SetActive(false); // Mulai dengan tombol next nonaktif
        // buttonFinish.SetActive(false);
    }

    // Menampilkan Ending dan Kesalahan
    public void ShowEnding(string endingType, List<SG2_PlanetRuntime> results, int correct, List<SG2_SOPlanetReason> planetReasons)
    {
        
        fadeCanvas.sortingOrder = 3; // Sorting order paling tinggi
        canvasGroup.blocksRaycasts = true;  // Nonaktifkan raycast supaya 
                                            // Menampilkan jenis Ending

        Debug.LogWarning($" isi runtime = {results.Count}, is planetRes = {planetReasons.Count}");

        endingText.text = $"{endingType}\n\nKesalahan: {results.Count - correct}/{results.Count}";

        // Membuat daftar alasan kesalahan berdasarkan hasil
        mistakesList = GetMistakes(results, planetReasons);
        Debug.Log($"Jumlah kesalahan: {mistakesList.Count}");

        if (mistakesList.Count > 0)
        {
            DisplayMistake(mistakesList[currentMistakeIndex]);
        }
        else
        {
            // Jika tidak ada kesalahan, tampilkan pesan default
            reasonText.text = "Tidak ada kesalahan.";
        }
        // DisplayMistake(mistakesList[currentMistakeIndex]);

        // Tombol Next aktifkan
        buttonNext.gameObject.SetActive(true);
        buttonFinish.gameObject.SetActive(false); // Finish belum aktif

        // Fade-In UI
        StartCoroutine(FadeInCanvas());
    }

    // Menampilkan alasan kesalahan iteratif
    private void DisplayMistake(string mistake)
    {
        // reasonText.text = mistake;
        reasonText.text += $"{mistake} \n\n";
    }


    private List<string> GetMistakes(List<SG2_PlanetRuntime> results, List<SG2_SOPlanetReason> planetReasons)
    {
        List<string> mistakes = new List<string>();

        foreach (var planet in results)
        {
            if (planet.playerDecision.HasValue)
            {
                bool isHabitable = planet.data.isHabitable;
                bool playerDecision = planet.playerDecision.Value;

                // Cari SG2_SOPlanetReason berdasarkan planetId
                SG2_SOPlanetReason reason = planetReasons.Find(pr => pr.planetId == planet.data.planetId);

                if (reason != null) // Jika ditemukan alasan untuk planet ini
                {
                    string symbolAnsw = playerDecision == isHabitable
                        ? "✔"
                        : "X";
                    string reasonText = playerDecision == isHabitable
                        ? reason.correctExplanation
                        : reason.incorrectExplanation;
                    mistakes.Add($"Planet: {planet.data.planetName} [{symbolAnsw}] \n\t{reasonText}");
                }
                else
                {
                    // Jika tidak ada alasan untuk planet ini, tambahkan default message
                    mistakes.Add($"Planet: {planet.data.planetName} - Tidak ada alasan yang tersedia.");
                }
            }
        }

        return mistakes;
    }


    // Mendapatkan alasan berdasarkan keputusan pemain
    // private List<string> GetMistakes(List<SG2_PlanetRuntime> results, List<SG2_SOPlanetReason> planetReasons)
    // {
    //     List<string> mistakes = new List<string>();

    //     foreach (var planet in results)
    //     {
    //         if (planet.playerDecision.HasValue)
    //         {
    //             bool isHabitable = planet.data.isHabitable;
    //             bool playerDecision = planet.playerDecision.Value;

    //             SG2_SOPlanetReason reason = planetReasons.Find(pr => pr.planetId == planet.data.planetId);
    //             if (reason != null)
    //             {
    //                 string reasonText = playerDecision == isHabitable
    //                     ? reason.correctExplanation
    //                     : reason.incorrectExplanation;
    //                 mistakes.Add($"Planet: {planet.data.planetName} - {reasonText}");
    //             }
    //         }
    //     }

    //     return mistakes;
    // }

    // Tombol untuk menunjukkan kesalahan berikutnya
    public void ShowNextMistake()
    {
        currentMistakeIndex++;
        if (currentMistakeIndex < mistakesList.Count)
        {
            DisplayMistake(mistakesList[currentMistakeIndex]);
        }
        else
        {
            buttonNext.gameObject.SetActive(false);
            buttonFinish.gameObject.SetActive(true); // Tampilkan tombol Finish setelah semua kesalahan ditampilkan
        }
    }

    // Fungsi untuk Fade-In
    private IEnumerator FadeInCanvas()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        
        
        // Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Membuka kursor
    }

    // Tombol Finish untuk kembali ke Menu Utama
    public void OnFinishButtonClick()
    {
        // Lakukan transisi fade-out dan kembali ke main menu
        StartCoroutine(FadeOutAndReturnToMainMenu());
    }

    private IEnumerator FadeOutAndReturnToMainMenu()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // Pindahkan ke Main Menu (Scene)
        UnityEngine.SceneManagement.SceneManager.LoadScene("I_Lobby");
    }
}
