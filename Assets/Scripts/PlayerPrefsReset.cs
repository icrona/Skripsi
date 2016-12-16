using UnityEngine;
using System.Collections;

public class PlayerPrefsReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("SizeTier1", PlayerPrefs.GetInt("NumOfSize")/2);
        PlayerPrefs.SetInt("FlavourTier1", 0);
        PlayerPrefs.SetInt("SizeTier2", -1);
        PlayerPrefs.SetInt("SizeTier3", -1);

        
        PlayerPrefs.SetInt("FlavourTier2", 0);
        PlayerPrefs.SetInt("FlavourTier3", 0);

        PlayerPrefs.SetInt("Frosting", -1);

        PlayerPrefs.SetInt("NumberOfTiers", 1);

        for(int i = 1; i <= 3; i++)
        {
            PlayerPrefs.SetInt("FrostingTier"+i, 2);
            PlayerPrefs.SetInt("SprinkleTopTier" + i, 0);
            PlayerPrefs.SetInt("SprinkleSideTier" + i, 0);
            PlayerPrefs.SetInt("PipeTopTier" + i, 0);
            PlayerPrefs.SetInt("PipeEdgeTier" + i, 0);
            PlayerPrefs.SetInt("PipeBottomTier" + i, 0);
        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
