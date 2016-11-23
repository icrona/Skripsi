using UnityEngine;
using System.Collections;

public class PlayerPrefsReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("FlavourTier1", -1);
        PlayerPrefs.SetInt("FlavourTier2", -1);
        PlayerPrefs.SetInt("FlavourTier3", -1);

        PlayerPrefs.SetInt("Frosting", -1);

        PlayerPrefs.SetInt("SizeTier1", -1);
        PlayerPrefs.SetInt("SizeTier2", -1);
        PlayerPrefs.SetInt("SizeTier3", -1);

        PlayerPrefs.SetInt("NumberOfTiers", 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
