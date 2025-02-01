using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lara {
    public class ScoreManager : MonoBehaviour {
        int             score;
        public TextMesh scoreText;
        string          textToDisplay;

        public void AddScore(int coinValue) {
            score += coinValue;
        }

// Start is called before the first frame update
        void Start() {
            score = 0;
        }

        // Update is called once per frame
        void Update() {
            textToDisplay = "Score: " + score.ToString();
            scoreText.text = textToDisplay;
        }
    }
}