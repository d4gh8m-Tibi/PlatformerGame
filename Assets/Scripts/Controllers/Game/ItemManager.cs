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

    private void Start()
    {
        GameController.instance.SetItemMangerInstace();
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
        var stars = FindObjectsOfType(typeof(Collectable)) as Collectable[];
        foreach (var item in stars)
        {
            Debug.Log(item.gameObject.name);
            item.OnLoadInit();
        }
    }

}
