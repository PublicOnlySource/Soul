using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LockOn : MonoBehaviour
{
    [SerializeField]
    private Detector detector;
    [SerializeField]
    private float disableLockDistance;

    private bool isLockOnKeyPress;
    private bool isRotateEnable;
    private float playerToTargetDistance;
    private GameObject mainCameraObj;
    private Transform lockOnTarget;
    private Vector3 rotate;

    public bool IsRotateEnable { get => isRotateEnable; set => isRotateEnable = value; }

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Player_void_disableLockon, Reset);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Player_void_disableLockon, Reset);
    }

    private void Start()
    {
        mainCameraObj = FollowCameraComponent.Instance.gameObject;
        Reset();
    }

    private void Update()
    {
        if (!isLockOnKeyPress && InputManager.Instance.IsLockOn)
        {
            isLockOnKeyPress = true;

            if (lockOnTarget == null)
            {
                FindTarget();
            }
            else
            {
                Reset();
            }
        }

        if (isLockOnKeyPress && !InputManager.Instance.IsLockOn)
        {
            isLockOnKeyPress = false;
        }

        if (lockOnTarget == null)
            return;

        UpdateRotate();
        UpdateLockOnImage();

        if (!IsTargetInRadius()) Reset();
    }

    private void UpdateRotate()
    {
        if (!isRotateEnable)
            return;

        rotate = Quaternion.LookRotation(lockOnTarget.position - transform.position).eulerAngles;
        rotate.x = 0;

        transform.rotation = Quaternion.Euler(rotate);
    }

    private void Reset()
    {
        EventManager.Instance.TriggerEvent<bool>(Enums.EventType.UI_bool_setActiveLockOnImage, false);

        if (lockOnTarget == null)
            return;

        lockOnTarget = null;
        FollowCameraComponent.Instance.DisableLockOn();
    }

    private void FindTarget()
    {
        detector.OnceDetecting();

        if (detector.DetectObject == null)
        {
            isLockOnKeyPress = false;
            FollowCameraComponent.Instance.ResetRotationByPlayerView();
            return;
        }

        if (detector.DetectObject.transform.parent.TryGetComponent(out MonsterController mc))
        {
            if (mc.IsDead)
                return;
        }

        lockOnTarget = detector.DetectObject.transform;
        IsRotateEnable = true;
        FollowCameraComponent.Instance.EnableLockOn(lockOnTarget);
    }

    private bool IsTargetInRadius()
    {
        playerToTargetDistance = Vector3.Magnitude(transform.position - lockOnTarget.transform.position);

        return playerToTargetDistance <= disableLockDistance;
    }

    private void UpdateLockOnImage()
    {
        EventManager.Instance.TriggerEvent<bool>(Enums.EventType.UI_bool_setActiveLockOnImage, true);
        EventManager.Instance.TriggerEvent<Vector3>(Enums.EventType.UI_vector3_setPositionLockOnImage, Camera.main.WorldToScreenPoint(lockOnTarget.position));
    }

    public bool IsEnable()
    {
        return lockOnTarget != null;
    }

    public Vector3 GetDirection()
    {
        return (lockOnTarget.transform.position - transform.position).normalized;
    }
}
