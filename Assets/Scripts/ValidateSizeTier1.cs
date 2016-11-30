using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ValidateSizeTier1 : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        int upperSize = PlayerPrefs.GetInt("SizeTier2");
        if (PlayerPrefs.GetInt("NumberOfTiers") == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Toggle>().interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < upperSize; i++)
            {
                transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Toggle>().interactable = false;
            }
        }        
    }
}

