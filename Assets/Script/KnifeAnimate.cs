using UnityEngine;
using System.Collections;

public class KnifeAnimate : MonoBehaviour {
    Vector3 posA, posB;
    private float speed = 250;
    public int animation;
    int hide;
    // Use this for initialization
    void Start () {
        transform.GetChild(3).gameObject.SetActive(false);
        posA = new Vector3(0, 100f, -320f);
        posB = new Vector3(90, 100f, -320f);
        animation = 0;
    }
    
    public void animate(bool on)
    {
        if (on)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(3).localPosition = posA;
            animation = 1;
            hide = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        if (animation == 1)
        {
            transform.GetChild(3).localPosition = Vector3.MoveTowards(transform.GetChild(3).localPosition, posB, step);
            hide++;
        }
        if (hide > 50)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
        
    }
}
