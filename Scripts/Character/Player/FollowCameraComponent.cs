using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraComponent : Singleton<FollowCameraComponent>
{
    [Header("Object Setting")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform cameraPivotTransform;

    [Header("Camera Setting")]
    [SerializeField]
    private float leftAndRightRotationSpeed = 220;
    [SerializeField]
    private float upAndDownRotationSpeed = 220;
    [SerializeField]
    private float minimumPivot = -30;
    [SerializeField]
    private float maximumPivot = 60;
    [SerializeField]
    private float cameraCollisionRadius = 0.2f;
    [SerializeField]
    private float cameraSmoothSpeed = 1;
    [SerializeField]
    private LayerMask collideWithLayers;

    [Header("LockOn Setting")]
    [SerializeField]
    private float lockOnRotationSpeed = 10;
    [SerializeField]
    private float lockOnUpDownOffset = 0f;
    [SerializeField]
    private float lockOnMinimumPivot = -30;
    [SerializeField]
    private float lockOnMaximumPivot = 60;

    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    private float upAndDownLookAngle;
    private float leftAndRightLookAngle;
    private float defaultCameraPositionZ;
    private float targetCameraPositionZ;
    private bool lockRotation;
    private Coroutine resetRotationCoroution;
    private Transform lockOnTarget;

    public Camera CameraObject { get => camera; }

    private void Awake()
    {
        defaultCameraPositionZ = CameraObject.transform.localPosition.z;
    }

    private void LateUpdate()
    {
        UpdateFollowTarget();

        if (!lockRotation) UpdateRotation();
        else UpdateLockRotation();
        UpdateCollisions();
    }

    private void UpdateFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void UpdateRotation()
    {
        if (lockRotation)
            return;

        leftAndRightLookAngle += (InputManager.Instance.CameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle -= (InputManager.Instance.CameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        cameraRotation.y = leftAndRightLookAngle;
        Quaternion targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void UpdateLockRotation()
    {
        if (lockOnTarget == null) return;

        Vector3 directionToTarget = lockOnTarget.position - transform.position;
        directionToTarget.y += lockOnUpDownOffset;

        Quaternion targetRotationY = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationY, Time.deltaTime * lockOnRotationSpeed);

        float targetUpDownAngle = Quaternion.LookRotation(directionToTarget).eulerAngles.x;
        targetUpDownAngle = Mathf.Clamp(targetUpDownAngle, lockOnMinimumPivot, lockOnMaximumPivot);

        Vector3 pivotRotation = cameraPivotTransform.localEulerAngles;
        pivotRotation.x = Mathf.LerpAngle(pivotRotation.x, targetUpDownAngle, Time.deltaTime * lockOnRotationSpeed);
        cameraPivotTransform.localEulerAngles = pivotRotation;
    }

    private void UpdateCollisions()
    {
        targetCameraPositionZ = defaultCameraPositionZ;

        Vector3 direction = camera.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out RaycastHit hit, Mathf.Abs(targetCameraPositionZ), collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraPositionZ = -(distanceFromHitObject - cameraCollisionRadius);
        }

        if (Mathf.Abs(targetCameraPositionZ) < cameraCollisionRadius)
        {
            targetCameraPositionZ = -cameraCollisionRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(camera.transform.localPosition.z, targetCameraPositionZ, 0.2f);
        camera.transform.localPosition = cameraObjectPosition;
    }

    IEnumerator ResetRotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.forward);

        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                leftAndRightLookAngle = transform.rotation.eulerAngles.y;
                resetRotationCoroution = null;
                UnLockRotation();
                yield break;
            }
            yield return null;
        }
    }

    public void ResetRotationByPlayerView()
    {
        LockRotation();

        if (resetRotationCoroution != null)
            return;

        resetRotationCoroution = StartCoroutine(ResetRotate());
    }

    public void LockRotation()
    {
        lockRotation = true;
    }

    public void UnLockRotation()
    {
        lockRotation = false;
    }

    public void EnableLockOn(Transform target)
    {
        lockOnTarget = target;
        lockRotation = true;
    }

    public void DisableLockOn()
    {
        leftAndRightLookAngle = transform.rotation.eulerAngles.y;
        upAndDownLookAngle = cameraPivotTransform.rotation.eulerAngles.x;
        lockOnTarget = null;
        UnLockRotation();
    }
}
