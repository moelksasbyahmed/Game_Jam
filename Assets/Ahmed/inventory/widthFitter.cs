using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class widthFitter : MonoBehaviour
{
    public Transform Content;
    public float tolarence = 25;
    public static float mostLeft;

    // Start is called before the first frame update
    void Start()
    {
        PixelImageLoader[] inventories = Content.GetComponentsInChildren<PixelImageLoader>();
        float maxWidth = 0;
        foreach (PixelImageLoader inventory in inventories)
        {
            if(inventory.inventory.texture.width > maxWidth)
            {
                maxWidth = inventory.inventory.texture.width;
     
            }
        }
            // change the 50
            maxWidth *= 50  * (GameObject.Find("Canvas").transform.localScale.x);
        Debug.Log(GetComponent<RectTransform>().position.x);
        Debug.Log(GetComponent<RectTransform>().sizeDelta.x / 2);

       mostLeft =  GetComponent<RectTransform>().position.x + GetComponent<RectTransform>().sizeDelta.x / 2;
        GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth + tolarence , 321.8f);
        GetComponent<RectTransform>().localPosition = new Vector2 { x = maxWidth + tolarence/ 2, y = -43.9f };

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
