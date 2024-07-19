using UnityEngine;

public class movingGround : MonoBehaviour
{
    public float speed;
    public float range;
    [Tooltip("if horizontal is true then the ground will be horizontally if not then it will move vertically")]
    public bool horizontal;
    Vector3 position;
    public float waitingTime;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (Mathf.Abs(Vector3.Magnitude(transform.position - position)) >= range)
        {
            speed = -speed;
            position = transform.position;
            time = Time.time;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - time >= waitingTime)
        {

            if (horizontal)
            {
                transform.position += new Vector3 { x = speed * Time.deltaTime };
            }
            else
            {
                transform.position += new Vector3 { y = speed * Time.deltaTime };

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent.parent = transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent.parent = null;
        }

    }
}
