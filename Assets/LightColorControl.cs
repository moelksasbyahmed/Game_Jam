using UnityEngine;
using UnityEngine.Rendering.Universal;
using static split;
public class LightColorControl : MonoBehaviour
{
    public bool beForActive; // true to make it follow the active state not the current state
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activePlayer != null)
        {
            if (beForActive)
            {

                switch (activePlayer.activePlayerColor)
                {
                    case ActivePlayer.colors.red:
                        GetComponent<Light2D>().color = activePlayer.red;
                        break;
                    case ActivePlayer.colors.green:
                        GetComponent<Light2D>().color = activePlayer.green;
                        break;
                    case ActivePlayer.colors.blue:
                        GetComponent<Light2D>().color = activePlayer.blue;
                        break;
                    case ActivePlayer.colors.yellow:
                        GetComponent<Light2D>().color = activePlayer.yellow;
                        break;
                    case ActivePlayer.colors.purble:
                        GetComponent<Light2D>().color = activePlayer.purble;
                        break;
                    case ActivePlayer.colors.cyan:
                        GetComponent<Light2D>().color = activePlayer.cyan;
                        break;



                }

            }

            else
            {

                switch (transform.parent.parent.GetComponent<split>().thisPlayerColor)
                {
                    case ActivePlayer.colors.red:
                        GetComponent<Light2D>().color = activePlayer.red;
                        break;
                    case ActivePlayer.colors.green:
                        GetComponent<Light2D>().color = activePlayer.green;
                        break;
                    case ActivePlayer.colors.blue:
                        GetComponent<Light2D>().color = activePlayer.blue;
                        break;
                    case ActivePlayer.colors.yellow:
                        GetComponent<Light2D>().color = activePlayer.yellow;
                        break;
                    case ActivePlayer.colors.purble:
                        GetComponent<Light2D>().color = activePlayer.purble;
                        break;
                    case ActivePlayer.colors.cyan:
                        GetComponent<Light2D>().color = activePlayer.cyan;
                        break;



                }

            }
        }
    }
}
