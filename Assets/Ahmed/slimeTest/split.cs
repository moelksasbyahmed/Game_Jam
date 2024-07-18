using System.Collections;
using UnityEngine;

public class split : MonoBehaviour
{
    public int numberOfSplitestsLeft = 3;
    public static GameObject activePlayer;
    public GameObject slime;

    static Transform playersParent;
    Collider2D c2D;
    // Start is called before the first frame update
    void Start()
    {
        if (activePlayer == null)
        {

            activePlayer = gameObject;
        }
        if (playersParent == null)
        {
            playersParent = transform.parent;
        }
        c2D = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activePlayer != gameObject)
        {
            try
            {

                transform.GetChild(transform.childCount - 1).GetComponent<movement>().enabled = false;
                transform.GetChild(transform.childCount - 1).GetComponent<collision>().enabled = false;
            }
            catch { }

            this.enabled = false;
            return;
        }
        c2D.offset = transform.GetChild(transform.childCount - 1).localPosition;
        if (Input.GetMouseButtonDown(0) && numberOfSplitestsLeft > 0 && activePlayer == gameObject)
        {
            StopAllCoroutines();
            StartCoroutine(splitt());
        }//for splitting
        if (Input.GetKeyDown(KeyCode.Tab) && activePlayer == gameObject)
        {
            int newIndex = activePlayer.transform.GetSiblingIndex() + 1;
            if (newIndex >= playersParent.childCount)
            {
                newIndex = 0;
            }
            activePlayer = playersParent.GetChild(newIndex).gameObject;
            Debug.Log(activePlayer.name);
            try
            {

                activePlayer.transform.GetChild(transform.childCount - 1).GetComponent<movement>().enabled = true;
                activePlayer.transform.GetChild(transform.childCount - 1).GetComponent<collision>().enabled = true;
            }
            catch { }
            activePlayer.GetComponent<split>().enabled = true;

        } //for switching between players


        if (Input.GetMouseButtonDown(1) && activePlayer == gameObject && possibleToJoin != null)
        {
            StopAllCoroutines();

            StartCoroutine(join(possibleToJoin));
        }//for rejoining

    }
    Transform possibleToJoin;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {

            if (collider.transform.parent != transform)
            {
                possibleToJoin = collider.transform.parent.GetChild(transform.childCount - 1);

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.parent != transform)
        {
            if (possibleToJoin != null)
            {
                if (collider.transform == possibleToJoin)
                {
                    possibleToJoin = null;

                }
            }

        }

    }
    public float dashspeedAfterSplit;
    IEnumerator splitt()
    {
        numberOfSplitestsLeft--;

        //transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return 0;
        Vector2 velocity = transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity;
        GameObject newSplitted = Instantiate(slime, playersParent);
        GameObject newSplitted2 = Instantiate(slime, playersParent);
        Debug.Log(transform.GetChild(transform.childCount - 1).position);

        // newSplitted.transform.localPosition = Vector3.zero;
        //newSplitted2.transform.localPosition = Vector3.zero;


        newSplitted.transform.position = transform.GetChild(transform.childCount - 1).position;
        newSplitted2.transform.position = transform.GetChild(transform.childCount - 1).position;
        startTime = Time.time;
        newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = velocity * 1.5f;
        newSplitted2.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = velocity;
        newSplitted2.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().AddForce(dashspeedAfterSplit * (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - newSplitted2.transform.GetChild(transform.childCount - 1).position).normalized, ForceMode2D.Force);



        // StartCoroutine(interpolate(velocity, velocity + dashspeedAfterSplit * (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - newSplitted2.transform.GetChild(transform.childCount - 1).position).normalized, newSplitted2.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>()));
        //Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity.y);

        Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).position);


        newSplitted.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;
        newSplitted2.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;


        newSplitted.transform.localScale = activePlayer.transform.localScale / 2;
        newSplitted2.transform.localScale = activePlayer.transform.localScale / 2;

        activePlayer = newSplitted2;

        Destroy(gameObject);



    }


    IEnumerator join(Transform toJoinWith)
    {
        numberOfSplitestsLeft++;

        //transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return 0;
        Vector2 velocity = transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity;
        GameObject newSplitted = Instantiate(slime, playersParent);

        Debug.Log(transform.GetChild(transform.childCount - 1).position);

        // newSplitted.transform.localPosition = Vector3.zero;
        //newSplitted2.transform.localPosition = Vector3.zero;

        Vector3 newPos = (transform.GetChild(transform.childCount - 1).position + toJoinWith.position) / 2;
        newSplitted.transform.position = new Vector3 { x = newPos.x, y = newPos.y + transform.localScale.x - 1f };

        startTime = Time.time;
        newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = velocity * 0.5f;



        // StartCoroutine(interpolate(velocity, velocity + dashspeedAfterSplit * (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - newSplitted2.transform.GetChild(transform.childCount - 1).position).normalized, newSplitted2.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>()));
        //Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity.y);

        Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).position);


        newSplitted.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;


        newSplitted.transform.localScale = activePlayer.transform.localScale * 2;

        activePlayer = newSplitted;

         Destroy(toJoinWith.parent.gameObject);
        Destroy(gameObject);
        yield break;
    }

    float startTime;
    public float changeTime = 1;
    IEnumerator interpolate(Vector3 startVelocity, Vector3 endVelocity, Rigidbody2D rb)
    {
        while (Time.time - startTime < changeTime)
        {

            rb.velocity = Vector3.Lerp(startVelocity, endVelocity, (Time.time - startTime) / changeTime);
            yield return null;
        }
    }
}
