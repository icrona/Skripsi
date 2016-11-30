using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryConfirmation : MonoBehaviour {

    public GameObject confirmation;
    int show;
    private Vector3 showPos;
    private Vector3 hidePos;
    private float speed = 1000;
    public Text confirmText;
    public Button yesDelete;
    public Button yesOrder;

    private void Start()
    {
        hidePos = new Vector3(750f, 0, 0);
        showPos = new Vector3(0f, 0, 0);
    }
    public void showConfirmOrder()
    {
        confirmation.SetActive(true);
        yesDelete.gameObject.SetActive(false);
        yesOrder.gameObject.SetActive(true);
        confirmText.text = "Proceed to Order ?";
        show = 1;
    }
    public void hideConfirm()
    {
        show = -1;
    }
    public void showConfirmDelete()
    {
        confirmation.SetActive(true);
        yesOrder.gameObject.SetActive(false);
        yesDelete.gameObject.SetActive(true);
        confirmText.text = "Delete This Cake ?";
        show = 1;
    }

    public void confirmed()
    {
        confirmation.SetActive(false);
        show = - 1;
    }
    public void order()
    {
        SceneManager.LoadScene("Order");
    }
    private void Update()
    {
        float step = speed * Time.deltaTime;
        if (show == 1)
        {
            confirmation.transform.localPosition = Vector3.MoveTowards(confirmation.transform.localPosition, showPos, step);
        }
        if (show == -1)
        {
            confirmation.transform.localPosition = Vector3.MoveTowards(confirmation.transform.localPosition, hidePos, step);
        }
    }
}
