using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField]
    private Transform rayStartObj;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private float radius;

    [Header("Setting - Angle")]
    [SerializeField]
    private bool useAngle;
    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle; 

    [Header("Debug")]
    [SerializeField]
    private bool viewCheckLine;
    [SerializeField]
    private float lineLength = 5f;
    [SerializeField]
    private bool viewAngle;
    [SerializeField]
    private bool viewRedius;

    private GameObject detectObject;
    private Coroutine coroutine;
    private Collider[] hitColliders;
    private float minDistance;
    private GameObject nearDetectObject;

    public GameObject DetectObject { get => detectObject; }
    public bool IsDetect { get => detectObject != null; }

    private void OnDisable()
    {
        Clear();
    }

    private IEnumerator Detecting()
    {
        while (true)
        {
            Detect();

            if (nearDetectObject != null)
            {
                detectObject = nearDetectObject;
                DisableDetect();
            }

            yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.UPDATE_SECOND_0_01);
        }       
    }

    private void Detect()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        minDistance = float.MaxValue;
        nearDetectObject = null;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 directionToTarget = hitColliders[i].transform.position - transform.position;

            if (useAngle && !CheckAngle(directionToTarget))
                continue;

            if (CheckObstacle(hitColliders[i].transform))
                continue;

            float distance = directionToTarget.magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearDetectObject = hitColliders[i].gameObject;
            }
        }
    }

    private bool CheckAngle(Vector3 direction)
    {
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        if (angle >= minAngle && angle <= maxAngle)
            return true;

        return false;
    }

    private bool CheckObstacle(Transform target)
    {
        if (Physics.Linecast(rayStartObj.position, target.position, out RaycastHit hit, obstacleLayer))
        {
            if (hit.collider != null)
                return true;
        }

        return false;
    }

    public void EnableDetect()
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(Detecting());
    }

    public void DisableDetect()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void Clear()
    {
        detectObject = null;
        DisableDetect();
    }

    public void OnceDetecting()
    {
        Detect();

        if (nearDetectObject != null)
        {
            detectObject = nearDetectObject;
        }
    }

    private void OnDrawGizmos()
    {
        DrawCheckLine();
        DrawRedius();
        DrawAngle();
    }

    private void DrawCheckLine()
    {
        if (!viewCheckLine || detectObject == null || rayStartObj == null)
            return;

        Gizmos.DrawLine(rayStartObj.position, detectObject.transform.position);
    }

    private void DrawRedius()
    {
        if (!viewRedius)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void DrawAngle()
    {
        if (!viewAngle) 
            return;

        Gizmos.color = Color.red;
        Vector3 minAngleDir = Quaternion.Euler(0, minAngle, 0) * transform.forward;
        Vector3 maxAngleDir = Quaternion.Euler(0, maxAngle, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + minAngleDir * radius);
        Gizmos.DrawLine(transform.position, transform.position + maxAngleDir * radius);
    }
    
}
