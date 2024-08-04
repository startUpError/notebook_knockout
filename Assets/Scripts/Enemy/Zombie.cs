using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    void FixedUpdate()
    {
        Target();
        Move();
    }

    protected override void Target ()
    {
        transform.up = target.transform.position - transform.position;
    }

    void OnDestroy() {
        if (heldItem != null && Random.Range(0.01f, 1) > 0) {
            GameObject item = Instantiate(droppedItemPrefab, transform.position, new Quaternion());
            item.GetComponent<DroppedItem>().item = heldItem;
            item.GetComponent<DroppedItem>().amount = amount;
        }
    }
}
