using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Bird : MonoBehaviour
    {
        public float upForce;

        private Rigidbody2D rigid;      // Upward force of the "Flap"
        private bool isDead = false;    // Has the player collider

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Flap()
        {
            // Only flap if the Bird isn't dead yet
            if (!isDead)
            {
                rigid.velocity = Vector2.zero;
                // Give the bird some upward force
                rigid.velocity = Vector2.zero;
                // Give the bird some upward force
                rigid.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Cancel the velocity
            rigid.velocity = Vector2.zero;
            // Bird is now dead
            isDead = true;
            // Break the news to the GameManager
            GameManager.Instance.BirdDied();
        }
    }
}