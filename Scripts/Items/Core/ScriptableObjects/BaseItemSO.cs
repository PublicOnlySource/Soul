using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemSO : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string name;
    [Multiline]
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private GameObject prefab;

    public int Id { get => id;}
    public string Name { get => name; }
    public string Description { get => description; }
    public Sprite Icon { get => icon; }
    public GameObject Prefab { get => prefab; }

    public abstract BaseItem CreateItem();
}
