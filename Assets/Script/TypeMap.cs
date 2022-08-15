using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMap : MonoBehaviour
{
    public int stage;
    public int IndexTextures;
    private Color ColorFog;
    [Header("Road")]
    public Material[] ListMaterialRoad;
    public Texture[] TexturesRoad;

    [Header("Wall")]
    public Material[] ListMaterialWall;
    public Texture[] TexturesWall;

    [Header("Birdge")]
    public Material MaterialBirdge;
    public Texture[] TexturesBirdge;

    [Header("Environment")]
    public GameObject EnvironmentManager;
    public List<Material> ListSkyBox;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("stage"))
        {
            stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        }
        else
        {
            stage = 1;
        }

        if (stage < 25)
        {
            stage = stage / 5;
        }
        else
        {
            stage = stage % 25 / 5;
        }
        switch (stage)
        {
            case 0:
                IndexTextures = 0;
                ColorUtility.TryParseHtmlString("#55A3AC", out ColorFog);
                EnvironmentManager.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                IndexTextures = 1;
                ColorUtility.TryParseHtmlString("#B8D0FE", out ColorFog);
                EnvironmentManager.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                IndexTextures = 2;
                ColorUtility.TryParseHtmlString("#B2EDD9", out ColorFog);
                EnvironmentManager.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                IndexTextures = 3;
                ColorUtility.TryParseHtmlString("#3A9CA8", out ColorFog);
                EnvironmentManager.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 4:
                IndexTextures = 4;
                ColorUtility.TryParseHtmlString("#B6EBBD", out ColorFog);
                EnvironmentManager.transform.GetChild(4).gameObject.SetActive(true);
                break;
            default:
                IndexTextures = 0;
                ColorUtility.TryParseHtmlString("#55A3AC", out ColorFog);
                EnvironmentManager.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
        for (int i = 0; i <= ListMaterialRoad.Length - 1; i++)
        {
            ListMaterialRoad[i].mainTexture = TexturesRoad[IndexTextures];
            ListMaterialWall[i].mainTexture = TexturesWall[IndexTextures];
        }
        MaterialBirdge.mainTexture = TexturesBirdge[IndexTextures];
        RenderSettings.skybox = ListSkyBox[IndexTextures];
        RenderSettings.fogColor = ColorFog;
    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("ChangeSkin")]
    public void ChangeSkin()
    {
        stage += 7;
        Star2t();
    }
    void Star2t()
    {
        int stage2 = stage;
        if (stage2 < 35)
        {
            stage2 = stage2 / 7;
        }
        else
        {
            stage2 = stage2 % 35 / 7;
        }
        switch (stage2)
        {
            case 0:
                IndexTextures = 0;
                ColorUtility.TryParseHtmlString("#55A3AC", out ColorFog);
                break;
            case 1:
                IndexTextures = 1;
                ColorUtility.TryParseHtmlString("#B8D0FE", out ColorFog);
                break;
            case 2:
                IndexTextures = 2;
                ColorUtility.TryParseHtmlString("#B2EDD9", out ColorFog);
                break;
            case 3:
                IndexTextures = 3;
                ColorUtility.TryParseHtmlString("#3A9CA8", out ColorFog);
                break;
            case 4:
                IndexTextures = 4;
                ColorUtility.TryParseHtmlString("#B6EBBD", out ColorFog);
                break;
            default:
                IndexTextures = 0;
                ColorUtility.TryParseHtmlString("#55A3AC", out ColorFog);
                break;
        }
        for (int i = 0; i <= ListMaterialRoad.Length - 1; i++)
        {
            ListMaterialRoad[i].mainTexture = TexturesRoad[IndexTextures];
            ListMaterialWall[i].mainTexture = TexturesWall[IndexTextures];
        }
        RenderSettings.fog = true;
        RenderSettings.fogColor = ColorFog;
    }
}
