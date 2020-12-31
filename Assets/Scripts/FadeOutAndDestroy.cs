using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAndDestroy : MonoBehaviour
{
    [SerializeField] float waitToFade = 1.0f;
    [SerializeField] float fadeTime = 2.0f;
    
    Color solidColor;
    Color fadedColor;
    Renderer rendererComp;

    void Start()
    {
        rendererComp = GetComponent<Renderer>();
        solidColor = rendererComp.material.color;
        fadedColor = new Color(solidColor.r, solidColor.g, solidColor.b, 0);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitToFade);
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            rendererComp.material.color = Color.Lerp(solidColor, fadedColor, t / fadeTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
