﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] groups;
        public int nextIndex = 0;

        // Spawns the next random group element
        public void SpawnNext()
        {
            // Get next random index (i)
            int i = Random.Range(0, groups.Length);
            // Spawn the random group
            Instantiate(groups[i], transform.position, Quaternion.identity);
            // Get next random index
            nextIndex = Random.Range(0, groups.Length);

            // Remove any empty parents
            RemoveEmptyParents();
        }

        void RemoveEmptyParents()
        {
            // Check for any parents without children
            Group[] groups = GameObject.FindObjectsOfType<Group>();
            foreach (Group g in groups)
            {
                // If the group doesn't have any children
                if (g.transform.childCount == 0)
                {
                    // Destroy the parent
                    Destroy(g.gameObject);
                }
            }
        }
        // Use this for initialization
        void Start()
        {
            SpawnNext();
        }
    }
}
