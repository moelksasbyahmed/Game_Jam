using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightchanger: MonoBehaviour
{
    Transform parent;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.parent;
        GetComponent<Light2D>().pointLightOuterRadius = (0.75f * parent.localScale.x) / 6;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
