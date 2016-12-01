using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SprinklePipingManager : MonoBehaviour {
    private Button[] sprinkleButton;
    private Button[] pipingButton;

	// Use this for initialization
	void Start () {
        sprinkleButton = new Button[2];
        pipingButton = new Button[3];

        for (int i = 0; i < 2; i++)
        {
            sprinkleButton[i] = transform.GetChild(4).GetChild(i).GetComponent<Button>();
            Button bs = sprinkleButton[i];
            AddListenerSprinkle(bs, i);
        }

        for (int i = 0; i < 3; i++)
        {
            pipingButton[i] = transform.GetChild(5).GetChild(i).GetComponent<Button>();
            Button bp = pipingButton[i];
            AddListenerPiping(bp, i);
        }
    }

    void AddListenerSprinkle(Button b,int i)
    {
        b.onClick.AddListener(() => changeSprinkle(b, i));
    }

    void AddListenerPiping(Button b, int i)
    {
        b.onClick.AddListener(() => changePiping(b, i));
    }

    void changeSprinkle(Button b, int x)
    {
        hideSprinkle();
        displayButtonSprinkle();
        b.interactable = false;
        showSprinkle(x);
    }

    void changePiping(Button b, int x)
    {
        hidePiping();
        displayButtonPiping();
        b.interactable = false;
        showPiping(x);
    }

    void hideSprinkle()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }
    void showSprinkle(int x)
    {
        transform.GetChild(0).GetChild(x).gameObject.SetActive(true);
    }
    void displayButtonSprinkle()
    {
        for (int i = 0; i < 2; i++)
        {
            sprinkleButton[i].interactable = true;
        }
    }

    void hidePiping()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
    }
    void showPiping(int x)
    {
        transform.GetChild(1).GetChild(x).gameObject.SetActive(true);
    }
    void displayButtonPiping()
    {
        for (int i = 0; i < 3; i++)
        {
            pipingButton[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
