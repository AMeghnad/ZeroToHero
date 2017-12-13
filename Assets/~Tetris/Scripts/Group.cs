using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Group : MonoBehaviour
    {
        public float holdDuration = 1f;
        public float fallInterval = 1f;
        public float fastInterval = .25f;

        private float holdTimer = 0f;
        private float fallTimer = 0f;
        private bool isFallingFaster = false;
        private bool isSpacePressed = false;
        private Spawner spawner;

        bool IsValidGridPos()
        {
            // Loop through all children in group
            foreach (Transform child in transform)
            {
                // Round the child's position
                Vector2 v = Grid.RoundVec2(child.position);
                // Not inside border?
                if (!Grid.InsideBorder(v))
                    return false;

                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;
                // If cell is NOT empty AND not part of same group
                if (Grid.grid[x, y] != null && Grid.grid[x, y].parent != transform)
                    return false;
            }
            // All other cases return true! Which means it's a valid position
            return true;
        }

        void UpdateGrid()
        {
            // Remove old children from grid
            for (int x = 0; x < Grid.width; x++)
            {
                for (int y = 0; y < Grid.height; y++)
                {
                    // If grid element exists at index AND it belongs to this current group
                    if (Grid.grid[x, y] != null && Grid.grid[x, y].parent == transform)
                    {
                        // Remove it from grid
                        Grid.grid[x, y] = null;
                    }
                }
            }

            // Add new children positions to grid
            foreach (Transform child in transform)
            {
                // Round the child's position
                Vector2 v = Grid.RoundVec2(child.position);
                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;
                // Set the coordinate to child
                Grid.grid[x, y] = child;
                child.position = v;
            }
        }

        // Use this for initialization
        void Start()
        {
            // Find the current spawner in the scene
            spawner = FindObjectOfType<Spawner>();
            // Check if null
            if (spawner == null)
            {
                // Display error
                Debug.LogError("Spawner does not exist in the current scene");
                Debug.Break();
            }
            // Check if game over
            if (!IsValidGridPos())
            {
                // Game is over!
                Grid.GameOver();
            }
        }

        void MoveLeftOrRight()
        {
            // Direction to move
            Vector3 moveDir = Vector3.zero;
            // Is going left?
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // Move Left
                moveDir = new Vector3(-1, 0, 0);
            }
            // Is going right?
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                moveDir = new Vector3(1, 0, 0);
            }

            // Is there a movement
            if (moveDir.magnitude > 0)
            {
                // Move the group in that direction
                transform.position += moveDir;
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid, update the grid
                    UpdateGrid();
                }
                else
                {
                    // It's NOT valid, revert
                    transform.position += -moveDir;
                }
            }
        }

        void MoveUpOrDown()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // Rotate around 90 degrees
                transform.Rotate(0, 0, -90);
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid Update grid
                    UpdateGrid();
                }
                else
                {
                    // It's NOT valid revert
                    transform.Rotate(0, 0, 90);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);
                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid, update grid
                    UpdateGrid();
                }
                else
                {
                    // It's not valid, revert
                    transform.position += new Vector3(0, 1, 0);
                }
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                holdTimer += Time.deltaTime;
                if (holdTimer >= holdDuration)
                {
                    isFallingFaster = true;
                }
                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
                {
                    isFallingFaster = false;
                    holdTimer = 0f;
                }
            }
        }

        void DetectFullRow()
        {
            // Clear any rows that are filled
            Grid.DeleteFullRows();
            // Spawn the next group
            spawner.SpawnNext();
            // Disable script
            enabled = false;
        }

        void Fall()
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);
            // See if valid
            if (IsValidGridPos())
            {
                // It's valid, update grid
                UpdateGrid();
            }
            else
            {
                // It's NOT valid, revert
                transform.position += new Vector3(0, 1, 0);
                // Detect full row
                DetectFullRow();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpacePressed = true;
            }

            if (!isSpacePressed)
            {
                MoveLeftOrRight();
                MoveUpOrDown();

                fallTimer += Time.deltaTime;
                // Ternary operator
                float currentInterval = isFallingFaster ? fastInterval : fallInterval;
                if (fallTimer >= fallInterval)
                {
                    print(currentInterval);
                    Fall();
                    fallTimer = 0f;
                }
            }
            else
            {
                Fall();
            }
        }
    }
}
