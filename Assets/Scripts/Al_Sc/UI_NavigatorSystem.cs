using System.Collections.Generic;
using UnityEngine;

public class UI_NavigatorSystem : MonoBehaviour
{
    public static UI_NavigatorSystem Instance { get; private set; }

    [Header("UI")]
    [SerializeField] UI_NavigatorUI prefab;
    [SerializeField] Transform root;

    Dictionary<UI_NavigatorTarget, UI_NavigatorUI> active = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Show(UI_NavigatorTarget target)
    {
        if (active.ContainsKey(target)) return;
        if (!target.isActiveAndEnabled) return;

        var ui = Instantiate(prefab, root);
        ui.Bind(target);
        active.Add(target, ui);
    }

    public void Hide(UI_NavigatorTarget target)
    {
        if (!active.TryGetValue(target, out var ui)) return;

        Destroy(ui.gameObject);
        active.Remove(target);
    }

    public void ClearAll()
    {
        foreach (var ui in active.Values)
            Destroy(ui.gameObject);

        active.Clear();
    }
}







// public class UI_NavigatorSystem : MonoBehaviour
// {
//     public static UI_NavigatorSystem Instance;

//     [SerializeField] private Transform rootUINavigator;
//     [SerializeField] private UI_NavigatorUI uiPrefab;
//     private Dictionary<UI_ObjectNavigator, UI_NavigatorUI> activeUIs = new();

//     public static bool NavigatorEnabled = false;
//     public static string ActiveTag = "focusFind";

//     private void Awake()
//     {
//         Instance = this;
//     }

//     public static void Register(UI_ObjectNavigator target)
//     {
//         if (!NavigatorEnabled) return;
//         if (target.navigatorTag != ActiveTag) return;

//         Instance.CreateUI(target);
//     }

//     public static void Unregister(UI_ObjectNavigator target)
//     {
//         Instance.RemoveUI(target);
//     }

//     void CreateUI(UI_ObjectNavigator target)
//     {
//         if (activeUIs.ContainsKey(target)) return;

//         var ui = Instantiate(uiPrefab, rootUINavigator);
//         ui.Bind(target);
//         activeUIs.Add(target, ui);
//     }

//     void RemoveUI(UI_ObjectNavigator target)
//     {
//         if (!activeUIs.TryGetValue(target, out var ui)) return;

//         Destroy(ui.gameObject);
//         activeUIs.Remove(target);
//     }

//     public static void ToggleNavigator()
//     {
//         NavigatorEnabled = !NavigatorEnabled;

//         if (!NavigatorEnabled)
//             Instance.ClearAll();
//     }

//     void ClearAll()
//     {
//         foreach (var ui in activeUIs.Values)
//             Destroy(ui.gameObject);

//         activeUIs.Clear();
//     }
// }
