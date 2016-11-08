using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextEdit : MonoBehaviour {

    public GameObject TextPanel;
    private int textIndex;
    private int tier;
    private GameObject textField;
    private GameObject editToggle;
    private bool touch;
    // Use this for initialization
    void Start () {
        TextPanel = transform.parent.parent.parent.parent.parent.parent.GetChild(0).GetChild(3).gameObject;
        tier = transform.parent.parent.parent.GetSiblingIndex();
        textField = TextPanel.transform.GetChild(tier).GetChild(0).GetChild(2).gameObject;
        editToggle= TextPanel.transform.GetChild(tier).GetChild(0).GetChild(3).gameObject;
    }
	
    void OnMouseDown()
    {
        textIndex = transform.GetSiblingIndex();
        touch = true;
    }
    void OnMouseUp()
    {
        touch = false;
    }
	// Update is called once per frame
	void Update (){
        if(touch && editToggle.GetComponent<Toggle>().isOn)
        {
            transform.parent.GetChild(textIndex).GetComponent<TextMesh>().text = textField.GetComponent<InputField>().text;
        }       
    }
}
