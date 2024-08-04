using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoProjectile : MonoBehaviour
{

    public AmmoEnum type;

    Rigidbody2D thisRB;

    public float movementSpeed = 10f;

    public int damage;
    public float knockbackForce;
    public float splashRange;

    public bool isPlayerOwned;

    bool isMoving = true;

    float despawnTimer = 0f;
    public float despawnTime = 15f;

    //Eraser Type
    public Vector3 targetLocation;
    public Vector3 midPoint;
    public float peakScale = 0.35f;
    public float eraserTimer = 0f;
    public Vector3 startingScale;

    public float timeToTarget;

    void Start ()
    {
        thisRB = gameObject.GetComponent<Rigidbody2D>();

        if (type == AmmoEnum.Eraser) {
            timeToTarget = Vector3.Distance(transform.position, targetLocation) / movementSpeed;
            startingScale = transform.localScale;
        }
    }

    void FixedUpdate ()
    {
        if (isMoving) {
            switch (type) {
                case AmmoEnum.Pencil:
                    /* if (thisRB.velocity.magnitude < movementSpeed) {
                        thisRB.AddForce(movementSpeed * transform.up);
                    } */
                    thisRB.velocity = transform.up * movementSpeed;
                    break;
                case AmmoEnum.Eraser:
                    eraserTimer += Time.fixedDeltaTime;
                    thisRB.velocity = (targetLocation - transform.position).normalized * movementSpeed;
                    if (/*eraserTimer <= timeToTarget / 2*/ eraserTimer < timeToTarget) {
                        //transform.localScale = Vector3.Lerp(Vector3.one, peakScale, eraserTimer);
                        transform.localScale = PEERP(eraserTimer, timeToTarget, peakScale);
                    }/* else if (eraserTimer < timeToTarget) {
                        transform.localScale = Vector3.Lerp(peakScale, Vector3.one, eraserTimer * 2);
                    }*/ else {
                        isMoving = false;
                        EraserLanded();
                    }
                    break;
            }
        } else {
            thisRB.velocity = Vector3.zero;
            despawnTimer += Time.fixedDeltaTime;
            if (despawnTimer >= despawnTime) {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isMoving) {
            switch (type) {
                case AmmoEnum.Pencil:
                    if (collider.gameObject.CompareTag("Obstacle")) {
                        isMoving = false;
                    } else if (isPlayerOwned && !collider.gameObject.CompareTag("Player")) {
                        collider.gameObject.GetComponent<Enemy>().health -= damage;
                        collider.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce * (collider.transform.position - transform.position).normalized);
                        Destroy(gameObject);
                    }
                    break;
                case AmmoEnum.Eraser:
                    break;
            }
        }
    }

    Vector3 PEERP (float currentTime, float totalTime, float peakScale) {
        float scalingFactor = (currentTime - (totalTime / 2)) * (currentTime - (totalTime / 2)) * ((1 - peakScale) / ((totalTime / 2) * (totalTime / 2))) + peakScale;
        return startingScale * scalingFactor;
    }

    void EraserLanded () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().startSize = splashRange;
        gameObject.GetComponent<CircleCollider2D>().radius = splashRange;
        List<Collider2D> enemiesWithinRange = new List<Collider2D>();
        thisRB.OverlapCollider(new ContactFilter2D(), enemiesWithinRange);
        foreach (Collider2D collider in enemiesWithinRange) {
            try {
                collider.gameObject.GetComponent<Enemy>().health -= damage;
            } catch {
                Debug.Log("Not enemy hit.");
            }
        }
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
