using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectManager : Singleton<EffectManager>
{
    public void Init()
    {
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_EFFECT_HIT, 1);
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_EFFECT_UPGRADE, 1);
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_EFFECT_BLOCK, 1);
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_EFFECT_PARRY, 1);
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_EFFECT_DRINK_POTION, 1);
    }

    private GameObject GetEffectObj()
    {
        return null;
    }

    public void ShowEffect(string name, Vector3 position)
    {
        GameObject obj = PoolingManager.Instance.Pop(name);
        obj.transform.position = position;

        obj.SetActive(true);
    }

    public void ShowEffect(string name, Vector3 position, Quaternion rotation)
    {
        GameObject obj = PoolingManager.Instance.Pop(name);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        obj.SetActive(true);
    }

    public void ShowEffect(string name, Vector3 position, Transform parent)
    {
        GameObject obj = PoolingManager.Instance.Pop(name);
        obj.transform.SetParent(parent, false);
        obj.transform.position = position;

        obj.SetActive(true);
    }

}
