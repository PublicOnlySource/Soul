using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAudioSource : MonoBehaviour
{
    private Coroutine coroutine;
    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        coroutine = StartCoroutine(Process());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator Process()
    {
        yield return new WaitUntil(() => audioSource.isPlaying);
        yield return CoroutineUtil.Instance.WaitForSeconds(audioSource.clip.length);

        if (audioSource.loop)
            yield break;

        PoolingManager.Instance.Push(typeof(AudioSource).Name, gameObject);
    }

}
