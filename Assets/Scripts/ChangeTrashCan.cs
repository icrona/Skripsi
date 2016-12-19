using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTrashCan : MonoBehaviour {
    public Sprite open;
    public Sprite close;

	public void openTrash(bool on)
    {
        if (on)
        {
            transform.GetComponent<Image>().sprite = open;
        }
        else
        {
            transform.GetComponent<Image>().sprite = close;
        }
    }
}
