using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {
    private GameObject[] menuList;
    public GameObject cakeList;
    public GameObject sidePanel;
    public GameObject buttonNext;
    public int index;
    public Text menuLabel;
    public string[] menuName;

    public Text completionText;
    private float progressPercent=0f;
    public float percent;
    public Image progressBar;

    public InputField cakeName;


    private void Start () {
        menuList = new GameObject[transform.childCount];
        for (int i= 0;i< transform.childCount; i++)
        {
            menuList[i] = transform.GetChild(i).gameObject;
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
        
    }
    public void backBtn()
    {
        index--;
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
    }
    public void nextBtn()
    {
        index++;
        if (index == menuList.Length)
        {
            goToHome();
        }
        else {
            menuListLabel();
            updateCompletionText(index);
        }
        activeCurrMenu();
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
        
        if(index==1 && PlayerPrefs.GetInt("Frosting") == -1)
        {
            buttonNext.SetActive(false);
        }
        else
        {
            buttonNext.SetActive(true);
        }
        if (index == 5)
        {
            buttonNext.SetActive(false);
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
        }
        transform.GetChild(index).gameObject.SetActive(true);
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
        SceneManager.LoadScene("StartMenu");
    }

    public void goToOrder()
    {
        SceneManager.LoadScene("Order");
    }
}
