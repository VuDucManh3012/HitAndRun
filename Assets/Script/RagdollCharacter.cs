using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour
{
    private Vector3 Force;
    Rigidbody myRigid;
    public GameObject ModelArmor;
    public GameObject ModelCharacter;
    public List<Texture> ListTexture;
    public int IndexSkin;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        Force.x = Random.Range(0, 1);
        Force.y = 1;
        Force.z = Random.Range(0, 1);
        myRigid.AddForce(Force * 100, ForceMode.Impulse);
    }
    public void RagdollChangeSkin(int NumberTextSkin ,double mylevel)
    {
        ModelArmor.GetComponent<Renderer>().material.mainTexture = ListTexture[NumberTextSkin];
        ModelCharacter.GetComponent<Renderer>().material.mainTexture = ListTexture[NumberTextSkin];
    }
}
