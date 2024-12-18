using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : PersistantSingleton<GameManager>
{
    public enum GameStates { Sandbox, Stationary}
    public GameStates State;

    [HideInInspector] public PlayerController Player;

    // Flappy Bird Score Variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public int flappyHighScore = 0;
    private int flappyCurrentScore;
    public int FlappyCurrentScore { 
        get { return flappyCurrentScore; } 
        set { flappyCurrentScore = value;

            if (flappyCurrentScore > flappyHighScore) {
                flappyHighScore = flappyCurrentScore;
            }

            scoreText.text = "Score: " + flappyCurrentScore;
            highScoreText.text = "Best: " + flappyHighScore;
        }
    }

    protected override void Awake() {
        base.Awake(); // Singleton Handling
    }
}
