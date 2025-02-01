using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    
    private int _score;
    public TextMeshProUGUI scoreText;

    public void AddScore(int coinValue) {
        _score += coinValue;
    }
    
    private void Start() {
        _score = 0;
    }

    private void Update() {
        scoreText.text = "Score: " + _score;
    }
}
