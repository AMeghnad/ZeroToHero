using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public delegate void SpawnCallBack();
        public SpawnCallBack onSpawn;

        // Use this for initialization
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }       

        // Access this function to spawn whatever subscribed to spawn callback
        public void Spawn()
        {
            // If there are subscribed functions
            if (onSpawn != null)
            {
                // Invoke them
                onSpawn.Invoke();
            }
        }

        public void ResetGame()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
