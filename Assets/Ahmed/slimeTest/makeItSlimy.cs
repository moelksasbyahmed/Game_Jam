using System.Collections;
using UnityEngine;

public class makeItSlimy : MonoBehaviour
{
    SpringJoint2D[] springs;
    public float dumpG = 0.8f;
    public float dump = 0.1f;
    public float freqG = 1f;
    public float freq = 30;

    // Start is called before the first frame update
    void Start()
    {
        springs = GetComponentsInChildren<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Groundd"))
        {
            springs[0].frequency = freqG;
            springs[0].dampingRatio = dumpG;
            springs[2].frequency = freqG;
            springs[2].dampingRatio = dumpG;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Groundd"))
        {
            startTime = Time.time;
            StopAllCoroutines();
            StartCoroutine(interpolate());
        }
    }
    float startTime;
    public float changeTime = 1;
    IEnumerator interpolate()
    {
        while(Time.time - startTime < changeTime)
        {

            springs[0].frequency = Mathf.Lerp(freqG,freq,(Time.time -startTime)/changeTime) ;
            springs[0].dampingRatio = Mathf.Lerp(dumpG, dump, (Time.time - startTime) / changeTime);
            springs[2].frequency = Mathf.Lerp(freqG, freq, (Time.time - startTime) / changeTime);
            springs[2].dampingRatio = Mathf.Lerp(dumpG, dump, (Time.time - startTime) / changeTime);
            yield return null;
        }
    }
}
