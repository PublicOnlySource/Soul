using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseItem
{
    private BaseItemSO itemSO;

    public BaseItem(BaseItemSO item)
    {
        this.itemSO = item;
    }

    public BaseItemSO ItemSO { get => itemSO; }
}
