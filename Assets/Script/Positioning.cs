using UnityEngine;
using System.Collections;

public class Positioning : MonoBehaviour {
    private float height;
    // Use this for initialization
	void Update () {
        height = transform.GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.GetChild(0).localScale.y;
        transform.parent.localPosition= new Vector3(0,height,0);
	}
	
}
