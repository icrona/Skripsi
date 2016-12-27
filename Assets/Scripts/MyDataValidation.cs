using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class MyDataValidation : MonoBehaviour {

    // Use this for initialization
    public InputField custName;
    public InputField custPhone;
    public InputField custEmail;
    public InputField custAddress;
    private string greenHexColor;
    private string redHexColor;
    private Color green;
    private Color red;
    public Button save;

    private bool isValidName;
    private bool isValidPhone;
    private bool isValidEmail;
    private bool isValidAddress;

    public GameObject panel;

    void Start()
    {
        greenHexColor = "#9AF597FF";
        redHexColor = "#F59494FF";
        ColorUtility.TryParseHtmlString(greenHexColor, out green);
        ColorUtility.TryParseHtmlString(redHexColor, out red);
        isValidPhone = false;
        isValidName = false;
        isValidAddress = false;
        isValidEmail = false;
        if(PlayerPrefs.GetInt("UserData")==1)
        {
            custName.text = PlayerPrefs.GetString("CustomerName");
            custPhone.text = PlayerPrefs.GetString("CustomerPhone");
            custEmail.text = PlayerPrefs.GetString("CustomerEmail");
            custAddress.text = PlayerPrefs.GetString("CustomerAddress");
        }
    }


    void Update()
    {
        if (isValidAddress && isValidEmail && isValidName && isValidPhone)
        {
            save.interactable = true;
        }
        else
        {
            save.interactable = false;
        }
    }
    public void checkName()
    {
        if (custName.text=="")
        {
            custName.image.color = red;
            custName.placeholder.GetComponent<Text>().text = "Name can not be empty";
            isValidName = false;
        }
        else
        {
            custName.image.color = green;
            isValidName = true;
            
        }
    }
    public void checkPhone()
    {
        if (custPhone.text == "")
        {
            custPhone.image.color = red;
            custPhone.placeholder.GetComponent<Text>().text = "Phone can not be empty";
            isValidPhone = false;
        }
        else
        {
            custPhone.image.color = green;
            isValidPhone = true;
            
        }
    }
    public void checkEmail()
    {
        if (custEmail.text == "")
        {
            custEmail.image.color = red;
            custEmail.placeholder.GetComponent<Text>().text = "Email can not be empty";
            isValidEmail = false;
        }

        else if (!Regex.IsMatch(custEmail.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        {
            custEmail.image.color = red;
            isValidEmail = false;
        }
        else
        {
            custEmail.image.color = green;
            isValidEmail = true;
            
        }
    }

    public void checkAddress()
    {
        if (custAddress.text == "")
        {
            custAddress.image.color = red;
            custAddress.placeholder.GetComponent<Text>().text = "Address can not be empty";
            isValidAddress = false;
        }
        else
        {
            custAddress.image.color = green;
            isValidAddress = true;           
        }
    }

    private void assignData(int userData)
    {
        PlayerPrefs.SetString("CustomerName", custName.text);
        PlayerPrefs.SetString("CustomerPhone", custPhone.text);
        PlayerPrefs.SetString("CustomerEmail", custEmail.text);
        PlayerPrefs.SetString("CustomerAddress", custAddress.text);
        PlayerPrefs.SetInt("UserData", userData);
    }

    public void saveData()
    {
        assignData(1);
        panel.transform.GetChild(0).GetComponent<Text>().text = "Your Data Have Been Saved";
        panel.SetActive(true);
    }

    public void resetData()
    {
        custName.text = "";
        custPhone.text = "";
        custEmail.text = "";
        custAddress.text = "";
        assignData(0);
        panel.transform.GetChild(0).GetComponent<Text>().text = "Your Data Have Been Reset";
        panel.SetActive(true);
    }

    public void backStart()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        SceneManager.LoadScene("StartMenu");
    }
    public void ok()
    {
        panel.SetActive(false);
    }
}


