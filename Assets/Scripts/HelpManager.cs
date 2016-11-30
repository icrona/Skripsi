using UnityEngine;
using System.Collections;

public class HelpManager : MonoBehaviour {

    // Use this for initialization
    public GameObject helpPanel;
    int show;
    private Vector3 showPos;
    private Vector3 hidePos;
    private float speed = 1500;
    private void Start()
    {
        hidePos = new Vector3(0, 960, 0);
        showPos = new Vector3(0, 320, 0);
    }
	public void showHelp()
    {
        show = 1;
    }
    public void hideHelp()
    {
        show = -1;
    }
    private void Update()
    {
        float step = speed * Time.deltaTime;
        if (show==1)
        {
            helpPanel.transform.localPosition = Vector3.MoveTowards(helpPanel.transform.localPosition, showPos, step);
        }
        if (show==-1)
        {
            helpPanel.transform.localPosition = Vector3.MoveTowards(helpPanel.transform.localPosition, hidePos, step);
        }
    }
}
