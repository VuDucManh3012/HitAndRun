using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranformAttackBoss : MonoBehaviour
{
    public static TranformAttackBoss Instance { get; private set; }
    public List<Vector3> ListTarget;
    public Vector3 root, TargetBefore, TargetCurrent;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        root = transform.position;
        TargetBefore = new Vector3(-9f, root.y + 5, root.z + 8);
        ListTarget.Add(TargetBefore);
        SetTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, TargetCurrent) >= 1f)
        {
            MoveToTarget();
        }
        else
        {
            SetTarget();
        }
    }
    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetCurrent, 0.5f);
    }
    private void SetTarget()
    {
        if (ListTarget.Count > 0)
        {
            TargetCurrent = ListTarget[0];
            ListTarget.RemoveAt(0);
        }
    }
    public void AddPosition()
    {
        Vector3 newTarget = new Vector3(-TargetBefore.x, root.y + Random.Range(0, 15), TargetBefore.z);
        ListTarget.Add(newTarget);
        TargetBefore = newTarget;
    }
    public void OffCompenent()
    {
        ListTarget.Clear();
        transform.position = root;
        this.enabled = false;
    }
}
