using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int score = 0;
    int lives = 3; 
    public GameObject scoreLabel;
    public GameObject livesLabel;
    public GameObject endGameLabel;
    public GameObject replayButton;
    public GameObject canvas;
    public UnityEvent deactivateBallEvent; 
    public UnityEvent speedEvent; 
    GameObject newCanvas;
    
    public void Start(){
        scoreLabel.GetComponent<TMP_Text>().text = "Score: " + score.ToString();
        livesLabel.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }

    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Thank you Colin Kirby for helping me with this!
    }

    public void LoseLife(){
        lives--;
        if (lives == 0) {
            EndGame("Game Over");
        }
        livesLabel.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }

    public void AddScore(){
        score++;
        scoreLabel.GetComponent<TMP_Text>().text = "Score: " + score.ToString();
        if (score == 120) {
            EndGame("Victory!");
            deactivateBallEvent.Invoke();
        }
        else if (score % 20 == 0) {
            speedEvent.Invoke();
        }
    }

    public void EndGame(string message){
        endGameLabel.GetComponent<TMP_Text>().text = message;
        CreateReplayButton();
    }

    public void CreateReplayButton(){
        newCanvas = Instantiate(canvas) as GameObject;
        GameObject newReplayButton = Instantiate(replayButton);
        newReplayButton.GetComponent<Button>().onClick.AddListener(Reset);
        newReplayButton.transform.SetParent(newCanvas.transform, false);
    }
}
