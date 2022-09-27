using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSkinEnding : MonoBehaviour
{
    public GameObject ModelCharacter;
    public Renderer MaterialWeaponSpecial;
    public List<Material> ListMaterialWeaponSpecial;
    public List<GameObject> ListModelWeapons;
    public List<Texture> ListTextureSkin;
    public void OnModel(int TypeShop, int IndexSkin)
    {
        ModelCharacter.SetActive(false);
        for (int i = 0; i < ListModelWeapons.Count; i++)
        {
            ListModelWeapons[i].SetActive(false);
        }
        if (TypeShop == 2)
        {
            ModelCharacter.SetActive(true);
            GameObject ModelArmor = ModelCharacter.transform.Find("Armor").gameObject;
            GameObject ModelBase = ModelCharacter.transform.Find("MineCraft_Base").gameObject;
            ModelArmor.GetComponent<Renderer>().material.mainTexture = ListTextureSkin[IndexSkin];
            ModelBase.GetComponent<Renderer>().material.mainTexture = ListTextureSkin[IndexSkin];
            MaterialWeaponSpecial.material = ListMaterialWeaponSpecial[IndexSkin];
        }
        else if (TypeShop == 1)
        {
            ListModelWeapons[IndexSkin].SetActive(true);
        }

    }
}
