using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtil : Singleton<CoroutineUtil>
{
    private Dictionary<float, WaitForSeconds> secondDic = new Dictionary<float, WaitForSeconds>();

    public static readonly float UPDATE_SECOND_0_01 = 0.01f;
    public static readonly float WAIT_SECOND_2 = 2f;

    public new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null)
            throw new System.Exception();

        return (Instance as MonoBehaviour).StartCoroutine(coroutine);
    }

    public new void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine == null)
            return;

        (Instance as MonoBehaviour).StopCoroutine(coroutine);
    }

    public new void StopAllCoroutines()
    {
        (Instance as MonoBehaviour).StopAllCoroutines();
    }

    public WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!secondDic.TryGetValue(seconds, out WaitForSeconds wfs))
            secondDic.Add(seconds, wfs = new WaitForSeconds(seconds));

        return wfs;
    }
}
