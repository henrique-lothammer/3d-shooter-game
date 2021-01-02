using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] GameObject flashHolder;
    [SerializeField] Sprite[] flashSprites;
    [SerializeField] SpriteRenderer[] spriteRenderers;

    [SerializeField] float flashTime = 0.05f;

    void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        flashHolder.SetActive(true);

        int flashSpriteIndex = Random.Range(0, flashSprites.Length);
        for(int i=0; i< spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = flashSprites[flashSpriteIndex];
        }

        Invoke(nameof(Deactivate), flashTime);
    }

    void Deactivate()
    {
        flashHolder.SetActive(false);
    }
}
