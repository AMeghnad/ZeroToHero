using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private float totalCookies = 0;
        [SerializeField]
        private float perSecond = 0;

        public static GameManager Instance = null;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        void Update()
        {
            totalCookies += perSecond * Time.deltaTime;
        }

        // Setters/Mutators
        public void AddCookies(int cookiesToAdd)
        {
            totalCookies += cookiesToAdd;
        }
        public void RemoveCookies(int cookiesToRemove)
        {
            totalCookies -= cookiesToRemove;
        }
        public void IncreasePerSecond(float perSecondToIncrease)
        {
            perSecond += perSecondToIncrease;
        }
        // Getters/Accessors
        public int GetTotalCookies()
        {
            return Mathf.RoundToInt(totalCookies);
        }
        public float GetPerSecond()
        {
            return perSecond;
        }
    }
}