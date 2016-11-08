using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnRotate : MonoBehaviour {
    public GameObject rotate; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!rotate.GetComponent<CakeRotate>().isActiveAndEnabled)
        {
            transform.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            turnRotate(true);
        }

    }
    public void turnRotate(bool on)
    {
        if (on)
        {
            rotate.GetComponent<CakeRotate>().enabled = true;
        }
    }
}
