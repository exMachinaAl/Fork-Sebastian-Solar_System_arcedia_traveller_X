using UnityEngine;
using UnityEngine.UI;

public class ScrollEndTrigger : MonoBehaviour
{
    public ScrollRect scrollRect;  // Drag Scroll Rect ke sini di Inspector

    void Update()
    {
        // Periksa apakah scroll sudah mencapai ujung bawah
        if (scrollRect.verticalNormalizedPosition <= 0.05f)  // Threshold sekitar 5% dari bawah
        {
            // Aksi ketika scroll sudah mencapai ujung bawah
            Debug.Log("Scroll sudah mencapai ujung bawah!");
            // Kamu bisa tambahkan aksi lain di sini, seperti memunculkan UI atau lainnya
        }
    }
}
