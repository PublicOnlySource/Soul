using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BaseHealthBar : UI_BaseSliderBar
{
    private Transform player;
    private Coroutine lookAtCoroutine;

    private void OnEnable()
    {
        if (player != null)
        {
            lookAtCoroutine = StartCoroutine(LookAt());
        }
    }

    private void OnDisable()
    {
        if (lookAtCoroutine != null)
        {
            StopCoroutine(lookAtCoroutine);
            lookAtCoroutine = null;
        }
    }

    private IEnumerator LookAt()
    {
        Vector3 lookAtPosition;

        while (true)
        {
            lookAtPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookAtPosition);
            transform.Rotate(0, 180, 0);
            yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.UPDATE_SECOND_0_01);
        }
    }

    public void EnableLookAt()
    {
        player = InGameManager.Instance.PlayerController.transform;
        lookAtCoroutine = StartCoroutine(LookAt());
    }

    public void SetHealthDataEvent(HealthData healthData)
    {
        healthData.updateHealthBarEvent = UpdateSliderBar;
        healthData.changeHealthBarMaxEvnet = UpdateSliderBarMax;
        healthData.setActiveHealthBarEvent = SetActive;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

}
