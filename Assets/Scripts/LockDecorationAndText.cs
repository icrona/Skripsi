using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LockDecorationAndText : MonoBehaviour {

    public Sprite locked;
    public Sprite unlocked;
    public GameObject lockDecoration;
    public GameObject lockText;
	// Use this for initialization

    public void decorationLock(bool isLocked)
    {
        if (isLocked) {
            lockDecoration.GetComponent<Image>().sprite = locked;
            colliderToggle(true,1);
        }
        else
        {
            lockDecoration.GetComponent<Image>().sprite = unlocked;
            colliderToggle(false,1);
        }
    }

    public void textLock(bool isLocked)
    {
        if (isLocked)
        {
            lockText.GetComponent<Image>().sprite = locked;
            colliderToggle(true, 2);
        }
        else
        {
            lockText.GetComponent<Image>().sprite = unlocked;
            colliderToggle(false, 2);
        }
    }

    void colliderToggle(bool disable,int decorationOrText)
    {
        for(int i = 0; i < PlayerPrefs.GetInt("NumberOfTiers");i++){
            for (int j = 0; j < 3; j++)
            {
                if (transform.GetChild(i).GetChild(j).gameObject.activeSelf)
                {
                    for(int k=0;k< transform.GetChild(i).GetChild(j).GetChild(decorationOrText).childCount; k++)
                    {
                        if (disable)
                        {
                            transform.GetChild(i).GetChild(j).GetChild(decorationOrText).GetChild(k).GetComponent<BoxCollider>().enabled = false;
                        }
                        else
                        {
                            transform.GetChild(i).GetChild(j).GetChild(decorationOrText).GetChild(k).GetComponent<BoxCollider>().enabled = true;
                        }                        
                    }
                }               
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
