using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    [SerializeField]
    private float enableParryDuration = 0.1f;

    private bool isActive = false;
    private float timer;
    private Coroutine coroutine;

    IEnumerator Parrying()
    {
        timer = 0f;
        isActive = true;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > enableParryDuration)
            {
                isActive = false;
                coroutine = null;
                yield break;
            }

            yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.UPDATE_SECOND_0_01);
        }
    }

    public void EnableParry()
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(Parrying());
    }

    public void DisableParry()
    {
        if (coroutine != null) 
        {
            StopCoroutine(coroutine);
            coroutine = null;
            isActive = false;
        }
    }

    public bool IsSuccess()
    {
        return isActive && timer <= enableParryDuration;
    }
}
