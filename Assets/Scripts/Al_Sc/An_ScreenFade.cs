using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class An_ScreenFade : MonoBehaviour
{
    public Image image;
    public static An_ScreenFade Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }



    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Ship")) return;

    //     FadeIn(new Color(0.6f, 0.8f, 1f), 2f); // planet cerah
    //     // fade.FadeIn(new Color(0.6f, 0.8f, 1f), 2f); // planet cerah
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (!other.CompareTag("Ship")) return;

    //     FadeOut(2f); // balik ke space
    //     // fade.FadeOut(2f); // balik ke space
    // }

    public void FadeIn(Color color, float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(new Color(color.r, color.g, color.b, 1f), time));
    }

    public void FadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(new Color(0,0,0,0), time));
    }

    IEnumerator Fade(Color target, float time)
    {
        Color start = image.color;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            image.color = Color.Lerp(start, target, t);
            yield return null;
        }
    }
}
