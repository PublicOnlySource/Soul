using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Destroyed : UI_Popup
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float hideTime = 2;

    public override void Show()
    {
        base.Show();
        StartCoroutine(SmoothHide());
    }

    private IEnumerator SmoothHide()
    {
        float timer = 0;

        yield return CoroutineUtil.Instance.WaitForSeconds(hideTime);
        while (timer < hideTime)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0,  timer / hideTime);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
