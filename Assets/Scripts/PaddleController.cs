using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    private float paddleWidth; 
    private Vector3 screenDimensions;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        paddleWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2; 
        screenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement;
        movement.y = rigidBody.velocity.y;

        if (Input.GetKey(KeyCode.RightArrow)) {
            if (rigidBody.position.x + paddleWidth > screenDimensions.x - .15) {
                movement.x = 0;
            }
            else {
                movement.x = speed; 
            }
        } 
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (rigidBody.position.x - paddleWidth < -screenDimensions.x - .2) {
                movement.x = 0;
            }
            else {
                movement.x = -speed;
            }
        }
        else {
            movement.x = 0;
        }

        rigidBody.velocity = movement;
    }
}
