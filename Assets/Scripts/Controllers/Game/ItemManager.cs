using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    private List<string> collectedItems = new List<string>();
    private void Awake()
    {
        instance = this;
    }

    public void Add(string id)
    {
        collectedItems.Add(id);
    }

    public int Count
    {
        get { return collectedItems.Count; }
    }

    public string ElementAt(int i)
    {
        return collectedItems[i];
    }

    public void LoadItemsOnLoad()
    {

    }

}
