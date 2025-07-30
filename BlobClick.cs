using UnityEngine;
using System.Collections;

public class BlobClick : MonoBehaviour
{
    private RebirthData rebirthData;
    private Coroutine squishRoutine;
    private Vector3 originalScale;
    public AudioClip blobClickSound;  
    private AudioSource audioSource;

    void Start()
    {
        rebirthData = FindObjectOfType<RebirthData>();
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ClickBlob()
    {
        if (rebirthData != null)
            rebirthData.GainEnergy();

        if (squishRoutine != null)
            StopCoroutine(squishRoutine);

        squishRoutine = StartCoroutine(SquishEffect());

        if (blobClickSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f); // vary pitch
            audioSource.PlayOneShot(blobClickSound);
            audioSource.pitch = 1f; // reset pitch
        }
    }

    IEnumerator SquishEffect()
    {
        transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
        squishRoutine = null;
    }
}
