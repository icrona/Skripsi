using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CakeRotate : MonoBehaviour
{
    private float rotationRateX;
    private float rotationRateY;
    private float rotateX;
    private float rotateY;
    private float eulerAngleX;
    private float eulerAngleY;
    public GameObject[] theCake;
    public Toggle faceUp;
    public Toggle faceYou;
    private bool rotateOnlyOnZ;
    private bool rotateOnlyOnY;
    void Awake()
    {
        theCake = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i] = transform.GetChild(i).gameObject;
        }

        rotateOnlyOnZ = false;
        rotateOnlyOnY = false;
        rotationRateX = 0.2f;
        rotationRateY = 0.1f;

        theCake = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i] = transform.GetChild(i).gameObject;
        }
    }
    void Update()
    {
        if (Input.touchCount == 1)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x > Screen.width/7 && touch.position.x < 6* Screen.width / 7 && touch.position.y > Screen.height/3 && touch.position.y <2* Screen.height/3) {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        rotateX = touch.deltaPosition.y * rotationRateX;
                        rotateY = -touch.deltaPosition.x * rotationRateY;
                        for (int i=0;i< transform.childCount;i++) { 
                            if (!rotateOnlyOnY && !rotateOnlyOnZ)
                            {
                                theCake[i].transform.Rotate(rotateX, rotateY, 0, Space.World);

                            }
                            if (rotateOnlyOnZ && !rotateOnlyOnY)
                            {
                                theCake[i].transform.Rotate(rotateX, 0, 0, Space.World);
                            }

                            if (rotateOnlyOnY && !rotateOnlyOnZ)
                            {
                                theCake[i].transform.Rotate(0, rotateY, 0, Space.World);
                            }
                        }
                    }
                }
            }
        } 
    }

    public void facingUp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(0, 180, 0);
            rotateOnlyOnY = true;
            rotateOnlyOnZ = false;
        }
        
    }
    public void facingYou()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(90, 180, 0);
            rotateOnlyOnZ = true;
            rotateOnlyOnZ = false;
        }
    }
    public void rotateOnAgain()
    {
        rotateOnlyOnY = false;
        rotateOnlyOnZ = false;
    }
}
