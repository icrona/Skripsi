using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnZoom : MonoBehaviour
{
    public GameObject rotate;


    // Update is called once per frame
    void Update()
    {
        if (!rotate.GetComponent<CakeZoom>().isActiveAndEnabled)
        {
            transform.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            turnZoom(true);
        }

    }
    public void turnZoom(bool on)
    {
        if (on)
        {
            rotate.GetComponent<CakeZoom>().enabled = true;
        }
    }
}
