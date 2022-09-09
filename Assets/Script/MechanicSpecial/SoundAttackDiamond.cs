using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAttackDiamond : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SfxVolumn");
        audioSource.clip = AudioClip;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeVolumn()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SfxVolumn");
    }
}
