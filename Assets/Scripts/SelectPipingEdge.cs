using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPipingEdge : MonoBehaviour
{
    // Use this for initialization
    public GameObject colorPipePanel;
    public GameObject pickPipePanel;
    public GameObject PipingLable;
    public GameObject PipingButton;
    public Button[] pipe;
    public GameObject pipes;
    private int numOfPipe;
    private int current;

    public GameObject selectColor;
    private Button[] colorButton;
    private Vector4[] color;
    private Vector4 hsl, rgb;
    private float intensity;
    public Slider slider;
    float H, S, V;
    private int index;

    public Material[] material;
    public int tier;

    private int shape;
    void Start()
    {
        color = new Vector4[17];
        color[0] = new Vector4(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
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

        slider.interactable = false;

        colorButton = new Button[selectColor.transform.childCount];
        for (int i = 0; i < selectColor.transform.childCount; i++)
        {
            colorButton[i] = selectColor.transform.GetChild(i).GetComponent<Button>();
            Button b = colorButton[i];
            AddListenerColor(b, i);
        }
        colorButton[0].interactable = false;
        intensity = 0.5f;


        numOfPipe = pipes.transform.childCount;
        pipe = new Button[numOfPipe];
        current = 0;
        for (int i = 0; i < numOfPipe; i++)
        {
            pipe[i] = pipes.transform.GetChild(i).GetComponent<Button>();
            Button b = pipe[i];
            AddListener(b, i);
        }
        tier = transform.GetSiblingIndex() + 1;
        material = new Material[3];

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
        material[1].color = Color.white;
    }
    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => pickPipe(b, i));
    }

    void AddListenerColor(Button b, int i)
    {
        b.onClick.AddListener(() => pickColor(b, i));
    }

    public void pickPipe(Button b, int x)
    {
        if (transform.GetComponent<SelectPipingTop>().pipe[0].interactable)
        {
            transform.GetComponent<SelectPipingTop>().pickPipe(transform.GetComponent<SelectPipingTop>().pipe[0], 0);
        }
        for (int i = 0; i < numOfPipe; i++)
        {
            pipe[i].interactable = true;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                shape = i;
            }
        }
        hidePipe(shape);
        b.interactable = false;
        if (x != 0)
        {
            current = x;
            StartCoroutine(pipeAnimate(current,shape));
            showColorPanel();
        }
        
    }

    void pickColor(Button b, int x)
    {
        if (x != 0)
        {
            slider.interactable = true;
        }
        for (int i = 0; i < selectColor.transform.childCount; i++)
        {
            colorButton[i].interactable = true;
        }

        material[1].color = color[x];
        index = x;
        b.interactable = false;
        slider.value = 0.5f;
        intensity = 0.5f;
        if (index == 0)
        {
            slider.interactable = false;
        }
    }

    void hidePipe(int shape)
    {
        for (int i = 0; i < transform.GetChild(shape).GetChild(0).GetChild(0).GetChild(1).GetChild(current).childCount; i++)
        {
            transform.GetChild(shape).GetChild(0).GetChild(0).GetChild(1).GetChild(current).GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator pipeAnimate(int current,int shape)
    {
        for (int i = 0; i < transform.GetChild(shape).GetChild(0).GetChild(0).GetChild(1).GetChild(current).childCount; i++)
        {
            transform.GetChild(shape).GetChild(0).GetChild(0).GetChild(1).GetChild(current).GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.02f);
        }
    }

    void showColorPanel()
    {
        pickPipePanel.SetActive(false);
        PipingLable.SetActive(false);
        PipingButton.SetActive(false);
        colorPipePanel.SetActive(true);
    }

    public void hideColorPanel()
    {
        colorPipePanel.SetActive(false);
        PipingLable.SetActive(true);
        PipingButton.SetActive(true);
        pickPipePanel.SetActive(true);
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

    void Update()
    {
        hsl = RgbaToHsl(color[index]);
        if (index != 0)
        {
            if (slider.value >= 0.5)
            {
                intensity = slider.value;
                hsl.z = intensity;
                rgb = HslToRgba(hsl);
                material[1].color = rgb;
            }
            if (slider.value < 0.5)
            {

                Color.RGBToHSV(color[index], out H, out S, out V);

                intensity = (slider.value / 0.5f) * V;
                V = intensity;
                material[1].color = Color.HSVToRGB(H, S, V);
            }
        }

    }
}
