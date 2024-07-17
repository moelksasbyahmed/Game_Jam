using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class split : MonoBehaviour
{
    public  int numberOfSplitestsLeft =3;
    public static GameObject activePlayer;
    public GameObject slime;

    static Transform playersParent;
    // Start is called before the first frame update
    void Start()
    {
        activePlayer = gameObject;
        if(playersParent == null)
        {
            playersParent = transform.parent;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && numberOfSplitestsLeft >0 && activePlayer == gameObject)
        {
            numberOfSplitestsLeft--;


            GameObject newSplitted = Instantiate(slime, transform.GetChild(transform.childCount-1).position, Quaternion.identity,playersParent);
            GameObject newSplitted2 = Instantiate(slime, transform.GetChild(transform.childCount - 1).position +  Vector3.right * 5 , Quaternion.identity,playersParent);
           
            
            newSplitted.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;
            newSplitted2.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;

            newSplitted.transform.localScale = activePlayer.transform.localScale / 2;
            newSplitted2.transform.localScale = activePlayer.transform.localScale / 2;
            
            
            activePlayer = newSplitted2;
            
            Destroy(gameObject);
            


        }
        if( Input.GetKeyDown(KeyCode.Tab) && activePlayer == gameObject)
        {
           int newIndex =  activePlayer.transform.GetSiblingIndex()+ 1;
            if(newIndex >= playersParent.childCount)
            {
                newIndex = 0;
            }
            activePlayer = playersParent.GetChild(newIndex).gameObject;

        }
        
    }
}
