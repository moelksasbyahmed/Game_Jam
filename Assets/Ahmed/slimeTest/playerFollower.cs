using UnityEngine;

public class playerFollower : MonoBehaviour
{
    Transform player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (split.activePlayer.activePlayerObject != null)
        {

            player = split.activePlayer.activePlayerObject.transform;

            transform.Translate(player.transform.GetChild(player.transform.childCount - 1).position + offset - transform.position);
                //= player.transform.GetChild(player.transform.childCount - 1).localPosition +offset;

        }

    }
}
