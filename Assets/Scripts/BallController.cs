using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    float lives = 3;
    Rigidbody2D rigidBody;
    public UnityEvent resetEvent;
    public UnityEvent scoreEvent;
    public GameObject livesLabel;
    float speed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Invoke("LaunchBall", 3);
    }

    void LaunchBall() {
        float rand = Random.Range(-.5f, .5f); 
        rigidBody.velocity = new Vector2(rand, -1) * speed; 
    }

    public void ResetBall(){
        gameObject.SetActive(true);
        rigidBody.velocity = Vector2.zero; 
        transform.position = new Vector2(0, -1.39f);
        Invoke("LaunchBall", 3);
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if (coll.collider.CompareTag("Brick")) {

            Destroy(coll.collider.gameObject);
            scoreEvent.Invoke();
        }
        else if (coll.collider.CompareTag("Player")) {
            float x = hitFactor(transform.position, coll.transform.position, coll.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            rigidBody.velocity = dir * speed;
        }
        else if (coll.collider.CompareTag("BottomWall")) {
            lives--;
            resetEvent.Invoke();
            if (lives == 0) {
                ToggleBall();
            }
            else {
                ResetBall();
            }
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    public void ToggleBall() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void IncreaseSpeed(){
        speed += 0.5f; 
    }
}
