using System.Collections;
using UnityEngine;

public class split : MonoBehaviour
{
    public int numberOfSplitestsLeft = 3;
    public static GameObject activePlayer;
    public GameObject slime;

    static Transform playersParent;
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
        if (Input.GetMouseButtonDown(0) && numberOfSplitestsLeft > 0 && activePlayer == gameObject)
        {
            StopAllCoroutines();
            StartCoroutine(sdsd());
        }
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

        }

    }


    IEnumerator sdsd()
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
        newSplitted2.transform.position = transform.GetChild(transform.childCount - 1).position ;

        newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = velocity * 1.5f;
        newSplitted2.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity = velocity;

        //Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>().velocity.y);

        Debug.Log(newSplitted.transform.GetChild(transform.childCount - 1).position);


        newSplitted.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;
        newSplitted2.GetComponent<split>().numberOfSplitestsLeft = numberOfSplitestsLeft;


        newSplitted.transform.localScale = activePlayer.transform.localScale / 2;
        newSplitted2.transform.localScale = activePlayer.transform.localScale / 2;

        activePlayer = newSplitted2;

        Destroy(gameObject);



    }
}
