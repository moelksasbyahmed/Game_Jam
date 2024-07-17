
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    public static string dialougeName;
    GameObject dialougeWindow;

    // Start is called before the first frame update
    void Start()
    {

        dialougeWindow = GameObject.Find("Dialouge Window");
        dialougeWindow.SetActive(false);





        //Dialouge.LoadAllDia();

        //the old is above ^^^^^
        //this is for test, will be changed later change  --------------------------------


    }

    // Update is called once per frame
    void Update()
    {
        //to make the player pause when talking, will break the Writing animation mechanics, and the Size animation
        //if (dialougeWindow.activeSelf)
        //{

        //    Time.timeScale = 0f;

        //}
        //else
        //{
        //    Time.timeScale = 1f;


        //}
        if (Input.GetKeyDown(KeyCode.M)) // changed change this will be deleted in the production and the below too
        {
            Dialouge.LoadAllDia(dialougeName);

        }



        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (dialougeWindow.activeSelf)
            {


                dialougeWindow.SetActive(false);
            }
            else
            {


                dialougeWindow.SetActive(true);
                // dialougeWindow.GetComponent<Dialouge>().LoadNextLine();

            }

        }




    }
}
