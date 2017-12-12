﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Snake
{
    public class Head : MonoBehaviour
    {
        public float moveRate = 0.3f;       // Movement Interval
        public float sprintRate = 0.1f;     // Sprint Interval
        public float keyDownDuration = 0.25f;  // How long does a key have to be down before sprinting?
        public GameObject tailPrefab;       // Prefab of tail to spawn

        private float keyDownTimer = 0f;// How long has any key been pressed?
        private float moveTimer = 0f;   // Timer to keep track of elapsed time
        private float interval = 0f;    // Store the move rate / sprint rate
        private Vector2 direction = Vector2.right;  // Movement direction of snake (Right by default)
        private bool hasEaten = false;              // has the snake eaten?
        private List<Transform> tail = new List<Transform>();   // List to keep track of tails

        void CheckInput()
        {
            // Check for sprint
            if (Input.anyKey)
            {
                // Count how long a key is down for
                keyDownTimer += Time.deltaTime;
                if (keyDownTimer >= keyDownDuration)
                {
                    interval = sprintRate;
                }
            }
            else
            {
                // Reset the key down timer
                keyDownTimer = 0f;
                // Reset the move speed
                interval = moveRate;
            }

            // Check which direction we want the snake to go next frame
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !direction.Equals(Vector2.left))
                direction = Vector2.right;
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !direction.Equals(Vector2.up))
                direction = Vector2.down;
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !direction.Equals(Vector2.right))
                direction = Vector2.left;
            else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !direction.Equals(Vector2.down))
                direction = Vector2.up;
        }

        void AppendTail(Vector3 gapPos)
        {
            // Load prefab into the world
            GameObject clone = Instantiate(tailPrefab, gapPos, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, clone.transform);

            // Reset the flag (The snake is now NEVER satisfied)
            hasEaten = false;
        }

        void RefreshTail(Vector3 gapPos)
        {
            // Do we have a tail?
            if (tail.Count > 0)
            {
                // Move the last Tail element to where the Head's old position was
                tail.Last().position = gapPos;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }

        void Move()
        {
            // Save current position
            Vector2 gapPos = transform.position;

            // Move head into the new direction
            transform.Translate(direction);

            // Has the snake eaten something
            if (hasEaten)
            {
                // Append size of the tail
                AppendTail(gapPos);
            }
            else
            {
                // Refresh the tail location
                RefreshTail(gapPos);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // If collided with Food
            if (other.name.Contains("Food"))
            {
                // Get longer in next Move call
                hasEaten = true;
                // Remove the food
                Destroy(other.gameObject);
                // Tell GameManager to spawn things
                GameManager.Instance.Spawn();
            }
            else
            {
                // Game over
                GameManager.Instance.ResetGame();
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Checks for user input
            CheckInput();
            // Count up the timer
            moveTimer += Time.deltaTime;
            // Is it time to move?
            if (moveTimer > interval)
            {
                Move(); // Move the snake
                moveTimer = 0f; // Reset the timer
            }
        }
    }
}
