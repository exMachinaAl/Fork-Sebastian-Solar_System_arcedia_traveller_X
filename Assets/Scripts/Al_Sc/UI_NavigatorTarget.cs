using UnityEngine;

public class UI_NavigatorTarget : MonoBehaviour
{
    public string factId;
    public string displayName;

    public void ShowNavigator()
    {
        UI_NavigatorSystem.Instance.Show(this);
    }

    public void HideNavigator()
    {
        if (UI_NavigatorSystem.Instance != null)
            UI_NavigatorSystem.Instance.Hide(this);
    }
}
