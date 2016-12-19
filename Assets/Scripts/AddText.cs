using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddText : MonoBehaviour
{
    // Use this for initialization
    public GameObject textPrefab;
    private GameObject text;
    public GameObject textColor;
    public InputField textText;
    public int index;
    public int tier;
    void Start()
    {
        tier = transform.GetSiblingIndex();
    }
    public void add()
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        textColor.SetActive(true);
        textText.text = "Add Text";
        PlayerPrefs.SetInt("TextTier", tier);
        PlayerPrefs.SetInt("TextIndex", (transform.GetChild(index).GetChild(2).childCount));
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
        }
        text = Instantiate(textPrefab, new Vector3(0f, -18f, 180f), Quaternion.identity) as GameObject;
        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        text.transform.parent = transform.GetChild(index).GetChild(2);
        text.transform.localRotation = Quaternion.Euler(90, 180, 0);
        releaseLock();
    }

    void releaseLock()
    {
        transform.parent.GetComponent<LockDecorationAndText>().textLock(false);
        transform.parent.GetComponent<LockDecorationAndText>().lockText.gameObject.transform.parent.GetComponent<Toggle>().isOn = false;
    }

}