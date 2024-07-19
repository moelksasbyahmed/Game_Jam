using UnityEngine;
using UnityEngine.Rendering.Universal;

public class changePlayerColor : MonoBehaviour
{
    ActivePlayer.colors _changeColorTo;
    public enum colors { red, blue, green };
    public colors changeColorTo;

    Color red = new Color32(163, 17, 19, 191);
    Color blue = new Color32(17, 32, 163, 191);
    Color green = new Color32(22, 163, 17, 191);

    // Start is called before the first frame update
    void Start()
    {
        Light2D spriteRenderer = GetComponent<Light2D>();
        switch (changeColorTo)
        {
            case colors.red:
                _changeColorTo = ActivePlayer.colors.red;
                spriteRenderer.color = red;
                break;
            case colors.blue:
                _changeColorTo = ActivePlayer.colors.blue;
                spriteRenderer.color = blue;
                break;
            case colors.green:
                _changeColorTo = ActivePlayer.colors.green;
                spriteRenderer.color = green;
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("core"))
        {
            collision.transform.parent.GetComponent<split>().thisPlayerColor = _changeColorTo;

        }
    }
}
