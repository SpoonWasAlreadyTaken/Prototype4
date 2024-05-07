using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * 100, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
