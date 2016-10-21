using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    
    public GameObject colorButtonPrefab;
    public GameObject colorButtonContainer;

    public Material cakeMaterial;
    private void Start()
    {
        
        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Color");
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate(colorButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(colorButtonContainer.transform, false);

            int index = textureIndex;
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            textureIndex++;
        }
    }


    private void ChangePlayerSkin(int index)
    {
        float x = (index % 4) * 0.25f;
        float y = ((int)index / 4) * 0.25f;
        
        if (y==0.0f)
        {
            y = 0.75f;
        }
        else if (y==0.25f)
        {
            y = 0.5f;
        }
        else if (y == 0.50f)
        {
            y = 0.25f;
        }
        else if (y == 0.75f)
        {
            y = 0f;
        }

        cakeMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
    }

    
}
