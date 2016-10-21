using UnityEngine;
using System.Collections;

public class CakeZoom : MonoBehaviour
{
    private float scaleFactor = 0.02f; 

    private float [] minScaleX;
    private float [] maxScaleX;
    private float [] minScaleY;
    private float [] maxScaleY;
    private float [] minScaleZ;
    private float [] maxScaleZ;
    
    private float[] scaleX;
    private float[] scaleY;
    private float[] scaleZ;

    public float[] initialScaleX;
    public float[] initialScaleY;
    public float[] initialScaleZ;

    public GameObject[] cakeZoom;

    Touch touchZero;
    Touch touchOne;

    Vector2 touchZeroPrevPos;
    Vector2 touchOnePrevPos;

    float prevTouchDeltaMag;
    float touchDeltaMag ;

    float deltaMagnitudeDiff;

    private void Start()
    {
        cakeZoom = new GameObject[transform.childCount];
        initialScaleX = new float[transform.childCount];
        initialScaleY = new float[transform.childCount];
        initialScaleZ = new float[transform.childCount];

        minScaleX = new float[transform.childCount];
        minScaleY = new float[transform.childCount];
        minScaleZ = new float[transform.childCount];

        maxScaleX = new float[transform.childCount];
        maxScaleY = new float[transform.childCount];
        maxScaleZ = new float[transform.childCount];

        scaleX = new float[transform.childCount];
        scaleY = new float[transform.childCount];
        scaleZ = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            cakeZoom[i] = transform.GetChild(i).gameObject;
            minScaleX[i] = 0.7f;
            minScaleY[i] = 0.7f;
            minScaleZ[i] = 0.7f;
            maxScaleX[i] = 2.0f;
            maxScaleY[i] = 2.0f;
            maxScaleZ[i] = 2.0f;
            initialScaleX[i] = cakeZoom[i].transform.localScale.x;
            initialScaleY[i] = cakeZoom[i].transform.localScale.y;
            initialScaleZ[i] = cakeZoom[i].transform.localScale.z;
            minScaleX[i] *= initialScaleX[i];
            maxScaleX[i] *= initialScaleX[i];
            minScaleY[i] *= initialScaleY[i];
            maxScaleY[i] *= initialScaleY[i];
            minScaleZ[i] *= initialScaleZ[i];
            maxScaleZ[i] *= initialScaleZ[i];
        }    
    }

    public void resetScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cakeZoom[i].transform.localScale = new Vector3(initialScaleX[i], initialScaleY[i], initialScaleZ[i]);
        }
    }
    void Update()
    {
        if (Input.touchCount == 2)
        {
            
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            for (int i = 0; i < transform.childCount; i++)
            {
                cakeZoom[i].transform.localScale -= new Vector3(deltaMagnitudeDiff * scaleFactor, deltaMagnitudeDiff * scaleFactor, deltaMagnitudeDiff * scaleFactor);

                scaleX[i] = Mathf.Clamp(cakeZoom[i].transform.localScale.x, minScaleX[i], maxScaleX[i]);
                scaleY[i] = Mathf.Clamp(cakeZoom[i].transform.localScale.y, minScaleY[i], maxScaleY[i]);
                scaleZ[i] = Mathf.Clamp(cakeZoom[i].transform.localScale.z, minScaleZ[i], maxScaleZ[i]);
                
                cakeZoom[i].transform.localScale = new Vector3(scaleX[i], scaleY[i], scaleZ[i]);
            }
            
        }
    }
}