
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class SG2_UI_PlanetPanel : MonoBehaviour
{
    public static SG2_UI_PlanetPanel Instance;

    public TMP_Text planetName;
    public TMP_Text dataText;
    public Button btnCloseInfo;
    public Button btnReadyToAnswer;

    SG2_PlanetRuntime runtime;
    bool isInformationShow = false;
    public SG2_UI_DecisionPanel decisionPanel;

    KeyCode openInformationButton = KeyCode.I;

    private CanvasGroup canvasGroup; // Tambahkan CanvasGroup untuk kontrol visibilitas

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>(); // Ambil CanvasGroup yang ada pada objek ini
    }

    void Start()
    {
        btnReadyToAnswer.onClick.AddListener(OnClickReadyAnswer);
        btnCloseInfo.onClick.AddListener(ToggleInformationUI);

        if (canvasGroup != null)
            canvasGroup.alpha = 0; // Set transparansi ke 0 agar UI tidak terlihat
        canvasGroup.interactable = false; // Menonaktifkan interaktivitas UI
        canvasGroup.blocksRaycasts = false; // Menonaktifkan raycast agar UI tidak menerima input
    }

    void Update()
    {
        OpenUIPlanetInformation();
    }

    //## press button to openInfoPlanetFyi
    public void OpenUIPlanetInformation()
    {
        if (runtime == null)
        {
            Debug.Log($"[Quest system] runtime nya loh null");
            return;
        }

        if (Input.GetKeyDown(openInformationButton))
        {
            Debug.Log($"[Quest system] Tombol I ditekan");
            Refresh();
            ToggleInformationUI();
        }
    }

    void OnClickReadyAnswer()
    {
        // decisionPanel.SetUIActive(true);
        ToggleInformationUI();
        decisionPanel.ToggleUI();
    }

    public void ToggleInformationUI()
    {
        if (isInformationShow)
        {
            Time.timeScale = 1;
            var shipSc = FindObjectOfType<Ship>();
            if (!shipSc.IsThePlayerPiloting())
                FindObjectOfType<PlayerController>().OnResumeGame();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // Mengunci kursor di tengah
            isInformationShow = false;
            // decisionPanel.SetUIActive(false);
            SetUIActive(false); // Panggil fungsi untuk menyembunyikan UI
        }
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Membuka kursor
            isInformationShow = true;
            // decisionPanel.SetUIActive(false);
            SetUIActive(true); // Panggil fungsi untuk menampilkan UI
        }
    }

    public void SetUIActive(bool isActive)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isActive ? 1 : 0; // Jika aktif, set transparansi 1, jika tidak set 0
            canvasGroup.interactable = isActive; // Mengaktifkan atau menonaktifkan interaktivitas
            canvasGroup.blocksRaycasts = isActive; // Mengaktifkan atau menonaktifkan penerimaan input
        }
    }

    public void ShowPlanet(SG2_PlanetRuntime r)
    {
        runtime = r;
        Refresh();
    }

    public void Refresh()
    {
        planetName.text = runtime.data.planetName;

        StringBuilder sb = new();

        sb.AppendLine(runtime.data.description);

        foreach (var fact in runtime.data.facts)
        {
            bool revealed = runtime.revealedFacts[fact.factId];
            sb.AppendLine("");
            sb.AppendLine(revealed ? fact.factText : "missing information ???");
        }

        dataText.text = sb.ToString();
    }
}



















// using UnityEngine;
// using TMPro;
// using System.Text;

// public class SG2_UI_PlanetPanel : MonoBehaviour
// {
//     public static SG2_UI_PlanetPanel Instance;

//     public TMP_Text planetName;
//     public TMP_Text dataText;

//     SG2_PlanetRuntime runtime;
//     bool isInformationShow = false;
//     public SG2_UI_DecisionPanel decisionPanel;

//     KeyCode openInformationButton = KeyCode.I;

//     void Awake() => Instance = this;

//     void Start()
//     {
//         gameObject.SetActive(false);
//     }

//     void Update()
//     {
//         OpenUIPlanetInformation();
//     }

//     //## press button to openInfoPlanetFyi
//     public void OpenUIPlanetInformation()
//     {
//         if (runtime == null)
//         {
//             Debug.Log($"[Quest system] runtime nya loh null");
//             return;
//         }

//         if (Input.GetKeyDown(openInformationButton))
//             {
//                 Debug.Log($"[Quest system] Tombol I ditekan");
//                 Refresh();
//                 ToggleInformationUI();
//             }

//     }

//     public void ToggleInformationUI()
//     {
//         if (isInformationShow)
//         {
//             isInformationShow = false;
//             decisionPanel.ToggleUI();
//             gameObject.SetActive(false);
//         }
//         else
//         {
//             isInformationShow = true;
//             decisionPanel.ToggleUI();
//             gameObject.SetActive(true);
//         }
//     }




//     public void ShowPlanet(SG2_PlanetRuntime r)
//     {
//         runtime = r;
//         Refresh();
//     }

//     public void Refresh()
//     {
//         planetName.text = runtime.data.planetName;

//         StringBuilder sb = new();

//         foreach (var fact in runtime.data.facts)
//         {
//             bool revealed = runtime.revealedFacts[fact.factId];
//             sb.AppendLine(revealed ? fact.factText : "???");
//         }

//         dataText.text = sb.ToString();
//     }
// }
