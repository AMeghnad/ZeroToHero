using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(Head))]
    public class UserInput : MonoBehaviour
    {
        private Head player;

        // Use this for initialization
        void Start()
        {
            player = GetComponent<Head>();
        }

        void Update()
        {
            CheckSprint();
            CheckMove();
        }

        void CheckSprint()
        {
            if (Input.anyKey)
            {
                player.Sprint();
            }
            else
            {
                player.Walk();
            }
        }

        // Update is called once per frame
        void CheckMove()
        {
            // Get player's current direction
            Vector2 direction = player.direction;
            // Check which direction we want the snake to go next frame
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !direction.Equals(Vector2.left))
                direction = Vector2.right;
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !direction.Equals(Vector2.up))
                direction = Vector2.down;
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !direction.Equals(Vector2.right))
                direction = Vector2.left;
            else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !direction.Equals(Vector2.down))
                direction = Vector2.up;

            // Set player's new direction 
            player.direction = direction;
        }
    }
}
