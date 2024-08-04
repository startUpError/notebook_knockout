using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldMeleeWeapon : MonoBehaviour
{

    public Weapon weapon;
    float delayTimer = 0f;
    bool hasStarted = false;

    void OnEnable ()
    {
        //Don't use this code because it runs before the weapon variable can be assigned

        /* if (weapon == null) {
            Debug.Log("No weapon referenced.");
            return;
        }
        transform.localScale = new Vector3(0.2f, weapon.attackRange, 1);
        transform.Rotate(transform.position - Vector3.up * (transform.localScale.y / 2), -weapon.sweepingEdgeRange / 2);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0 && !hasStarted) {
            if (weapon == null) {
                Debug.Log("No weapon referenced.");
                return;
            }
            Debug.Log("Found weapon");
            transform.localScale = new Vector3(1, weapon.attackRange, 1);
            transform.Rotate(Vector3.forward * -weapon.sweepingEdgeRange);
            transform.Translate(0, transform.localScale.y, 0);
            hasStarted = true;
        }
        delayTimer++;
        transform.Translate(0, -transform.localScale.y, 0);
        transform.Rotate(Vector3.forward * weapon.sweepingSpeed);
        transform.Translate(0, transform.localScale.y, 0);
        Debug.Log(transform.localRotation.eulerAngles.z);
        if (transform.localRotation.eulerAngles.z >= weapon.sweepingEdgeRange && transform.localRotation.eulerAngles.z <= 180) {
            Debug.Log("Too far!");
            Destroy(gameObject);
        }
        //TODO: Fix exploit where if the player just spins really fast they can get sweeping edge the whole range around their character
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject);
        } else if (/*isPlayerOwned && */!collider.gameObject.CompareTag("Player")) {
            collider.gameObject.GetComponent<Enemy>().health -= weapon.attackDamage;
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(weapon.knockbackForce * (collider.transform.position - transform.position).normalized);
        }
    }
}
