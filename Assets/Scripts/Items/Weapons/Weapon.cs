using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 1)]
public class Weapon : Item
{
    [Header("Attack")]
    public int attackDamage;
    public float attackRange;
    public float sweepingEdgeRange;
    public float sweepingSpeed;
    public float knockbackForce;
    public float cooldownTime;

    [Header("Typing")]
    public bool isRanged;

    public AmmoEnum ammoType;
}

//Soon to be moved to JSON files