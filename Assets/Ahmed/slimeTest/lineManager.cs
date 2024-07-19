using UnityEngine;

public class lineManager : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3 target;
    Transform start;
    public void setTaget(Vector3 target, Transform start)
    {

        this.target = target;
        this.start = start;

    }
    public void ditatch( Transform start)
    {

       
        this.start = start;

    }
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {

            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, target);
        }

    }
}
