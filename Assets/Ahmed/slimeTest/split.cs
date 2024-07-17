using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class split : MonoBehaviour
{
    public static int numberOfSplitestsLeft =3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && numberOfSplitestsLeft >0)
        {
            numberOfSplitestsLeft--;


        }
        
    }
}
