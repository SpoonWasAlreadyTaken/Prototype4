using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float bound;
    private void FixedUpdate()
    {
        if (transform.position.x < bound)
        {
            Destroy(gameObject); 
        }
    }
}
