using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = ccm.Debug;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<string, PoolingObject> dic = new Dictionary<string, PoolingObject>();

    private readonly string ADD_POOLNAME = "_Pool";

    public void CreatePool(string objName, int count)
    {
        if (dic.TryGetValue(objName, out PoolingObject poolingObject))
            return;

        Transform parent = new GameObject(objName + ADD_POOLNAME).transform;
        parent.SetParent(this.transform);
        GameObject addressableObj = AddressableManager.Instance.GetPrefab(objName);

        poolingObject = new PoolingObject();
        poolingObject.Create(count, addressableObj, parent);
        dic.Add(objName, poolingObject);
    }

    public void CreatePool(string poolName, GameObject obj, int count)
    {
        if (dic.TryGetValue(poolName, out PoolingObject poolingObject))
            return;

        Transform parent = new GameObject(poolName + ADD_POOLNAME).transform;
        parent.SetParent(this.transform);

        poolingObject = new PoolingObject();
        poolingObject.Create(count, obj, parent);
        dic.Add(poolName, poolingObject);
    }

    public void Push(string poolName, GameObject obj)
    {
        if (!dic.TryGetValue(poolName, out PoolingObject poolingObject))
        {
            Debug.Log("해당 오브젝트는 풀링이 적용되지 않았습니다.");
            return;
        }

        poolingObject.Push(obj);
    }

    public GameObject Pop(string poolName)
    {
        if (!dic.TryGetValue(poolName, out PoolingObject poolingObject))
        {
            CreatePool(poolName, 1);
            poolingObject = dic[poolName];
        }

        return poolingObject.Pop();
    }

    public GameObject Pop(string poolName, bool isShow)
    {
        GameObject obj = Pop(poolName);
        obj.SetActive(isShow);

        return obj;
    }

    public T Pop<T>(string poolName)
    {
        return Pop(poolName).GetComponent<T>();
    }

    public T Pop<T>(string poolName, bool isShow)
    {
        return Pop(poolName, isShow).GetComponent<T>();
    }

    public void Delete(string poolName)
    {
        if (!dic.TryGetValue(poolName, out PoolingObject poolingObject))
            return;

        poolingObject.Dispose();
        dic.Remove(poolName);
    }

}