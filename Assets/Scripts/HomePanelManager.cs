using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomePanelManager : MonoBehaviour
{
    // Use this for initialization
    public GameObject homePanel;
    int show;
    private Vector3 showPos;
    private Vector3 hidePos;
    private float speed = 1000;
    private void Start()
    {
        hidePos = new Vector3(-562.5f, 50, 0);
        showPos = new Vector3(-187.5f, 50, 0);
    }
    public void showHome()
    {
        if (transform.GetComponent<CakeData>().saved)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            show = 1;
        }
        
    }
    public void hideHome()
    {
        show = -1;
    }
    private void Update()
    {
        float step = speed * Time.deltaTime;
        if (show == 1)
        {
            homePanel.transform.localPosition = Vector3.MoveTowards(homePanel.transform.localPosition, showPos, step);
        }
        if (show == -1)
        {
            homePanel.transform.localPosition = Vector3.MoveTowards(homePanel.transform.localPosition, hidePos, step);
        }
    }
}

