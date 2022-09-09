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
        ReadText();
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
        PlayerPrefs.SetString("key", PlayerPrefs.GetString("key"));
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
