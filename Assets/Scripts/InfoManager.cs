using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InfoManager : MonoBehaviour
{

    void Start()
    {
        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => closeInfo());
    }
    public void openInfo()
    {
        gameObject.SetActive(true);
    }
    void closeInfo()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        gameObject.SetActive(false);
    }
}

