using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private GameObject[] enemy;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 15f;
    private int chooseEnemy;

    void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        chooseEnemy = Random.Range(0, enemy.Length);

        transform.LookAt(enemy[chooseEnemy].transform);
        StartCoroutine(SelfDestruct());

        if (enemy == null)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (chooseEnemy <= enemy.Length && enemy[chooseEnemy] != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy[chooseEnemy].transform.position, speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * 100, ForceMode.Impulse);

            Destroy(gameObject);
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}
