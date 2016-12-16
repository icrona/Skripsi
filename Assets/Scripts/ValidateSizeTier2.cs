using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ValidateSizeTier2 : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        int belowSize = PlayerPrefs.GetInt("SizeTier1");
        Debug.Log(belowSize);
        for (int i = belowSize + 1; i < 3; i++)
        {
            transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Toggle>().interactable = false;
        }
        
        int upperSize = PlayerPrefs.GetInt("SizeTier3");
        Debug.Log(upperSize);
        for (int i = 0; i < upperSize; i++)
        {
            transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Toggle>().interactable = false;
        }
    }
}
