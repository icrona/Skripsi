using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ValidateSizeTier3 : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        int belowSize = PlayerPrefs.GetInt("SizeTier2");
        for (int i = belowSize + 1; i < 3; i++)
        {
            transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<Toggle>().interactable = false;
        }
    }
}
