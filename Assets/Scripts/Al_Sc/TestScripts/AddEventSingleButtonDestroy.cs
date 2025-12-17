using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AddEventSingleButtonDestroy : MonoBehaviour
{
    [Header("Settings")]
    public Button btnOke;
    public GameObject parentOfPrefab;

    // Deklarasi event agar bisa dipanggil dari luar prefab
    public event Action FunctionAdder;

    void Start()
    {
        // Menambahkan listener ke button
        btnOke.onClick.AddListener(OnStartClicked);
    }

    // Fungsi yang dijalankan ketika tombol ditekan
    public void OnStartClicked()
    {
        Destroy(parentOfPrefab); // Hapus prefab setelah tombol ditekan
        
        // Menjalankan event jika ada
        FunctionAdder?.Invoke(); 
    }
}
