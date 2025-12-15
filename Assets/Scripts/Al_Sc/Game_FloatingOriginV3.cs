using UnityEngine;
using System.Collections.Generic;

public class Game_FloatingOriginV3 : MonoBehaviour
{
    [Header("Assign Player Root Transform")]
    public Transform player;

    [Header("Jarak batas sebelum dunia digeser")]
    public float threshold = 500f;

    public static Vector3 globalOffset;

    private static readonly List<Transform> sceneObjects = new();

    public event System.Action PostFloatingOriginUpdate;

    void Start()
    {
        RefreshSceneObjects();
    }

    void LateUpdate()
    {
        if (player == null) return;

        FloatingUpdate();

        PostFloatingOriginUpdate?.Invoke();
    }

    void FloatingUpdate()
    {
        // FULL 3D check
        if (player.position.sqrMagnitude > threshold * threshold)
        {
            Vector3 shift = player.position; // FULL VECTOR (INI KUNCI)

            // Geser seluruh dunia
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                Transform t = sceneObjects[i];
                if (!t || t == player) continue;

                t.position -= shift;
            }

            // Catat offset global (untuk galaxy coords, save/load, dll)
            globalOffset += shift;

            // Player benar-benar kembali ke origin
            player.position = Vector3.zero;

            Physics.SyncTransforms();

            Debug.Log($"[FloatingOrigin360] Shift: {shift} | GlobalOffset: {globalOffset}");
        }
    }

    public void RefreshSceneObjects()
    {
        sceneObjects.Clear();

        foreach (var t in FindObjectsOfType<Transform>())
        {
            if (t == player) continue;
            if (t.parent != null) continue; // hanya root world objects
            sceneObjects.Add(t);
        }
    }

    public static void RegisterObject(Transform t)
    {
        if (t && !sceneObjects.Contains(t))
            sceneObjects.Add(t);
    }

    public static void UnregisterObject(Transform t)
    {
        sceneObjects.Remove(t);
    }

    // Fungsi untuk mengubah pemain (switch character)
    public void SwitchPlayer(Transform newPlayer)
    {
        if (newPlayer != null)
        {
            // Update referensi player yang aktif
            player = newPlayer;

            // Pastikan semua objek di-refresh dengan player baru
            RefreshSceneObjects();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Vector3.zero, threshold);
    }
}














// public class Game_FloatingOriginV3 : MonoBehaviour
// {
//     [Header("Assign Player Root Transform")]
//     public Transform player;

//     [Header("Jarak batas sebelum dunia digeser")]
//     public float threshold = 500f;

//     public static Vector3 globalOffset;

//     private static readonly List<Transform> sceneObjects = new();

//     public event System.Action PostFloatingOriginUpdate;

//     void Start()
//     {
//         RefreshSceneObjects();
//     }

//     void LateUpdate()
//     {
//         if (!player) return;

//         FloatingUpdate();
//         if (PostFloatingOriginUpdate != null) {
//             PostFloatingOriginUpdate ();
//         }
//     }

//     void FloatingUpdate()
//     {
//         // FULL 3D check
//         if (player.position.sqrMagnitude > threshold * threshold)
//         {
//             Vector3 shift = player.position; // FULL VECTOR (INI KUNCI)

//             // Geser seluruh dunia
//             for (int i = 0; i < sceneObjects.Count; i++)
//             {
//                 Transform t = sceneObjects[i];
//                 if (!t || t == player) continue;

//                 t.position -= shift;
//             }

//             // Catat offset global (untuk galaxy coords, save/load, dll)
//             globalOffset += shift;

//             // Player benar-benar kembali ke origin
//             player.position = Vector3.zero;

//             Physics.SyncTransforms();

//             Debug.Log($"[FloatingOrigin360] Shift: {shift} | GlobalOffset: {globalOffset}");
//         }
//     }

//     public void RefreshSceneObjects()
//     {
//         sceneObjects.Clear();

//         foreach (var t in FindObjectsOfType<Transform>())
//         {
//             if (t == player) continue;
//             if (t.parent != null) continue; // hanya root world objects
//             sceneObjects.Add(t);
//         }
//     }

//     public static void RegisterObject(Transform t)
//     {
//         if (t && !sceneObjects.Contains(t))
//             sceneObjects.Add(t);
//     }

//     public static void UnregisterObject(Transform t)
//     {
//         sceneObjects.Remove(t);
//     }

//     void OnDrawGizmos()
//     {
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireSphere(Vector3.zero, threshold);
//     }
// }
