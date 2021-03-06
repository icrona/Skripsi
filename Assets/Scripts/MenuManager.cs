﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {
    private GameObject[] menuList;
    public GameObject cakeList;
    public GameObject sidePanel;
    public GameObject buttonNext;
    public GameObject buttonBack;
    public int index;
    public Text menuLabel;
    public string[] menuName;

    public Text completionText;
    private float progressPercent=0f;
    public float percent;
    public Image progressBar;

    public InputField cakeName;

    public GameObject info;
    private GameObject[] infoList;


    private void Start () {
        menuList = new GameObject[transform.childCount];
        infoList = new GameObject[transform.childCount];
        for (int i= 0;i< transform.childCount; i++)
        {
            menuList[i] = transform.GetChild(i).gameObject;
            infoList[i] = info.transform.GetChild(i).gameObject;
        }
        index = 0;
        activeCurrMenu();
        menuName = new string[transform.childCount];
        menuName[0] = "Cake Basic";
        menuName[1] = "Frosting & Color";
        menuName[2] = "Sprinkle & Piping";
        menuName[3] = "Decoration";
        menuName[4] = "Text";
        menuName[5] = "Preview";
        buttonBack.SetActive(false);
    }
    public void backBtn()
    {
        if (index == 5)
        {
            transform.GetComponent<CakeData>().resetPreviewData();
        }
        index--;
        transform.GetComponent<ButtonSound>().playSound();
        if (index == 0)
        {
            buttonBack.SetActive(false);
        }
        if (index<0)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            menuListLabel();
            updateCompletionText(index);
        }
        activeCurrMenu();
        if ((index == 1 && PlayerPrefs.GetInt("Frosting") == -1) || index == 5)
        {
            buttonNext.SetActive(false);
        }
        else
        {
            buttonNext.SetActive(true);
        }
    }
    public void nextBtn()
    { 
        index++;
        if (index == 1)
        {
            buttonBack.SetActive(true);
        }
        if(index == 5)
        {
            transform.GetComponent<CakeData>().showPreviewData();
        }
        menuListLabel();
        updateCompletionText(index);
        activeCurrMenu();
        transform.GetComponent<ButtonSound>().playSound();
        if((index==1 && PlayerPrefs.GetInt("Frosting") == -1)||index==5)
        {
            buttonNext.SetActive(false);
        }
        else
        {
            buttonNext.SetActive(true);
        }
        
    }
    public void menuListLabel()
    {
        menuLabel.text = menuName[index];
    }
    private void Update()
    {
        if (progressPercent<percent)
        {
            progressPercent += 60 * Time.deltaTime;
            progressBar.fillAmount = progressPercent / 100f;
        }
        if (progressPercent>percent)
        {
            progressPercent -= 60 * Time.deltaTime;
            progressBar.fillAmount = progressPercent / 100f;
        }
    }
    public void updateCompletionText(int index)
    {
        percent = Mathf.Round((float)index / transform.childCount * 100 * 100f)/100f;
        completionText.text = "Completion : " +percent + "%";
    }
    public void activeCurrMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            info.transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(index).gameObject.SetActive(true);
        info.transform.GetChild(index).gameObject.SetActive(true);
    }
    public void lastMenu()
    {
        if (cakeName.text!="")
        {
            updateCompletionText(index + 1);
        } 
    }
    public void goToHome()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        SceneManager.LoadScene("StartMenu");
    }

    public void goToOrder()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        SceneManager.LoadScene("Order");
    }

    public void openInfo()
    {
        info.SetActive(true);
    }
    public void closeInfo()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        info.SetActive(false);
    }
}
