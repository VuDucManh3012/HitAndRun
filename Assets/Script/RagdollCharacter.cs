using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    private Vector3 Force;
    Rigidbody myRigid;
    public GameObject ModelArmor;
    public GameObject ModelCharacter;
    public GameObject WeaponSpecial;

    public List<Texture> ListTexture;
    public int IndexSkin;

    private bool AddedForce;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();

        if (Random.Range(0, 2) == 0)
        {
            Force.x = -0.1f;
        }
        else
        {
            Force.x = 0.1f;
        }
        Force.y = 1;
        Force.z = 0;
        AddedForce = false;
    }
    private void Update()
    {
        if (!AddedForce)
        {
            myRigid.AddForce(Force * 300, ForceMode.Impulse);
            AddedForce = true;
        }

    }
    private bool RagdollActive = false;
    public void RagdollChangeSkin(int NumberTextSkin, double mylevel)
    {
        if (!RagdollActive)
        {
            mylevel += 10000;
            if (mylevel >= double.Parse("250"))
            {
                ModelArmor.SetActive(true);
                ModelArmor.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                ModelCharacter.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                WeaponSpecial.transform.Find(PlayerPrefs.GetString("CurrentSkin").Remove(PlayerPrefs.GetString("CurrentSkin").Length - 1)).gameObject.SetActive(true);
            }
            ModelArmor.GetComponent<Renderer>().material.mainTexture = ListTexture[NumberTextSkin];
            ModelCharacter.GetComponent<Renderer>().material.mainTexture = ListTexture[NumberTextSkin];
            RagdollActive = true;
        }
    }
}
