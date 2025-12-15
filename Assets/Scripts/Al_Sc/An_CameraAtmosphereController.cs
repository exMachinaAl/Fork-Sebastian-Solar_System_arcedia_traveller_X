using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class An_CameraAtmosphereController : MonoBehaviour
{
    public static An_CameraAtmosphereController Instance;

    Camera cam;
    Coroutine routine;

    [Header("Default Space Color")]
    public Color spaceColor = Color.black;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cam = GetComponent<Camera>();

        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = spaceColor;
    }

    public void SetAtmosphere(Color targetColor, float duration)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(LerpColor(targetColor, duration));
    }

    public void ResetToSpace(float duration)
    {
        SetAtmosphere(spaceColor, duration);
    }

    IEnumerator LerpColor(Color target, float time)
    {
        Color start = cam.backgroundColor;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / time;
            cam.backgroundColor = Color.Lerp(start, target, t);
            yield return null;
        }

        cam.backgroundColor = target;
    }
}
