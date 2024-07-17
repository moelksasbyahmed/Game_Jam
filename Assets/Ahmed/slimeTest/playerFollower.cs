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
        if (split.activePlayer != null)
        {

            player = split.activePlayer.transform;

            transform.position = player.transform.GetChild(transform.childCount - 1).position +offset;

        }

    }
}
