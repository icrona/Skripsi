using UnityEngine;
using System.Collections;

public class TextSize : MonoBehaviour {

    // Use this for initialization
    private float scaleFactor = 0.02f;
    private float minScaleX;
    private float maxScaleX;
    private float minScaleY;
    private float maxScaleY;
    private float minScaleZ;
    private float maxScaleZ;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    public float initialScaleX;
    public float initialScaleY;
    public float initialScaleZ;
    Touch touchZero;
    Touch touchOne;

    Vector2 touchZeroPrevPos;
    Vector2 touchOnePrevPos;

    float prevTouchDeltaMag;
    float touchDeltaMag;

    float deltaMagnitudeDiff;
    void Start () {
        minScaleX = 0.5f;
        minScaleY = 0.5f;
        minScaleZ = 0.5f;
        maxScaleX = 2.0f;
        maxScaleY = 2.0f;
        maxScaleZ = 2.0f;

        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
        initialScaleZ = transform.localScale.z;
        minScaleX *= initialScaleX;
        maxScaleX *= initialScaleX;
        minScaleY *= initialScaleY;
        maxScaleY *= initialScaleY;
        minScaleZ *= initialScaleZ;
        maxScaleZ *= initialScaleZ;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 2 && !transform.parent.parent.parent.parent.parent.GetComponent<CakeZoom>().isActiveAndEnabled)
        {

            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                transform.localScale -= new Vector3(deltaMagnitudeDiff * scaleFactor, deltaMagnitudeDiff * scaleFactor, deltaMagnitudeDiff * scaleFactor);

                scaleX = Mathf.Clamp(transform.localScale.x, minScaleX, maxScaleX);
                scaleY = Mathf.Clamp(transform.localScale.y, minScaleY, maxScaleY);
                scaleZ = Mathf.Clamp(transform.localScale.z, minScaleZ, maxScaleZ);

                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            }
        } 
}
