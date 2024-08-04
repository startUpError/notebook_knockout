using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int health = 6;
    public int maxHealth = 6;
    public Weapon heldWeapon;
    public int[] heldAmmo = new int[Enum.GetNames(typeof(AmmoEnum)).Length - 1];
    public int metalBits = 0;
    public int notes = 0;

    public float knockbackForce = 400;

    Rigidbody2D playerRB;

    void Start ()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (health <= 0) {
            Debug.Log("You died!");
            Destroy(gameObject);
        } else if (health > maxHealth) {
            health = maxHealth;
        }
    }

    //TODO: Move this to each enemies attacking function
    void OnCollisionEnter2D (Collision2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            health -= enemy.baseDamage + (enemy.heldWeapon != null ? enemy.heldWeapon.attackDamage : 0);
            playerRB.AddForce(knockbackForce * collider.GetContact(0).normal);
        }
    }
}
