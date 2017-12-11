using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Column : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.StartsWith("Bird"))
            {
                GameManager.Instance.BirdScored();
            }
        }
    }
}