using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public GameObject musicBtn;
    public GameObject soundBtn;
    public Sprite musicOff;
    public Sprite musicOn;
    public Sprite soundOff;
    public Sprite soundOn;

    void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            soundBtn.GetComponent<Image>().sprite = soundOff;
            soundBtn.transform.parent.GetComponent<Toggle>().isOn=true;
        }
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            musicBtn.GetComponent<Image>().sprite = musicOff;
            musicBtn.transform.parent.GetComponent<Toggle>().isOn = true;
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
	public void muteSound(bool mute)
    {
        if (mute)
        {
            soundBtn.GetComponent<Image>().sprite = soundOff;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            soundBtn.GetComponent<Image>().sprite = soundOn;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
    public void muteMusic(bool mute)
    {
        if (mute)
        {
            musicBtn.GetComponent<Image>().sprite = musicOff;
            gameObject.GetComponent<AudioSource>().mute = true;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            musicBtn.GetComponent<Image>().sprite = musicOn;
            gameObject.GetComponent<AudioSource>().mute = false;
            PlayerPrefs.SetInt("Music", 1);
        }
        
    }
}
