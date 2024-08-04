using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;

    Transform playerTF;
    Transform cameraTF;
    public bool cameraFollowsPlayer = false;


    public float movementSpeed = 15f;
    [Range(0,1)]
    public float drag = 0.05f;

    Vector2 movementVector = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerTF = transform;
        cameraTF = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraFollowsPlayer) {
            cameraTF.position = playerTF.position - new Vector3(0, 0, 10);

            if (playerRB.velocity != Vector2.zero) {
                cameraTF.position -= (Vector3)playerRB.velocity *  Mathf.Lerp(0, 1, 0.1f);
            }
        }
    }

    void FixedUpdate () 
    {
        movementVector = Vector2.zero;
        if (Input.GetKey("d")) {
            movementVector.x += movementSpeed;
        }
        if (Input.GetKey("a")) {
            movementVector.x -= movementSpeed;
        }
        if (Input.GetKey("w")) {
            movementVector.y += movementSpeed;
        }
        if (Input.GetKey("s")) {
            movementVector.y -= movementSpeed;
        }
        if (playerRB.velocity.magnitude < movementSpeed) {
            playerRB.AddForce(movementVector);
        }
        playerRB.velocity *= 1 - drag;
    }
}
