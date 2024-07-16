using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//-----------------------------------------------------------------------------
//the slot must have a slot tag and the items must have ok tag
public class DragAndDrop : MonoBehaviour
{
    //tehse for the moving betwean inventory process
    //the item holder inside of each inventory must be the last item, so every instantia of a slot needto be sibling index 0
    public Transform theMainContentViewer;
    public Transform itemTransfareHolder; // for putting the item in while transfaring u need tomake it the last item in the inventory viewr, whcih means eevery inventory u instantaite it needs to be sibking index 0
    Transform oldParent;



    //end here
    public static float step;
    /// the squar width and height

    public float Rstep = 50;
    // Start is called before the first frame update
    void Start()
    {

        step = Rstep * (GameObject.Find("Canvas").transform.localScale.x);
        Debug.Log(Rstep);
    }
    void redoTheParent(Transform item)
    {
        item.SetParent(oldParent, true);


    }
    bool world = false;
    // Update is called once per frame
    public GraphicRaycaster raycaster;
    IEnumerator FollowMouse(Transform taget, Vector3 toSub)
    {
        while (!Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.x < widthFitter.mostLeft && !world)
            {
                //add later an object in the outside world to store the objects that are on the floor  change ------------------------ important
                Transform ds = Instantiate(taget.GetComponent<items>().data.worldPrefabe).transform;
                Destroy(taget.gameObject);
                taget = ds;
                world = true;
                yield return null;
            }
            else if (world && Input.mousePosition.x >= widthFitter.mostLeft)
            {
                Transform ds = Instantiate(taget.GetComponent<items>().data.UIprefabe, itemTransfareHolder).transform;
                Destroy(taget.gameObject);
                taget = ds;
                world = false;
                taget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3 { z = 3 });

                yield return null;

            }
            if (world)
            {
                taget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3 { z = 3 });
            }
            else
            {

                taget.position = Input.mousePosition;
            }
            //Debug.Log(Input.mousePosition.x);
            //Debug.Log("sdsd   " + widthFitter.mostLeft);
            yield return null;

        }
        if (world)
        {
            yield break;
        }
        List<RaycastResult> results2 = new List<RaycastResult>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        raycaster.Raycast(eventData, results2);
        Vector2 result;
        if (results2.Count > 1 && results2[1].gameObject.CompareTag("slot"))
        {
            results2[0].gameObject.GetComponent<Image>().raycastTarget = false;

            if (doRayCast(results2[1].gameObject.transform.position, 0))
            {


                Debug.Log(results2[1].gameObject.name);
                if (check(results2[0].gameObject.GetComponent<items>().data.dimention, results2[1].gameObject.transform.position, out result))
                {//                                                                       the slot ^

                    // taget.position = results2[1].gameObject.transform.position - toSub;
                    taget.SetParent(results2[1].gameObject.transform.parent.GetChild((results2[1].gameObject.transform.parent.childCount - 1)));
                    //u stopped here
                    taget.position = result;
                    yield return null;
                    results2[1].gameObject.transform.parent.GetComponent<inventoryHolder>().save();


                    Debug.Log(result);

                }
                else
                {
                    taget.position = results[1].gameObject.transform.position - toSub;
                    //no enough space
                    redoTheParent(taget);
                    yield return null;
                    results[1].gameObject.transform.parent.GetComponent<inventoryHolder>().save();
                }


            }
            else
            {
                taget.position = results[1].gameObject.transform.position - toSub;
                redoTheParent(taget);

                yield return null;
                results[1].gameObject.transform.parent.GetComponent<inventoryHolder>().save();
                //there are no slot
                Debug.Log("heeeeerereeee");
            }
        }
        else
        {
            //there are no slot
            taget.position = results[1].gameObject.transform.position - toSub;
            redoTheParent(taget);

            yield return null;
            results[1].gameObject.transform.parent.GetComponent<inventoryHolder>().save();
            Debug.Log("heeeeerereeeessedwe2");

        }
        try
        {

            results2[0].gameObject.GetComponent<Image>().raycastTarget = true;
        }
        catch (Exception e)
        {
            //it is ouside the pannal (maybe if u want to add the drop functionality like this)
            Debug.LogWarning(e);
        }


    }

    static List<RaycastResult> results = new List<RaycastResult>();


    //and make a function that do a raycast ata specific opsition and returns the ful list
    List<RaycastResult> doRayCast(Vector2 position)
    {
        List<RaycastResult> result = new List<RaycastResult>();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        raycaster.Raycast(eventData, result);

        return result;



    }
    bool doRayCast(Vector2 position, int x)
    {
        List<RaycastResult> result = new List<RaycastResult>();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;

        raycaster.Raycast(eventData, result);
        if (result.Count > 1)
        {
            Debug.Log($"{result[x].gameObject.CompareTag("slot")}  the name of the object that was hit {result[x].gameObject.name}");
            Debug.Log(eventData.position);
            return result[x].gameObject.CompareTag("slot");
        }
        else
            return false;
    }
    public bool right, left, up, down;
    bool check(Vector2 dimensions, Vector2 Newslot, out Vector2 result)
    {
        result = Vector2.zero;
        Debug.Log(Newslot);
        Debug.Log(dimensions);
        //bool x = true; //positive x

        //for (int i = 1; i < dimensions.x; i++)
        //{
        //    Newslot.x += step;

        //    if (!doRayCast(Newslot, 0).CompareTag("slot"))
        //    {
        //        x = false;

        //        break;

        //    }

        //}
        //Newslot.x -= step * (dimensions.x - 1); //reset the position for the original position
        //bool x_ = true; // negative x

        //for (int i = 1; i < dimensions.x; i++)
        //{
        //    Newslot.x -= step;

        //    if (!doRayCast(Newslot, 0).CompareTag("slot"))
        //    {
        //        x_ = false;

        //        break;

        //    }

        //}

        //Newslot.x += step * (dimensions.x - 1); //reset the position for the original position




        //if (!x && !x_)
        //{
        //    return results[1].gameObject.transform.position; //moving failed, no enough space
        //}


        //// this was the horizental check, to the vertical check now



        //bool y = true; //positive y

        //for (int i = 1; i < dimensions.y; i++)
        //{
        //    Newslot.y += step;

        //    if (!doRayCast(Newslot, 0).CompareTag("slot"))
        //    {
        //        y = false;

        //        break;

        //    }

        //}
        //Newslot.y -= step * (dimensions.y - 1); //reset the position for the original position
        //bool y_ = true; // negative y

        //for (int i = 1; i < dimensions.y; i++)
        //{
        //    Newslot.y -= step;

        //    if (!doRayCast(Newslot, 0).CompareTag("slot"))
        //    {
        //        y_ = false;

        //        break;

        //    }

        //}

        //Newslot.y += step * (dimensions.y - 1); //reset the position for the original position




        //if (!y && !y_)
        //{
        //    return results[1].gameObject.transform.position; //moving failed, no enough space
        //}

        //bool multyx,multyy;
        ////this was the main parts, for the secondary one now --
        //int stepx = step;
        //int stepy = step;
        //if (x && !x_)
        //    stepx = step;
        //else if (!x && x_)
        //    stepx = -step;
        //else
        //    multyx = true;

        //if (y && !y_)
        //    stepy = step;
        //else if (!y && y_)
        //    stepy = -step;
        //else
        //    multyy = true;

        //bool found = true;
        //for (int i = 1; i < dimensions.x; i++)
        //{
        //    for (int j = 1; j < dimensions.y; j++)
        //    {
        //        if(!doRayCast(new Vector2 { x = Newslot.x + stepx * i, y = Newslot.y + stepy * j }, 0).CompareTag("slot"))
        //        {
        //            found = false;
        //            break;

        //        }
        //    }
        //}





        //// approach of checking all the surroundings as circles getting larger
        //Vector2 direction = Vector2.right;
        right = false; left = false;
        up = false; down = false;
        //Vector2[] maxes = new Vector2[4]; // max: down-right , top-right, top-left, down-left

        ////you can optimize it more to making it skip the whole the checking like jump overthem instantly if direction is true
        //for (int i = 0; ; i++)
        //{
        //    if (!doRayCast(Newslot += Vector2.right * step, 0).CompareTag("slot"))
        //    {
        //        right = true; // dont check right 
        //    }
        //    else
        //    {
        //        maxes[0] = Newslot;
        //    }
        //    for (int j = 0;j< (1+2*i);j++)
        //    {
        //        if(!right)
        //        {
        //            if (!doRayCast(Newslot += Vector2.up * step, 0).CompareTag("slot"))
        //            {
        //                right = true; // dont check right 
        //            }
        //            else
        //            {
        //                maxes[1] = Newslot;
        //            }


        //        }
        //    }
        //    for (int j = 0; j < 2 + 2 * i; j++)
        //    {
        //        if (!up)
        //        {
        //            if (!doRayCast(Newslot += Vector2.left * step, 0).CompareTag("slot"))
        //            {
        //                up = true; // dont check up 
        //            }



        //        }
        //    }
        //    for (int j = 0; j < 2 + 2 * i; j++)
        //    {
        //        if (!left)
        //        {
        //            if (!doRayCast(Newslot += Vector2.down * step, 0).CompareTag("slot"))
        //            {
        //                left = true; // dont check left  
        //            }



        //        }
        //    }
        //    for (int j = 0; j < 2 + 2 * i; j++)
        //    {
        //        if (!down)
        //        {
        //            if (!doRayCast(Newslot += Vector2.right * step, 0).CompareTag("slot"))
        //            {
        //                down = true; // dont check down 
        //            }



        //        }
        //    }


        //}

        int x = 1;
        int y = 0;
        float maxRight = 0f;
        float maxTop = 0f;
        float maxLeft = 0f;
        float maxDown = 0f;

        Newslot += new Vector2 { x = step / 2, y = step / 2 };
        //this approach search in each direction if not found it revers it whith the ability to add to what it did
        Vector2 orignalSlot = Newslot;

        Debug.Log(orignalSlot);
        if (!doRayCast(orignalSlot, 0))
        {
            return false;
        }
        for (int i = 1; i <= dimensions.x; i++)
        {
            Debug.Log("hereee");
            if (x >= dimensions.x)
            {
                maxRight = (Newslot + Vector2.right * step * (i - 1)).x;
                Debug.Log(maxRight);

                break;

            }
            Debug.Log("01");
            if (!doRayCast(Newslot + Vector2.right * step * i, 0))
            {
                right = true;
                maxRight = (Newslot + Vector2.right * step * (i - 1)).x;
                Debug.Log(maxRight);


                break;

            }
            x++;

        }
        maxLeft = Newslot.x;
        if (right)
        {
            for (int i = 1; i <= dimensions.x; i++)
            {
                if (x >= dimensions.x)
                {
                    maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;

                    break;

                }
                Debug.Log("02");

                if (!doRayCast(Newslot + Vector2.left * step * i, 0))
                {
                    left = true;
                    maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;
                    break;

                }
                x++;

            }

        }
        Debug.Log($"the ;middle; right {right} up {up} down {down} left {left} middle ");

        if (right && left)
        {
            up = true;
            Debug.Log($"the ;middle; right {right} up {up} down {down} left {left} middle ");

            right = false;
            left = false;
            return false;
        }
        right = false;
        left = false;

        y++;


        for (int j = 1; j <= dimensions.y; j++)
        {
            x = 1;


            if (y >= dimensions.y)
            {
                maxTop = (Newslot).y;

                break;

            }
            Debug.Log("03");

            if (!doRayCast(Newslot += Vector2.up * step, 0))
            {
                up = true;
                maxTop = (Newslot - Vector2.up * step).y;
                break;

            }



            for (int i = 1; i <= dimensions.x; i++)
            {
                if (Mathf.Abs((Newslot + Vector2.right * step * (i)).x) > Mathf.Abs(maxRight))
                {
                    break;
                }



                if (x >= dimensions.x)
                {
                    maxRight = (Newslot + Vector2.right * step * (i - 1)).x; //test

                    break;

                }
                Debug.Log($"j  {j}   04");
                Debug.Log(Newslot + Vector2.right * step * i);
                Debug.Log("the logeofds");
                if (!doRayCast(Newslot + Vector2.right * step * i, 0))
                {
                    right = true;
                    maxRight = (Newslot + Vector2.right * step * (i - 1)).x;

                    break;

                }
                x++;

            }
            if (right)
            {
                for (int i = 1; i <= dimensions.x; i++)
                {
                    if (Mathf.Abs((Newslot + Vector2.left * step * (i)).x) > Mathf.Abs(maxLeft))
                    {
                        break;
                    }
                    if (x >= dimensions.x)
                    {
                        maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;

                        break;

                    }
                    Debug.Log("05");

                    if (!doRayCast(Newslot + Vector2.left * step * i, 0))
                    {
                        left = true;


                        maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;
                        //stopped here
                        break;

                    }
                    x++;

                }

            }
            Debug.Log($"the ;up; right {right} up {up} down {down} left {left} j {j} ");

            if (right && left)
            {
                up = true;
                Debug.Log($"the ;up; right {right} up {up} down {down} left {left} j {j} ");
                maxTop = (Newslot - Vector2.up * step).y;
                right = false;
                left = false;
                break;
            }
            right = false;
            left = false;

            y++;
        }

        right = false;
        left = false;
        maxDown = orignalSlot.y;
        if (up)
        {
            Newslot = orignalSlot;
            for (int j = 1; j <= dimensions.y; j++)
            {
                x = 1;

                if (y >= dimensions.y)
                {
                    maxDown = (Newslot).y;

                    break;

                }
                Debug.Log("06");

                if (!doRayCast(Newslot += Vector2.down * step, 0))
                {
                    down = true;
                    maxDown = (Newslot - Vector2.down * step).y;
                    break;

                }



                for (int i = 1; i <= dimensions.x; i++)
                {
                    if (Mathf.Abs((Newslot + Vector2.right * step * (i)).x) > Mathf.Abs(maxRight))
                    {
                        break;
                    }
                    if (x >= dimensions.x)
                    {
                        maxRight = (Newslot + Vector2.right * step * (i - 1)).x;

                        break;

                    }
                    Debug.Log("07");

                    if (!doRayCast(Newslot + Vector2.right * step * i, 0))
                    {

                        Debug.Log($"in j {j} the right was closed");

                        right = true;
                        maxRight = (Newslot + Vector2.right * step * (i - 1)).x;

                        break;

                    }
                    x++;

                }
                if (right)
                {
                    Debug.Log($"in j {j} the right was closed");
                    for (int i = 1; i <= dimensions.x; i++)
                    {
                        if (Mathf.Abs((Newslot + Vector2.left * step * (i)).x) > Mathf.Abs(maxLeft))
                        {
                            break;
                        }
                        if (x >= dimensions.x)
                        {
                            maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;

                            break;

                        }
                        Debug.Log($" j {j}  08");

                        if (!doRayCast(Newslot + Vector2.left * step * i, 0))
                        {
                            left = true;
                            maxLeft = (Newslot + Vector2.left * step * (i - 1)).x;

                            Debug.Log($"in j {j} the left was closed");

                            break;

                        }
                        x++;

                    }

                }
                Debug.Log($"the ;down; right {right} up {up} down {down} left {left} j {j} ");
                if (right && left)
                {
                    down = true;
                    Debug.Log($"the ;down; right {right} up {up} down {down} left {left} j {j} ");
                    maxDown = (Newslot - Vector2.down * step).y;

                    right = false;
                    left = false;
                    break;
                }
                right = false;
                left = false;
                y++;
            }


        }

        Debug.Log($" the maxieess  right {maxRight} left {maxLeft} top {maxTop} down {maxDown} ");
        if (up && down)
        {
            return false;
        }
        else
        {
            result = new Vector2 { x = (maxRight + maxLeft) / 2, y = (maxTop + maxDown) / 2 };
            return true;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            results.Clear();
            raycaster.Raycast(eventData, results);

            if (results.Count > 1)
            {
                GameObject hit = results[0].gameObject;
                Vector3 toSub = results[1].gameObject.transform.position - hit.transform.position; // slot - item
                Debug.Log("Hit UI element: " + hit.name);
                if (hit.CompareTag("ok") && results[1].gameObject.CompareTag("slot"))
                { //the item   ^                    the slot ^
                    world = false;
                    oldParent = hit.transform.parent;
                    hit.transform.SetParent(itemTransfareHolder, true); // temprorarly putting it there
                    results[1].gameObject.transform.parent.GetComponent<inventoryHolder>().save();
                    StartCoroutine(FollowMouse(hit.transform, toSub));
                }
               

                // Perform actions based on the hit UI element
            }
            else
            {
                //based on the hit world element

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    Debug.Log("hit in the 2d World "+ hit.collider.name);
                    StartCoroutine(FollowMouse(hit.transform,Vector3.zero));

                }

                
            }
        }
    }
}
