using UnityEngine;
using UnityEngine.UI;

public class SG2_UI_DecisionPanel : MonoBehaviour
{

    public bool isInformationShow = false;
    [SerializeField]private CanvasGroup canvasGroup; // Tambahkan CanvasGroup untuk kontrol visibilitas

    public Button btnClose;
    public Button btnYes;
    public Button btnNo;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>(); // Ambil CanvasGroup yang ada pada objek ini
    }
    void Start()
    {
        btnClose.onClick.AddListener(OnClose);
        btnYes.onClick.AddListener(DecideYes);
        btnNo.onClick.AddListener(DecideNo);

        if (canvasGroup != null)
            canvasGroup.alpha = 0; // Set transparansi ke 0 agar UI tidak terlihat
        canvasGroup.interactable = false; // Menonaktifkan interaktivitas UI
        canvasGroup.blocksRaycasts = false; // Menonaktifkan raycast agar UI tidak menerima input
    }

    // void Update()
    // {

    // }

    void HandleDecissionWithKey()
    {
        if (!isInformationShow) return;

        // if 
    }

    public void ToggleUI()
    {
        if (isInformationShow)
        {
            isInformationShow = false;
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
            SetUIActive(false); // Panggil fungsi untuk menyembunyikan UI
        }
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Membuka kursor
            isInformationShow = true;
            SetUIActive(true); // Panggil fungsi untuk menampilkan UI
        }
    }

    public void OnClose()
    {
        ToggleUI();
    }
    public void DecideYes()
    {
        SG2_PlanetManager.Instance.MakeDecision(true);
        // gameObject.SetActive(false);
        ToggleUI();
        // SetUIActive(false); // Panggil fungsi untuk menampilkan UI
    }

    public void DecideNo()
    {
        SG2_PlanetManager.Instance.MakeDecision(false);
        // gameObject.SetActive(false);
        ToggleUI();
        // SetUIActive(true); // Panggil fungsi untuk menampilkan UI
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
}
