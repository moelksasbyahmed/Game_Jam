using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class dropedItemsSaver : MonoBehaviour
{
    savedDroppedItems theLoadedItems;
    int oldChildCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load());
        oldChildCount = transform.childCount;
        
    }
    bool finishedLoading = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }

        if(transform.childCount != oldChildCount &&  finishedLoading)
        {
            StartCoroutine(save());
            oldChildCount = transform.childCount;
        }
        
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(0.0001f);
        // GameObject Inev = Instantiate(InventoryPrefab, GameObject.Find("Viewport").transform);
        //stop the above tempo as it is for the full inventory instantuation at the end and even not here, but in the main mind

        // u need to replace the below loop for the new (shape bassed inventory grid using the load image pixel) // done in the file pixelImage Loader
        //for (int i = 0; i < _inventory.maxitems; i++)
        //{

        //    Instantiate(slotPrefab, Inev.transform.GetChild(0));
        //}
        Debug.Log("im heree");
        try
        {

        theLoadedItems = JsonUtility.FromJson<savedDroppedItems>(File.ReadAllText(Path.Combine(Application.dataPath, "Resources/" + transform.name + ".txt")));
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            finishedLoading = true;
            yield break;
        }
        foreach (broDropped ok in theLoadedItems.items)
        {
            Debug.Log("whaeree??");
            GameObject temp = Instantiate(allTheItems.allitems[ok.ID].worldPrefabe, transform,true);
            temp.transform.position = ok.position;
            Debug.Log(ok.position);
        }

        finishedLoading = true;
    }



    IEnumerator save()
    {
        yield return new WaitForSeconds(0.1f);
        List<(int, Vector2)> __ite = new List<(int, Vector2)>();
        items[] hi = transform.GetComponentsInChildren<items>();
        foreach (items item in hi)
        {
            Debug.Log(item.gameObject);
            Debug.Log(" the item position and name or the opposite" + item.transform.name + item.transform.position);
            __ite.Add((item.data.ID,item.transform.position)); //save the world position of the world item
        }
        savedDroppedItems savedDroppedItems = new savedDroppedItems(__ite);
        string json = JsonUtility.ToJson(savedDroppedItems);



        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/" + transform.name + ".txt"), json);



        // PlayerInventory._items = new item(__ite);
    }
}
[Serializable]
public class savedDroppedItems
{

    public List<broDropped> items = new List<broDropped>();
    public savedDroppedItems(List<(int, Vector2)> input)
    {
        items.Clear();
        foreach ((int, Vector2) ins in input)
        {
            items.Add(new broDropped(ins.Item1, ins.Item2));


        }

    }
}

[Serializable]
public class broDropped
{ 
    public int ID;
    public Vector2 position;
    public broDropped(int id, Vector2 position)
    {
        ID = id;
        this.position = position;
    }

}
