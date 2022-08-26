using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ModelChangeSkin : MonoBehaviour
{
    public List<int> WeaponNoBuy;
    public List<int> SkinNoBuy;
    public int TypeShop = 0;
    public int indexSkin = 10;

    public GameObject ParentModelSkin;
    public GameObject ParentModelWeapon;
    public GameObject Effect;

    CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        changeSkinModel();
        checkYON();
    }
    void changeSkinModel()
    {
        AddAtList();
        RandomSkin();
        FromTextureToSkin();
    }
    void AddAtList()
    {
        WeaponNoBuy.Clear();
        SkinNoBuy.Clear();
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("weaponSpecial" + i) < 2 || !PlayerPrefs.HasKey("weaponSpecial" + i))
            {
                WeaponNoBuy.Add(i);
            }
            if (PlayerPrefs.GetInt("skinSpecial" + i) < 2 || !PlayerPrefs.HasKey("skinSpecial" + i))
            {
                SkinNoBuy.Add(i);
            }
        }
    }
    void RandomSkin()
    {
        //Random TypeShop
        if (WeaponNoBuy.Count > 0 && SkinNoBuy.Count > 0)
        {
            int stage = System.Int32.Parse(PlayerPrefs.GetString("stage"));
            if (stage % 10 == 8)
            {
                TypeShop = 2;
            }
            else if (stage % 5 == 3)
            {
                TypeShop = 1;
            }
        }
        else if (WeaponNoBuy.Count > 0)
        {
            TypeShop = 1;
        }
        else if (SkinNoBuy.Count > 0)
        {
            TypeShop = 2;
        }
        else
        {
            TypeShop = 0;
        }

        //Random IndexSkin
        if (TypeShop == 1)
        {
            indexSkin = Random.Range(0, WeaponNoBuy.Count);
        }
        else if (TypeShop == 2)
        {
            indexSkin = Random.Range(0, SkinNoBuy.Count);
        }
    }
    void FromTextureToSkin()
    {
        //type ==1 : Weapon ; type==2:Skin
        if (indexSkin != 10 && TypeShop == 1)
        {
            ParentModelWeapon.SetActive(true);
            ParentModelWeapon.transform.GetChild(indexSkin).gameObject.SetActive(true);
            Effect.SetActive(true);
        }
        else if (indexSkin != 10 && TypeShop == 2)
        {
            ParentModelSkin.SetActive(true);
            ParentModelSkin.transform.GetChild(indexSkin).gameObject.SetActive(true);
            Effect.SetActive(true);
        }
    }
    void checkYON()
    {
        if (indexSkin == 10 || TypeShop == 0)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = false;
            ParentModelWeapon.SetActive(false);
            ParentModelSkin.SetActive(false);
            Effect.SetActive(false);
        }
    }
}
