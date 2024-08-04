using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public int maxHealth;
    [Header("Attack")]
    public int baseDamage;
    public float baseAttackRange;

    [Header("Movement")]
    public float movementSpeed;
    [Range(0,1)]
    public float drag = 0.05f;

    [Header("Loot")]
    public Item heldItem;
    public int amount;

    [SerializeField]
    protected GameObject droppedItemPrefab;

    protected Player target;
    protected Rigidbody2D selfRB;

    public Weapon heldWeapon;

    protected void OnEnable ()
    {
        target = GameObject.Find("Player").GetComponent<Player>();
        selfRB = this.GetComponent<Rigidbody2D>();
        if (heldItem is Weapon weaponItem) {
            heldWeapon = weaponItem;
        }
    }

    protected void Update ()
    {
        if (target == null) {
            /* target = (Player)GameObject.CreatePrimitive(PrimitiveType.Plane).AddComponent(typeof(Player));
            Destroy(target.gameObject.GetComponent<MeshCollider>());
            target.transform.Rotate(0, 90, 0); */

            target = GameObject.Find("GameManager").GetComponent<Player>();
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    protected abstract void Target ();

    protected void Move ()
    {
        if (selfRB.velocity.magnitude < movementSpeed) {
            selfRB.AddForce(transform.up * movementSpeed);
        }
        selfRB.velocity *= 1 - drag;
    }
}
