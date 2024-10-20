using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject
{
    private GameObject originalObj;
    private Transform parent;
    private Queue<GameObject> queue = new Queue<GameObject>();

    public void Create(int count, GameObject target, Transform parent)
    {
        originalObj = target;
        this.parent = parent;

        GameObject obj = null;

        for (int i = 0; i < count; i++)
        {
            obj = GameObject.Instantiate(target, parent);
            obj.name = target.name;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }


    public GameObject Pop()
    {
        if (queue.Count == 0)
            Create(1, originalObj, parent);

        return queue.Dequeue();
    }

    public void Push(GameObject obj)
    {
        obj.transform.SetParent(parent);
        obj.SetActive(false);
        queue.Enqueue(obj);
    }

    public void Dispose()
    {
        GameObject.Destroy(parent.gameObject);
    }
}

