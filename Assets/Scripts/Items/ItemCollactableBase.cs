using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particles;
    public float timeToHide = 3f;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        //if (particles != null) particles.transform.SetParent(null);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
            Collect();

        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particles != null) particles.Play();
        if (audioSource != null) audioSource.Play();
    }
}
