using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class WolfKing : MonoBehaviour{
    public WolfKingOnHitResponse onHitResponse;

    [SerializeField]
    private Node behaviourTreeRoot;
    [SerializeField]
    private WolfKingStateTable wolfKingStateTable;    [SerializeField]    private GameObject wolfBody;    [SerializeField]    private HPVisualIndicator hpVisualIndicator;
    private bool activated = false;

    // Start is called before the first frame update
    void  Start(){
        InitStateTable();
		InitWolfKingBeaviourTree();
        AttachDamagableParts();     
    }

    private void Update()
    {
        hpVisualIndicator.SyncHPRate(wolfKingStateTable.currentHP / wolfKingStateTable.attribute.maxHP);
    }

    public void ApplyStunForce(float force) {
        wolfKingStateTable.stunProcess += force * wolfKingStateTable.attribute.stunForceRate;
    }

    /// <summary>
    /// cannot envoke in the first frame(when BT is init)
    /// </summary>
    public void ActiveToFight() {
        if (!activated) {
            activated = true;
            behaviourTreeRoot.ExecuteNode();
            wolfBody.SetActive(true);
        }
    }

    public void EndShowTime() {
        Debug.Log("show time end!");
        float remainHP = 100;
        if (wolfKingStateTable.currentHP > remainHP)
            wolfKingStateTable.currentHP = remainHP;
    }

	void InitWolfKingBeaviourTree() {
        foreach (var node in GetComponentsInChildren<WolfKingLeafNode>())
            node.Init(wolfKingStateTable);
        foreach (var node in GetComponentsInChildren<WolfKingConditionNode>())
            node.Init(wolfKingStateTable);
    }

    void AttachDamagableParts() {
        foreach (var p in GetComponentsInChildren<WolfKingDamageblePart>())
            p.AttachTo(wolfKingStateTable);
    }

    void InitStateTable() {
        wolfKingStateTable.currentHP = wolfKingStateTable.attribute.maxHP;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 0.7f, 0.3f);
        Gizmos.DrawSphere(transform.position, wolfKingStateTable.attribute.facingDistance);
    }
}
