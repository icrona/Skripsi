using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextEdit : MonoBehaviour {

    private GameObject TextPanel;
    private GameObject selectColor;
    private GameObject colorPanel;
    private GameObject slider;
    private Button [] colorButton;
    public Vector4[] color;
    public Vector4 hsl, rgb;
    public float intensity;
    private float temp;
    float H, S, V;
    private int index;
    public int tier;
    public int textIndex;
    private GameObject textField;
    private bool touch;
    // Use this for initialization
    void Start () {
        TextPanel = transform.parent.parent.parent.parent.parent.parent.GetChild(0).GetChild(3).gameObject;
        tier = transform.parent.parent.parent.GetSiblingIndex();
        textIndex = transform.GetSiblingIndex();
        textField = TextPanel.transform.GetChild(tier).GetChild(0).GetChild(2).gameObject;
    
        colorPanel = TextPanel.transform.GetChild(tier).GetChild(0).GetChild(0).gameObject;
        selectColor = colorPanel.transform.GetChild(2).GetChild(0).gameObject;
        slider = colorPanel.transform.GetChild(0).GetChild(0).gameObject;
        PlayerPrefs.SetInt("TextIndex", -1);
        PlayerPrefs.SetInt("TextTier", -1);

        color = new Vector4[17];
        color[0] = new Vector4(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        color[1] = new Vector4(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        color[2] = new Vector4(0 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
        color[3] = new Vector4(255 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
        color[4] = new Vector4(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);

        color[5] = new Vector4(255 / 255f, 0 / 255f, 128 / 255f, 255 / 255f);
        color[6] = new Vector4(128 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
        color[7] = new Vector4(255 / 255f, 69 / 255f, 0 / 255f, 255 / 255f);
        color[8] = new Vector4(128 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);

        color[9] = new Vector4(255 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
        color[10] = new Vector4(0 / 255f, 255 / 255f, 128 / 255f, 255 / 255f);
        color[11] = new Vector4(255 / 255f, 140 / 255f, 0 / 255f, 255 / 255f);
        color[12] = new Vector4(0 / 255f, 128 / 255f, 255 / 255f, 255 / 255f);

        color[13] = new Vector4(220 / 255f, 20 / 255f, 60 / 255f, 255 / 255f);
        color[14] = new Vector4(0 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        color[15] = new Vector4(181 / 255f, 112 / 255f, 74 / 255f, 255 / 255f);
        color[16] = new Vector4(128 / 255f, 128 / 255f, 128 / 255f, 255 / 255f);

        slider.GetComponent<Slider>().interactable = false;
        colorButton = new Button[selectColor.transform.childCount];
        for (int i = 0; i < selectColor.transform.childCount; i++)
        {
            colorButton[i] = selectColor.transform.GetChild(i).GetComponent<Button>();
            Button b = colorButton[i];
            AddListener(b, i);
        }
        colorButton[0].interactable = false;
        intensity = 0.5f;
    }
    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => pickColor(b, i));
    }

    void pickColor(Button b, int x)
    {
        if (x != 0)
        {
            slider.GetComponent<Slider>().interactable = true;
        }
        for (int i = 0; i < selectColor.transform.childCount; i++)
        {
            colorButton[i].interactable = true;
        }
        if (transform.parent.parent.parent.GetSiblingIndex() == PlayerPrefs.GetInt("TextTier")
            && transform.GetSiblingIndex() == PlayerPrefs.GetInt("TextIndex"))
        {
            transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color = color[x];
        }
        index = x;
        b.interactable = false;
        slider.GetComponent<Slider>().value = 0.5f;
        intensity = 0.5f;
    }

    public static Vector4 HslToRgba(Vector4 hsl)
    {
        float r, g, b;

        if (hsl.y == 0.0f)
            r = g = b = hsl.z;

        else
        {
            var q = hsl.z < 0.5f ? hsl.z * (1.0f + hsl.z) : hsl.z + hsl.y - hsl.z * hsl.y;
            var p = 2.0f * hsl.z - q;
            r = HueToRgb(p, q, hsl.x + 1.0f / 3.0f);
            g = HueToRgb(p, q, hsl.x);
            b = HueToRgb(p, q, hsl.x - 1.0f / 3.0f);
        }

        return new Vector4(r, g, b, 1);
    }

    public static Vector4 RgbaToHsl(Vector4 rgba)
    {
        float r = rgba.x;
        float g = rgba.y;
        float b = rgba.z;

        float max = (r > g && r > b) ? r : (g > b) ? g : b;
        float min = (r < g && r < b) ? r : (g < b) ? g : b;

        float h, s, l;
        h = s = l = (max + min) / 2.0f;

        if (max == min)
            h = s = 0.0f;

        else
        {
            float d = max - min;
            s = (l > 0.5f) ? d / (2.0f - max - min) : d / (max + min);

            if (r > g && r > b)
                h = (g - b) / d + (g < b ? 6.0f : 0.0f);

            else if (g > b)
                h = (b - r) / d + 2.0f;

            else
                h = (r - g) / d + 4.0f;

            h /= 6.0f;
        }

        return new Vector4(h, s, l, rgba.w);
    }

    private static float HueToRgb(float p, float q, float t)
    {
        if (t < 0.0f) t += 1.0f;
        if (t > 1.0f) t -= 1.0f;
        if (t < 1.0f / 6.0f) return p + (q - p) * 6.0f * t;
        if (t < 1.0f / 2.0f) return q;
        if (t < 2.0f / 3.0f) return p + (q - p) * (2.0f / 3.0f - t) * 6.0f;
        return p;
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetInt("TextIndex",textIndex);
        PlayerPrefs.SetInt("TextTier", tier);
        textField.GetComponent<InputField>().text = transform.parent.GetChild(textIndex).GetComponent<TextMesh>().text;
        temp = slider.GetComponent<Slider>().value;
        intensity = 0.5f;
        slider.GetComponent<Slider>().value = 0.5f;
        for (int i=0;i<color.Length;i++)
        {
            if (color[i] == (Vector4)transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color)
            {
                pickColor(colorButton[i], i);
                if (i == 0)
                {
                    transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color = Color.black;
                }
            }
        }

        intensity = temp;
        slider.GetComponent<Slider>().value = temp;
    }

	// Update is called once per frame
	void Update (){
        if (transform.parent.parent.parent.GetSiblingIndex() == PlayerPrefs.GetInt("TextTier")
            && transform.GetSiblingIndex()== PlayerPrefs.GetInt("TextIndex"))
        {
            transform.parent.GetChild(textIndex).GetComponent<TextMesh>().text = textField.GetComponent<InputField>().text;

                hsl = RgbaToHsl(color[index]);
            if (color[index] == (Vector4)Color.black)
            {
                transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color = Color.black;
            }
            else
            {
                if (slider.GetComponent<Slider>().value >= 0.5)
                {
                    intensity = slider.GetComponent<Slider>().value;
                    hsl.z = intensity;
                    rgb = HslToRgba(hsl);
                    transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color=rgb;
                }
                if (slider.GetComponent<Slider>().value < 0.5)
                {

                    Color.RGBToHSV(color[index], out H, out S, out V);

                    intensity = (slider.GetComponent<Slider>().value / 0.5f) * V;
                    V = intensity;
                    transform.parent.GetChild(textIndex).GetComponent<TextMesh>().color = Color.HSVToRGB(H, S, V);
                }
            }
                
        }       
    }
}
