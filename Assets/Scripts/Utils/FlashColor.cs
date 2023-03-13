using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float durationFlash = .3f;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Flash();
    }

    public void Flash()
    {
        foreach (var c in spriteRenderers)
            c.DOColor(color, durationFlash).SetLoops(2, LoopType.Yoyo);
    }
}
