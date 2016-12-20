using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

    // Use this for initialization
    private AudioClip click;
    private AudioClip frosting;
    private AudioClip sprinkle;
    private AudioClip piping;
    private AudioClip screenshot;
    private AudioClip cake;
    private Button button;
    private Toggle toggle;
    private Dropdown dropdown;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
	void Start () {
        click = Resources.Load("Sound/click") as AudioClip;
        frosting = Resources.Load("Sound/frosting") as AudioClip;
        sprinkle = Resources.Load("Sound/sprinkle") as AudioClip;
        screenshot = Resources.Load("Sound/screenshot") as AudioClip;
        cake = Resources.Load("Sound/cake") as AudioClip;
        piping = Resources.Load("Sound/piping") as AudioClip;
        gameObject.AddComponent<AudioSource>();
        source.clip = click;
        source.playOnAwake = false;
        if (gameObject.GetComponent<Button>() != null)
        {
            button = gameObject.GetComponent<Button>();
            if (gameObject.tag == "Sprinkle")
            {
                button.onClick.AddListener(() => playSprinkle());
            }
            else if (gameObject.tag == "Screenshot")
            {
                button.onClick.AddListener(() => screenshotSound());
            }
            else if(gameObject.tag == "Cake")
            {
                button.onClick.AddListener(() => playCake());
            }
            else{
                button.onClick.AddListener(() => playSound());
            }
            
        }
        else if(gameObject.GetComponent<Toggle>() != null)
        {
            toggle = gameObject.GetComponent<Toggle>();
            if (gameObject.tag == "Frosting")
            {
                toggle.onValueChanged.AddListener(delegate { playFrosting(); });
            }
            else
            {
                toggle.onValueChanged.AddListener(delegate { playSound(); });
            }
            
        }
        else if (gameObject.GetComponent<Dropdown>() != null)
        {
            dropdown = gameObject.GetComponent<Dropdown>();
            dropdown.onValueChanged.AddListener(delegate { playSound(); });
        }
    }

    public void playSound()
    {
        if (PlayerPrefs.GetInt("Sound") ==1)
        {
            source.PlayOneShot(click);
        }
        
    }
    public void playFrosting()
    {
        if (PlayerPrefs.GetInt("Sound") ==1)
        {
            source.PlayOneShot(frosting);
        }
    }
    public void playSprinkle()
    {
        if (PlayerPrefs.GetInt("Sound") ==1)
        {
            source.PlayOneShot(sprinkle);
        }
    }
    public void playCake()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            source.PlayOneShot(cake);
        }
    }
    public void screenshotSound()
    {
        if (PlayerPrefs.GetInt("Sound") ==1)
        {
            source.PlayOneShot(screenshot);
        }
    }

    public void playPiping()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            source.PlayOneShot(piping);
        }
    }
}
