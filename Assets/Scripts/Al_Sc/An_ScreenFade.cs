using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class An_ScreenFade : MonoBehaviour
{
    public Canvas fadeCanvas;
    public CanvasGroup fadeCanvasGroup;
    public Image image;
    public static An_ScreenFade Instance;
    public Action cacheCb;

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

    public void FadeIn(Color color, float time, Action callback)
    {
        cacheCb += callback;

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
        setToCanvasGroup();
        image.gameObject.SetActive(true);
        Color start = image.color;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            image.color = Color.Lerp(start, target, t);
            yield return null;
        }
        cacheCb?.Invoke();
    }

    public void setToCanvasGroup()
    {
        
        fadeCanvas.sortingOrder = 2; // Sorting order paling tinggi
        // fadeCanvasGroup.alpha = 0f;           // Sembunyikan visualnya
        fadeCanvasGroup.blocksRaycasts = true;  // Nonaktifkan raycast supaya gameplay UI dapat menerima input
    }
}
