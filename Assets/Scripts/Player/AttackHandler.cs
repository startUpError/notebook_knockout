using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{

    public Player player;

    public GameObject[] projectilePrefabs;
    public GameObject[] meleeWeaponPrefabs;

    public bool canAttack = true;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.up = mousePos - transform.position;
        if (Input.GetMouseButtonDown(0) && canAttack) {
            if (player.heldWeapon.isRanged) {
                if (player.heldAmmo[(int)player.heldWeapon.ammoType - 1] >= 1) {
                    //General
                    GameObject shotProjectileObj = Instantiate(projectilePrefabs[(int)player.heldWeapon.ammoType - 1], transform.position + (transform.up * 0.65f), transform.rotation);
                    AmmoProjectile projectile = shotProjectileObj.GetComponent<AmmoProjectile>();
                    projectile.isPlayerOwned = true;
                    projectile.damage = player.heldWeapon.attackDamage;
                    player.heldAmmo[(int)player.heldWeapon.ammoType - 1]--;

                    //Eraser things
                    projectile.knockbackForce = player.heldWeapon.knockbackForce;
                    projectile.splashRange = player.heldWeapon.sweepingEdgeRange;

                    Vector3 target = mousePos;
                    if (Mathf.Abs((target - transform.position).magnitude) > player.heldWeapon.attackRange) {
                        Vector3 angle = (target - transform.position).normalized;
                        target = transform.position + player.heldWeapon.attackRange * angle;
                    }
                    projectile.targetLocation = target;
                }
            } else {
                Debug.Log("Melee weapon");
                GameObject swingingWeaponObj = Instantiate(meleeWeaponPrefabs[0], transform); Debug.Log("Made weapon!");
                Debug.Log(swingingWeaponObj);
                HeldMeleeWeapon weaponScript = swingingWeaponObj.GetComponent<HeldMeleeWeapon>();
                weaponScript.weapon = player.heldWeapon; Debug.Log("Add weapon reference");
            }
            canAttack = false;
        }
        if (!canAttack) {
            timer += Time.deltaTime;
            if (timer >= player.heldWeapon.cooldownTime) {
                canAttack = true;
                timer = 0f;
            }
        }
    }
}
