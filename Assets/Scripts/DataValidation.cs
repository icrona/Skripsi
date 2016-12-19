using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SelectedDate
{
    public static System.DateTime date = System.DateTime.Now;
    public static System.DateTime now = System.DateTime.Now;
}
public class DateCallback : AndroidJavaProxy
{
    public DateCallback() : base("android.app.DatePickerDialog$OnDateSetListener") { }

    public void onDateSet(AndroidJavaObject view, int year, int monthOfYear, int dayOfMonth)
    {
        SelectedDate.date = new System.DateTime(year, monthOfYear + 1, dayOfMonth);
    }
}
public class DataValidation : MonoBehaviour {

    // Use this for initialization
    public InputField custName;
    public InputField custPhone;
    public InputField custEmail;
    public InputField custDelivDate;
    public InputField custAddress;
    public InputField custNotes;
    private string greenHexColor;
    private string redHexColor;
    private Color green;
    private Color red;
    public Button order;
    private AndroidJavaObject activity;
    private bool isTriggered;

    private bool isValidName;
    private bool isValidPhone;
    private bool isValidEmail;
    private bool isValidDate;
    private bool isValidAddress;

    void Start()
    {
        greenHexColor = "#9AF597FF";
        redHexColor = "#F59494FF";
        ColorUtility.TryParseHtmlString(greenHexColor, out green);
        ColorUtility.TryParseHtmlString(redHexColor, out red);
        isTriggered = false;
        SelectedDate.now = SelectedDate.now.AddDays(PlayerPrefs.GetInt("MinDays")-1);
        
        isValidDate = false;
        isValidPhone = false;
        isValidName = false;
        isValidAddress = false;
        isValidEmail = false;
        custDelivDate.placeholder.GetComponent<Text>().text = "Minimum " + PlayerPrefs.GetInt("MinDays") + " day(s) from today";

    }
    void PickDate()
    {
        new AndroidJavaObject("android.app.DatePickerDialog", activity, new DateCallback(), SelectedDate.date.Year, SelectedDate.date.Month - 1, SelectedDate.date.Day).Call("show");
    }
    public void showdatePicker()
    {
        activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(PickDate));
        isTriggered = true;
    }

    void Update()
    {
        if (isTriggered == true)
        {
            custDelivDate.text = System.String.Format("{0:yyyy-MM-dd}", SelectedDate.date);
        }
        
        if (isValidAddress && isValidDate && isValidEmail && isValidName && isValidPhone)
        {
            order.interactable = true;
        }
        else
        {
            order.interactable = false;
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
    public void checkDelivDate()
    {
        if (SelectedDate.now.CompareTo(SelectedDate.date)>0)
        {
            custDelivDate.image.color = red;
            isValidDate = false;
        }
        else
        {
            custDelivDate.image.color = green;
            isValidDate = true;
            
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
    public void checkNotes()
    {
        custNotes.image.color = green;
        
    }
}


