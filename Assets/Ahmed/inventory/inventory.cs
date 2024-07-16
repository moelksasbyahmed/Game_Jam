using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new inventory", menuName ="inventory/new inventory")]
[System.Serializable]
public class inventory : ScriptableObject
{

    public Texture2D texture;

   public List<inventoryitem> items  = new List<inventoryitem>(); // idk what was this
    
}
