using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator myanim;
    // Start is called before the first frame update
    void Start()
    {
        myanim = GetComponent<Animator>();
        myanim.SetInteger("AnimRandom", Random.Range(1, 4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
