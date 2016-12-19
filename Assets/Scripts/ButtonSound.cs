using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]

public class ButtonSound : MonoBehaviour {

    // Use this for initialization
    private AudioClip click;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
	void Start () {
        click = Resources.Load("Sound/click") as AudioClip;
        gameObject.AddComponent<AudioSource>();
        source.clip = click;
        source.playOnAwake = false;

        button.onClick.AddListener(() => playSound());
	}

    public void playSound()
    {
        source.PlayOneShot(click);
    }
}
