using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    public class CookieAnim : MonoBehaviour
    {
        public Animator anim;

        private Cookie cookie;

        // Use this for initialization
        void Start()
        {
            cookie = GetComponent<Cookie>();
            cookie.onClick += OnClick;
        }

        void OnClick(Vector3 point)
        {
            anim.SetTrigger("Click");
        }

        void OnMouseEnter()
        {
            anim.SetTrigger("Enter");
        }

        void OnMouseExit()
        {
            anim.SetTrigger("Exit");
        }
    }
}