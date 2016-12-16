using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CheckConnection : MonoBehaviour
{
    public IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://skripsweet.xyz");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
}
	