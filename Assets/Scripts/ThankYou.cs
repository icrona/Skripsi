using UnityEngine;
using System.Collections;

public class ThankYou : MonoBehaviour {

    public GameObject thankYouPanel;
    int show;
    private Vector3 showPos;
    private Vector3 hidePos;
    private float speed = 1500;
    private void Start()
    {
        showPos = new Vector3(0, 320, 0);
    }
    public void showThankYou()
    {
        show = 1;
    }
    private void Update()
    {
        float step = speed * Time.deltaTime;
        if (show == 1)
        {
            thankYouPanel.transform.localPosition = Vector3.MoveTowards(thankYouPanel.transform.localPosition, showPos, step);
        }
    }
}
