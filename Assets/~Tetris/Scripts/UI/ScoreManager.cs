using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class ScoreManager : MonoBehaviour
    {
        public int value = 100;
        public int score;
        public Text scoreText;

        // Use this for initialization
        void Start()
        {
            Grid.onRowsCleared += OnRowsClear;
        }

        void OnRowsClear(int rowsCleared)
        {
            score += value * rowsCleared;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
