using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DecorationManager : MonoBehaviour {

    private Button[] decorationButton;

	// Use this for initialization
	void Start () {
        decorationButton = new Button[2];
	    for(int i = 0; i < 2; i++)
        {
            decorationButton[i] = transform.GetChild(0).GetChild(i).GetComponent<Button>();
            Button b = decorationButton[i];
            AddListener(b, i);
        }
	}

    void AddListener(Button b,int i)
    {
        b.onClick.AddListener(() => changeDecoration(b,i));
    }

    void changeDecoration(Button b,int x)
    {
        hide();
        displayButton();
        b.interactable = false;
        show(x);
    }

    void displayButton()
    {
        for(int i = 0; i < 2; i++)
        {
            decorationButton[i].interactable = true;
        }
    }
    void hide()
    {
        for(int i = 0; i < 2; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
    }

    void show(int x)
    {
        transform.GetChild(1).GetChild(x).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
