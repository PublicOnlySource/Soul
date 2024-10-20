using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Debug = ccm.Debug;

public class ResourceManager : Singleton<ResourceManager>
{

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Load(string path)
    {
        return Load<GameObject>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject obj = Load(path);

        if (obj == null)
        {
            Debug.Log("리소스가 존재하지 않습니다. -> " + path);
            return null;
        }

        return GameObject.Instantiate(obj, parent);
    }

    public T Instantiate<T>(string path, Transform parent = null)
    {
        GameObject obj = Instantiate(path, parent);

        if (!obj.TryGetComponent(out T t))
        {
            Debug.Log("컴포넌트가 존재하지 않습니다. -> " + typeof(T).FullName);
            return default;
        }

        return t;
    }
}

