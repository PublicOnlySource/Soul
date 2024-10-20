using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = ccm.Debug;

public class SOManager : Singleton<SOManager>
{
    private Dictionary<int, BaseItemSO> itemSODic= new Dictionary<int, BaseItemSO>();

    public void Init()
    {     
    }

    private void InitData<T1,T2>(string path, Dictionary<int, T2> dic) where T1 : BaseItemSO where T2 : BaseItemSO
    {
        T1[] datas = Resources.LoadAll<T1>(path);

        foreach (T1 data in datas)
        {
            dic.Add(data.Id, data as T2);
        }
    }

    public T GetItemSO<T>(int id) where T : BaseItemSO
    {
        BaseItemSO data = GetItemSO(id);

        if (data is T)
            return data as T;

        return null;
    }

    public BaseItemSO GetItemSO(int id)
    {
        if (!itemSODic.TryGetValue(id, out BaseItemSO data))
        {
            Debug.Log($"{id} 데이터가 존재하지 않습니다.");
            return null;
        }

        return data;       
    }
} 