using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Save : MonoBehaviour
{
    public TMP_Text Diamond;
    public Text Key;
    public Text Stage;
    public GameObject Char;

    public GameObject GameStartScene;

    public CanvasManager CanvasManager;
    public GameObject QualityKey;
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
        GameStartScene.SetActive(true);
        CanvasManager.CanvasQualityKeyController();
        ReadText();
    }
    [ContextMenu("AddKey")]
    public void AddKey()
    {
        int currentKey = System.Int32.Parse(PlayerPrefs.GetString("key"));
        currentKey += 1;
        PlayerPrefs.SetString("key", currentKey.ToString());
    }
    public void ReadText()
    {
        Diamond.text = PlayerPrefs.GetInt("diamond").ToFormatString();
        Char.GetComponent<ControllerPlayer>().QualityDiamond = PlayerPrefs.GetInt("diamond");

        Key.text = PlayerPrefs.GetString("key");

        Stage.text = "Stage " + PlayerPrefs.GetString("stage");
        Char.GetComponent<ControllerPlayer>().QualityStage = double.Parse(PlayerPrefs.GetString("stage"));
    }
    public void WriteText()
    {
        PlayerPrefs.SetInt("diamond", (int)Char.GetComponent<ControllerPlayer>().QualityDiamond);
        PlayerPrefs.SetString("key", PlayerPrefs.GetString("key"));
        PlayerPrefs.SetString("stage", Char.GetComponent<ControllerPlayer>().QualityStage.ToString());
    }
    public void WriteTextFirst()
    {
        PlayerPrefs.SetInt("diamond", 0);
        PlayerPrefs.SetString("key", 0.ToString());
        PlayerPrefs.SetString("stage", 1.ToString());
    }
    [ContextMenu("AddDiamond")]
    public void AddDiamond()
    {
        CanvasManager.DiamondFly(10000, null);
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
