using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IntensityColor : MonoBehaviour, IPointerUpHandler {
    public int value;
    public GameObject cake;
    public void OnPointerUp(PointerEventData eventData)
    {
        if (transform.GetComponent<Slider>().value<0.35)
        {
            value = 3;
        }
        else if (transform.GetComponent<Slider>().value < 0.5)
        {
            value = 2;
        }
        else if (transform.GetComponent<Slider>().value < 0.65)
        {
            value = 1;
        }
        else
        {
            value = 0;
        }
        PlayerPrefs.SetInt("FrostingTier" + (transform.parent.parent.parent.GetSiblingIndex()+1), value);
        cake.GetComponent<CakePrice>().calculateCakePrice();
    }
}
