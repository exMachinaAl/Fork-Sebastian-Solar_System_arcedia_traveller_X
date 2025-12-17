using UnityEngine;
using UnityEngine.UI;

public class UI_NavigatorUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] RectTransform root;
    [SerializeField] RectTransform arrow;
    [SerializeField] Text label;  // Label untuk nama objek dan jarak

    UI_NavigatorTarget target;
    Camera cam;

    const float BORDER = 60f;
    const float CLOSE_DISTANCE = 30f; // Batas jarak dekat

    public void Bind(UI_NavigatorTarget target)
    {
        this.target = target;
        cam = Camera.main;

        label.text = target.displayName;

        FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate += OnOriginShift;
        UpdateVisual();
    }

    void OnDestroy()
    {
        FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate -= OnOriginShift;
    }

    void OnOriginShift()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        // Hitung jarak antara kamera (pemain) dan target
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);

        // Posisi di layar
        Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);

        // Cek apakah target di belakang kamera
        if (screenPos.z < 0)
        {
            screenPos *= -1;
        }

        // Cek apakah target berada di layar
        bool onScreen =
            screenPos.z > 0 &&
            screenPos.x > BORDER && screenPos.x < Screen.width - BORDER &&
            screenPos.y > BORDER && screenPos.y < Screen.height - BORDER;

        // Jika target berada di layar
        if (onScreen)
        {
            arrow.gameObject.SetActive(false);
            root.position = screenPos;

            // Cek apakah jaraknya sangat dekat
            if (distance < CLOSE_DISTANCE)
            {
                label.gameObject.SetActive(false); // Sembunyikan label ketika jaraknya sangat dekat
            }
            else
            {
                label.gameObject.SetActive(true); // Tampilkan label jika jaraknya lebih jauh
                label.text = $"{target.displayName}\n {distance.ToString("F2")} meters"; // Tampilkan jarak
            }

            return;
        }

        // Jika target di luar layar, tampilkan arrow
        arrow.gameObject.SetActive(true);
        screenPos.x = Mathf.Clamp(screenPos.x, BORDER, Screen.width - BORDER);
        screenPos.y = Mathf.Clamp(screenPos.y, BORDER, Screen.height - BORDER);
        root.position = screenPos;

        Vector2 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
        Vector2 dir = (Vector2)screenPos - screenCenter;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // Jika jaraknya sangat dekat, sembunyikan label
        if (distance < CLOSE_DISTANCE)
        {
            label.gameObject.SetActive(false);
        }
        else
        {
            label.gameObject.SetActive(true);
            label.text = $"{target.displayName}\n {distance.ToString("F2")} meters"; // Tampilkan jarak
        }
    }
}

















// using UnityEngine;
// using UnityEngine.UI;

// public class UI_NavigatorUI : MonoBehaviour
// {
//     [Header("Refs")]
//     [SerializeField] RectTransform root;
//     [SerializeField] RectTransform arrow;
//     [SerializeField] Text label;  // Label untuk nama objek dan jarak

//     UI_NavigatorTarget target;
//     Camera cam;

//     const float BORDER = 60f;

//     public void Bind(UI_NavigatorTarget target)
//     {
//         this.target = target;
//         cam = Camera.main;

//         label.text = target.displayName;

//         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate += OnOriginShift;
//         UpdateVisual();
//     }

//     void OnDestroy()
//     {
//         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate -= OnOriginShift;
//     }

//     void OnOriginShift()
//     {
//         if (target == null)
//         {
//             Destroy(gameObject);
//             return;
//         }

//         UpdateVisual();
//     }

//     void UpdateVisual()
//     {
//         Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);

//         // behind camera
//         if (screenPos.z < 0)
//         {
//             screenPos *= -1;
//         }

//         bool onScreen =
//             screenPos.z > 0 &&
//             screenPos.x > BORDER && screenPos.x < Screen.width - BORDER &&
//             screenPos.y > BORDER && screenPos.y < Screen.height - BORDER;

//         if (onScreen)
//         {
//             arrow.gameObject.SetActive(false);
//             root.position = screenPos;

//             // Update label to show distance
//             float distance = Vector3.Distance(cam.transform.position, target.transform.position);
//             label.text = $"{target.displayName} - {distance.ToString("F2")} meters"; // Menampilkan jarak di label

//             return;
//         }

//         // offscreen arrow
//         arrow.gameObject.SetActive(true);

//         screenPos.x = Mathf.Clamp(screenPos.x, BORDER, Screen.width - BORDER);
//         screenPos.y = Mathf.Clamp(screenPos.y, BORDER, Screen.height - BORDER);

//         root.position = screenPos;

//         Vector2 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
//         Vector2 dir = (Vector2)screenPos - screenCenter;

//         float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//         arrow.rotation = Quaternion.Euler(0, 0, angle - 90f);

//         // Update label to show distance even if offscreen
//         float distanceOffScreen = Vector3.Distance(cam.transform.position, target.transform.position);
//         label.text = $"{target.displayName} \n{distanceOffScreen.ToString("F2")} meters"; // Menampilkan jarak
//     }
// }
















// // using UnityEngine;
// // using UnityEngine.UI;

// // public class UI_NavigatorUI : MonoBehaviour
// // {
// //     [Header("Refs")]
// //     [SerializeField] RectTransform root;
// //     [SerializeField] RectTransform arrow;
// //     [SerializeField] Text label;

// //     UI_NavigatorTarget target;
// //     Camera cam;

// //     const float BORDER = 60f;

// //     public void Bind(UI_NavigatorTarget target)
// //     {
// //         this.target = target;
// //         cam = Camera.main;

// //         label.text = target.displayName;

// //         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate += OnOriginShift;
// //         UpdateVisual();
// //     }

// //     // void LateUpdate()
// //     // {
// //     //     if (target == null)
// //     //     {
// //     //         Destroy(gameObject);
// //     //         return;
// //     //     }

// //     //     UpdateVisual();
// //     // }

// //     void OnDestroy()
// //     {
// //         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate -= OnOriginShift;
// //     }

// //     void OnOriginShift()
// //     {
// //         if (target == null)
// //         {
// //             Destroy(gameObject);
// //             return;
// //         }

// //         UpdateVisual();
// //     }

// //     void UpdateVisual()
// //     {
// //         Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);

// //         // behind camera
// //         if (screenPos.z < 0)
// //         {
// //             screenPos *= -1;
// //         }

// //         bool onScreen =
// //             screenPos.z > 0 &&
// //             screenPos.x > BORDER && screenPos.x < Screen.width - BORDER &&
// //             screenPos.y > BORDER && screenPos.y < Screen.height - BORDER;

// //         if (onScreen)
// //         {
// //             arrow.gameObject.SetActive(false);
// //             root.position = screenPos;
// //             return;
// //         }

// //         // offscreen arrow
// //         arrow.gameObject.SetActive(true);

// //         screenPos.x = Mathf.Clamp(screenPos.x, BORDER, Screen.width - BORDER);
// //         screenPos.y = Mathf.Clamp(screenPos.y, BORDER, Screen.height - BORDER);

// //         root.position = screenPos;

// //         Vector2 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
// //         Vector2 dir = (Vector2)screenPos - screenCenter;

// //         float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
// //         arrow.rotation = Quaternion.Euler(0, 0, angle - 90f);
// //     }
// // }










// // // using UnityEngine;
// // // using UnityEngine.UI;

// // // public class UI_NavigatorUI : MonoBehaviour
// // // {
// // //     [SerializeField] private RectTransform rect;
// // //     [SerializeField] private Text label;

// // //     private UI_NavigatorTarget target;
// // //     private Camera cam;

// // //     // void Update()
// // //     // {
// // //         // if (Input.GetKeyDown(KeyCode.F))
// // //         // {
// // //         //     UI_NavigatorSystem.ToggleNavigator();
// // //         // }
// // //     // }

// // //     public void Bind(UI_NavigatorTarget target)
// // //     {
// // //         this.target = target;
// // //         cam = Camera.main;
// // //         label.text = target.displayName;


// // //         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate += OnOriginShift;
// // //         UpdatePosition();
// // //     }

// // //     void OnDestroy()
// // //     {
// // //         FindObjectOfType<Game_FloatingOriginV3>().PostFloatingOriginUpdate -= OnOriginShift;
// // //     }

// // //     void OnOriginShift()
// // //     {
// // //         UpdatePosition();
// // //     }

// // //     public void UpdatePosition()
// // //     {
// // //         if (!target || !target.gameObject.activeInHierarchy)
// // //         {
// // //             gameObject.SetActive(false);
// // //             return;
// // //         }

// // //         Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);

// // //         gameObject.SetActive(screenPos.z > 0);
// // //         rect.position = screenPos;
// // //     }
// // // }
