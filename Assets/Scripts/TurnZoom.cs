using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnZoom : MonoBehaviour
{
    public GameObject cake;


    // Update is called once per frame
    void Update()
    {
        if (!cake.GetComponent<CakeZoom>().isActiveAndEnabled)
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
            cake.GetComponent<CakeZoom>().enabled = true;
        }
    }
}
