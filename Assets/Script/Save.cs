using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public Text Diamond;
    public Text Key;
    public Text Stage;
    public GameObject Char;

    public Text DiamondOfflineText;
    public GameObject PopupOfflineReward;
    public GameObject GameStartScene;
    public double DiamondBonusOffline;
    public void ReadDimondOffline()
    {
        if (PlayerPrefs.HasKey("DateBefore"))
        {
            //Tinh tg offline
            System.DateTime DateBefore = System.DateTime.Parse(PlayerPrefs.GetString("DateBefore"));
            System.DateTime DateNow = System.DateTime.Now;
            System.TimeSpan t = DateNow - DateBefore;
            double Distance = Mathf.Abs(ToSingle(t.TotalMinutes));
            Distance = Distance - Distance % 1;
            //
            if (Distance >= 10)
            {
                //hien popup
                PopupOfflineReward.SetActive(true);
                GameStartScene.SetActive(false);
                //
                if (PlayerPrefs.HasKey("DistanceMax"))
                {
                    int DistanceMax = PlayerPrefs.GetInt("DistanceMax");

                    if (Distance >= DistanceMax)
                    {
                        DiamondBonusOffline = DistanceMax * 10;
                    }
                    else
                    {

                        DiamondBonusOffline = Distance * 10;
                    }
                    DiamondOfflineText.text = DiamondBonusOffline.ToString();
                }
            }
        }
    }
    public void Awake()
    {
        Start();
    }
    public void Start()
    {
        if (!PlayerPrefs.HasKey("diamond"))
        {
            WriteTextFirst();
        }
        ReadText();
        DiamondBonusOffline = 0;
        PopupOfflineReward.SetActive(false);
        GameStartScene.SetActive(true);
        ReadText();
        ReadDimondOffline();
    }
    [ContextMenu("AddKey")]
    public void AddKey()
    {
        int currentKey =System.Int32.Parse(PlayerPrefs.GetString("key"));
        currentKey += 1;
        PlayerPrefs.SetString("key", currentKey.ToString());
        Debug.Log(PlayerPrefs.GetString("key"));
    }

    public void ReadText()
    {
        Diamond.text = PlayerPrefs.GetString("diamond");
        Char.GetComponent<ControllerPlayer>().QualityDiamond = double.Parse(PlayerPrefs.GetString("diamond"));

        Key.text = PlayerPrefs.GetString("key");

        Stage.text = "Stage " + PlayerPrefs.GetString("stage");
        Char.GetComponent<ControllerPlayer>().QualityStage = double.Parse(PlayerPrefs.GetString("stage"));
    }
    public void WriteText()
    {
        PlayerPrefs.SetString("diamond", Diamond.text);
        PlayerPrefs.SetString("key", Key.text);
        PlayerPrefs.SetString("stage", Char.GetComponent<ControllerPlayer>().QualityStage.ToString());
    }
    public void WriteTextFirst()
    {
        PlayerPrefs.SetString("diamond", 0.ToString());
        PlayerPrefs.SetString("key", 0.ToString());
        PlayerPrefs.SetString("stage", 1.ToString());
    }
    [ContextMenu("AddDiamond")]
    public void AddDiamond()
    {
        PlayerPrefs.SetString("diamond", 10000.ToString());
    }
    public static float ToSingle(double value)
    {
        return (float)value;
    }
    [ContextMenu("DeleteAll")]
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
