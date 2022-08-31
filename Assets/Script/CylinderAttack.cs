using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderAttack : MonoBehaviour
{
    public GameObject Sword;
    public GameObject ColliderCube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Sword.SetActive(true);
            ColliderCube.transform.localScale = new Vector3(1, 1, 1);
            ColliderCube.SetActive(true);
        }
        else if (other.tag == "Enemy3")
        {
            Sword.SetActive(true); 
            ColliderCube.transform.localScale = new Vector3(1, 1, 1);
            ColliderCube.SetActive(true);
        }
    }
}
