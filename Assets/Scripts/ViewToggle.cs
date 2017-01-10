using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ViewToggle : MonoBehaviour {

    public Toggle viewToggle;
    private int inView;
    private float speed =1000;
    private Vector3 offPos;
    private Vector3 onPos;
    public GameObject rotate;
    public void Start()
    {
        onPos = new Vector3(180.5f,50,0);
        offPos = new Vector3(295.5f, 50, 0);
    }
    public void Update()
    {
        float step = speed * Time.deltaTime;
        if (inView==1)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, onPos, step);
        }
        if (inView==-1)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, offPos, step);
        }

    }
    public void toggleView()
    {
        if (viewToggle.isOn)
        {
            inView = 1;
            viewToggle.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else
        {
            inView = -1;
            viewToggle.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
    public void forceOff()
    {
        inView = 0;
        viewToggle.isOn = false;
        transform.localPosition =offPos;
       
    }
   
}
