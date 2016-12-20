using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public GameObject loading;
    public GameObject exitConfirm;
    public GameObject panelConnection;

    public void toMain()
    {
        loading.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void toGallery()
    {
        SceneManager.LoadScene("Gallery");
    }
    public void toMyData()
    {
        SceneManager.LoadScene("MyData");
    }
    public void toSignature()
    {
        SceneManager.LoadScene("Signature");
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
    public void okay()
    {
        panelConnection.transform.GetChild(0).GetComponent<Text>().text = "No Internet Connection";
        panelConnection.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void tutorial()
    {
        Handheld.PlayFullScreenMovie("test.mp4", Color.black, FullScreenMovieControlMode.Hidden);
    }

}
