using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddFigure : MonoBehaviour {
    // Use this for initialization
    public GameObject[] figurePrefab;
    private GameObject figureInstantiate;
    public int index;

    public Button[] figure;
    public GameObject figures;
    private int numOffigure;
    void Start()
    {
        numOffigure = figures.transform.childCount;
        figure = new Button[numOffigure];
        figurePrefab = new GameObject[numOffigure];
        figurePrefab = (GameObject[])Resources.LoadAll<GameObject>("Prefab/Figures");
        for (int i = 0; i < numOffigure; i++)
        {
            figure[i] = figures.transform.GetChild(i).GetComponent<Button>();
            Button b = figure[i];
            AddListener(b, i);
        }
    }

    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => addfigure(i));
    }

    public void addfigure(int x)
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        transform.parent.parent.GetComponent<CakeZoom>().resetScale();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
        }
        figureInstantiate = Instantiate(figurePrefab[x], new Vector3(0f, -20f, 180f), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;

        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        figureInstantiate.transform.parent = transform.GetChild(index).GetChild(1);
        releaseLock();
    }
    void releaseLock()
    {
        transform.parent.GetComponent<LockDecorationAndText>().decorationLock(false);
        transform.parent.GetComponent<LockDecorationAndText>().lockDecoration.gameObject.transform.parent.GetComponent<Toggle>().isOn = false;
    }

}
