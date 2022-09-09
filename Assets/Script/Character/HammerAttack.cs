using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HyperCatSdk;
using MoreMountains.NiceVibrations;
public class HammerAttack : MonoBehaviour
{
    public ControllerPlayer controllerPlayer;
    void OnTriggerEnter(Collider other)
    {
        void DestroyEnemy()
        {
            Destroy(other.transform.Find("EnemyModel").gameObject);
            Destroy(other.transform.Find("TextLevel").gameObject);
            Destroy(other.transform.Find("LowerLevel").gameObject);
            Destroy(other.transform.Find("UpperLevel").gameObject);
            other.transform.Find("EnemyRagdoll").gameObject.SetActive(true);
            other.transform.GetComponent<BoxCollider>().enabled = false;
            controllerPlayer.myLevel += other.GetComponent<Enemy>().levelBonus;
            controllerPlayer.FloatingTextAnimator.Play("FloatingText");
            controllerPlayer.FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
            if (other.GetComponent<Enemy>().levelBonus >= 10)
            {
                controllerPlayer.FloatingTextUp.SetActive(false);
                controllerPlayer.FloatingTextUp.SetActive(true);
                controllerPlayer.OnParticle(4);
                controllerPlayer.OnParticle(6);
            }
            HCVibrate.Haptic(HapticTypes.SoftImpact);
            controllerPlayer.SetSkin();
            controllerPlayer.CheckLevelEnemy();
            controllerPlayer.AddPitchSoundAttack();
        }
        if (other.CompareTag("Enemy"))
        {
            DestroyEnemy();
        }
        else if (other.CompareTag("Enemy3"))
        {
            DestroyEnemy();
        }
    }
}
