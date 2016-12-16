using UnityEngine;
using System.Collections;

public class PlayerPrefsReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("SizeTier1", PlayerPrefs.GetInt("NumOfSize")/2);
        PlayerPrefs.SetInt("SizeTier2", -1);
        PlayerPrefs.SetInt("SizeTier3", -1);

        PlayerPrefs.SetInt("FlavourTier1", 0);
        PlayerPrefs.SetInt("FlavourTier2", 0);
        PlayerPrefs.SetInt("FlavourTier3", 0);

        PlayerPrefs.SetInt("Frosting", -1);

        PlayerPrefs.SetInt("FrostingTier1", 2);
        PlayerPrefs.SetInt("FrostingTier2", 2);
        PlayerPrefs.SetInt("FrostingTier3", 2);


        PlayerPrefs.SetInt("NumberOfTiers", 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
