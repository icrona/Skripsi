using UnityEngine;
using System.Collections;

public class PipingPosition : MonoBehaviour {

    // Use this for initialization
    public Vector3[,] position;
    public Quaternion[,] rotation;
    public GameObject[,] pipes;
    public int []numOfPipe;
    public int []kindOfPipe;
    public Material [] material;
    public int tier;
    int y;

    void Start () {
        material = new Material[3];
        pipes = new GameObject[3, 13];
        position = new Vector3[3, 36];
        rotation = new Quaternion[3, 36];
        numOfPipe = new int[3];
        kindOfPipe = new int[3];
        tier = transform.parent.parent.parent.GetSiblingIndex() + 1;

        switch (tier)
        {
            case 1:
                material = (Material[])Resources.LoadAll<Material>("Pipe/Materials/Tier1");
                break;
            case 2:
                material = (Material[])Resources.LoadAll<Material>("Pipe/Materials/Tier2");
                break;
            case 3:
                material = (Material[])Resources.LoadAll<Material>("Pipe/Materials/Tier3");
                break;
        }

        for (int x = 0; x < transform.childCount; x++)
        {
            Debug.Log("x1 : " + x);
            numOfPipe[x] = transform.GetChild(x).GetChild(0).childCount;
            kindOfPipe[x] = transform.GetChild(x).childCount - 1;

            for (int i = 0; i < numOfPipe[x]; i++)
            {
                position[x,i] = transform.GetChild(x).GetChild(0).GetChild(i).localPosition;
                rotation[x, i] = transform.GetChild(x).GetChild(0).GetChild(i).localRotation;
            }
            if (x == 0)
            {
                y = 1;
            }
            else
            {
                y = 0;
            }
            for (int i = y; i < kindOfPipe[x]; i++)
            {
                Debug.Log("x : " + x);
                Debug.Log("i : " + i);
                pipes[x, i] = transform.GetChild(x).GetChild(i + 1).gameObject;

                for (int j = 0; j < numOfPipe[x]; j++)
                {
                    pipes[x, i].transform.GetChild(j).localPosition = position[x, j];
                    pipes[x, i].transform.GetChild(j).localRotation = rotation[x, j];
                    for (int k = 0; k < pipes[x, i].transform.GetChild(j).childCount; k++)
                    {
                        pipes[x, i].transform.GetChild(j).GetChild(k).GetComponent<Renderer>().sharedMaterial = material[x];
                    }
                    if(pipes[x, i].transform.GetChild(j).childCount == 0)
                    {
                        pipes[x, i].transform.GetChild(j).GetComponent<Renderer>().sharedMaterial = material[x];
                    }
                }
            }
        }
        pipes[0, 0] = transform.GetChild(0).GetChild(1).gameObject;
        Material[] mats = pipes[0, 0].transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials;
        mats[3] = material[0];
        for (int i = 0; i < pipes[0, 0].transform.childCount; i++)
        {
            pipes[0, 0].transform.GetChild(i).GetChild(0).GetComponent<Renderer>().materials = mats;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
