using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour {
    public GameObject loading;
    public GameObject exitConfirm;
    public void toMain()
    {
        loading.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void toGallery()
    {
        SceneManager.LoadScene("Gallery");
    }
    public void exitApps()
    {
        exitConfirm.SetActive(true);
    }
    public void exitYes()
    {
        Application.Quit();
    }
    public void exitNo()
    {
        exitConfirm.SetActive(false);
    }
}
