using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour {

    public void toMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void toGallery()
    {
        SceneManager.LoadScene("Gallery");
    }
}
