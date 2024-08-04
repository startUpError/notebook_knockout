using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public Item item;
    public int amount = 0; // Only use for ammo types

    void Start () {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) {
            if (item is Weapon weapon) {
                collider.gameObject.GetComponent<Player>().heldWeapon = weapon;
            } else if (item is AmmoItem ammo) {
                collider.gameObject.GetComponent<Player>().heldAmmo[(int)ammo.ammoType] += amount;
            }
            Destroy(gameObject);
        }
    }
}
