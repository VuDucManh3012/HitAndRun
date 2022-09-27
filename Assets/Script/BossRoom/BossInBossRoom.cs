using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInBossRoom : MonoBehaviour
{
    private Animator myAnim;
    public static BossInBossRoom Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnBossFromindexBoss();
    }
    void SpawnBossFromindexBoss()
    {
        int indexBoss = BossRoom.Instance.indexBoss;
        this.transform.Find("EnemyModel").GetChild(indexBoss).gameObject.SetActive(true);
        this.transform.Find("EnemyRagdoll").GetChild(indexBoss).gameObject.SetActive(true);
        myAnim = transform.Find("EnemyModel").GetChild(indexBoss).GetComponent<Animator>();
    }
    public void SetTriggerAnimIsAttacked()
    {
        myAnim.SetTrigger("IsAttacked");
    }
    public void OnRagdoll()
    {
        transform.Find("EnemyModel").gameObject.SetActive(false);
        transform.Find("EnemyRagdoll").gameObject.SetActive(true);
    }
}
