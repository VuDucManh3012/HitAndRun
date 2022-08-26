using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderProcessStage : MonoBehaviour
{
    public List<GameObject> ListImageStage;
    public GameObject ParentImageStage;

    public Slider sliderProcessStage;
    public int StageCurrent;

    [Header("Environment")]
    public Image Environment1;
    public Image Environment2;
    public List<Sprite> ListImageEnvironment1;
    public List<Sprite> ListImageEnvironment2;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            StageCurrent = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        }
        catch
        {
            StageCurrent = 1;
        }
        BatImage();
        SetImageEnviroment();
    }
    void SetImageEnviroment()
    {
        int stage2 = StageCurrent % 26;
        if (stage2 <= 5)
        {
            Environment1.sprite = ListImageEnvironment1[0];
            Environment2.sprite = ListImageEnvironment2[0];
        }
        else if (stage2 <= 10)
        {
            Environment1.sprite = ListImageEnvironment1[1];
            Environment2.sprite = ListImageEnvironment2[1];
        }
        else if (stage2 <= 15)
        {
            Environment1.sprite = ListImageEnvironment1[2];
            Environment2.sprite = ListImageEnvironment2[2];
        }
        else if (stage2 <= 20)
        {
            Environment1.sprite = ListImageEnvironment1[3];
            Environment2.sprite = ListImageEnvironment2[3];
        }
        else if (stage2 <= 25)
        {
            Environment1.sprite = ListImageEnvironment1[4];
            Environment2.sprite = ListImageEnvironment2[4];
        }

    }
    void BatImage()
    {
        int stage;
        try
        {
            stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
        }
        catch
        {
            stage = 1;
        }

        int stage2 = stage % 5;
        switch (stage2)
        {
            case 1:
                sliderProcessStage.value = 17;
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        BatImageDangVao(ListImageStage[i]);
                    }
                    if (i > 0)
                    {
                        BatImageChuaQua(ListImageStage[i]);
                    }
                }
                setText(0);
                break;
            case 2:
                sliderProcessStage.value = 34;

                for (int i = 0; i < 5; i++)
                {
                    if (i < 1)
                    {
                        BatImageDaQua(ListImageStage[i]);
                    }
                    if (i == 1)
                    {
                        BatImageDangVao(ListImageStage[i]);
                    }
                    if (i > 1)
                    {
                        BatImageChuaQua(ListImageStage[i]);
                    }
                }
                setText(1);
                break;
            case 3:
                sliderProcessStage.value = 50;
                for (int i = 0; i < 5; i++)
                {
                    if (i < 2)
                    {
                        BatImageDaQua(ListImageStage[i]);
                    }
                    if (i == 2)
                    {
                        BatImageDangVao(ListImageStage[i]);
                    }
                    if (i > 2)
                    {
                        BatImageChuaQua(ListImageStage[i]);
                    }
                }
                setText(2);
                break;
            case 4:
                sliderProcessStage.value = 67;
                for (int i = 0; i < 5; i++)
                {
                    if (i < 3)
                    {
                        BatImageDaQua(ListImageStage[i]);
                    }
                    if (i == 3)
                    {
                        BatImageDangVao(ListImageStage[i]);
                    }
                    if (i > 3)
                    {
                        BatImageChuaQua(ListImageStage[i]);
                    }
                }
                setText(3);
                break;
            case 0:
                sliderProcessStage.value = 84;
                for (int i = 0; i < 5; i++)
                {
                    if (i < 4)
                    {
                        BatImageDaQua(ListImageStage[i]);
                    }
                    if (i == 4)
                    {
                        BatImageDangVao(ListImageStage[i]);
                    }
                }
                setText(4);
                break;
            default:
                break;
        }
    }
    void BatImageDaQua(GameObject gameObject)
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }
    void BatImageDangVao(GameObject gameObject)
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    void BatImageChuaQua(GameObject gameObject)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    void setText(int distanceToTop)
    {
        int a = distanceToTop;
        foreach (GameObject item in ListImageStage)
        {
            for (int i = 0; i < 3; i++)
            {
                if (item.transform.GetChild(i).gameObject.active)
                {
                    item.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = (StageCurrent - a).ToString();
                    break;
                }
            }
            a--;
        }
    }
}
