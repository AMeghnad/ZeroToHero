using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsLastSibling : MonoBehaviour
{

    // Use this for initialization
    void OnDrawGizmos()
    {
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();
    }
}
