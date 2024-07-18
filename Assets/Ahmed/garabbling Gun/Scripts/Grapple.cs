using System;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    Camera cam;
    public GameObject lrPrefab;

    // LineRenderer lr;
    public LayerMask grappleMask;
    public float grappleLength = 5;
    [Min(1)]
    public int maxPoints = 3;

    private Rigidbody2D rig;
    private List<Vector2> points = new List<Vector2>();

    private void Start()
    {

        cam = Camera.main;
        rig = GetComponent<Rigidbody2D>();

    }

    public float dampingRatio, frequincy,distance;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2) && transform.parent.GetComponent<split>().numberOfSplitestsLeft >0)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleMask);
            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                points.Add(hitPoint);
                //adding the joint to the player and connecting it 
                SpringJoint2D xx = gameObject.AddComponent<SpringJoint2D>();

                xx.connectedBody = hit.transform.GetComponent<Rigidbody2D>();
                xx.autoConfigureConnectedAnchor = false;
                xx.connectedAnchor = hit.transform.InverseTransformPoint(hitPoint);
                xx.autoConfigureDistance = false;
                xx.distance = distance;
                xx.dampingRatio = dampingRatio;
                xx.frequency = frequincy;

                springJoint2Ds.Add(xx);

                //add the line renderer to the world
                lineManager lm = Instantiate(lrPrefab).GetComponent<lineManager>();
                lm.setTaget(hitPoint, transform);
                lines.Add(lm);


                if (points.Count > maxPoints)
                {
                    Detatch();
                }
            }
        }



        if (Input.GetMouseButtonDown(1) && points.Count > 0)
        {
            Detatch();
        }
    }
    List<SpringJoint2D> springJoint2Ds = new List<SpringJoint2D>();
    List<lineManager> lines = new List<lineManager>();
    public void Detatch()
    {
        points.RemoveAt(0);




        GameObject playerCore = Instantiate(gameObject,transform);
        playerCore.layer = 10;
        playerCore.transform.localPosition = Vector3.zero;
        playerCore.transform.SetParent(null, true);
        try
        {

            playerCore.GetComponent<movement>().enabled = false;
            playerCore.GetComponent<collision>().enabled = false;
            playerCore.GetComponent<Grapple>().enabled = false;
            for (int i = 0; i < playerCore.transform.childCount; i++)
            {

                Destroy(playerCore.transform.GetChild(i).gameObject);
            }

        }
        catch (Exception e) { Debug.LogWarning(e); }






        //destory the component on the player
        Destroy(springJoint2Ds[0]);
        springJoint2Ds.RemoveAt(0);


        //di attach the the line froom the player and creat an isntantiate of the core of the player with disabling the movement and destroying its childs and the set it as target for the rop
        lines[0].ditatch(playerCore.transform);
        lines.RemoveAt(0);
    }

    Vector2 centriod(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction);

        foreach (Vector2 point in points)
        {
            Gizmos.DrawLine(transform.position, point);
        }
    }
}
