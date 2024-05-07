using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    [SerializeField] private GameObject missile;

    public GameObject powerupIndicator;
    public float speed = 5.0f;
    private Vector3 abovePlayer;
    [SerializeField] private bool powerSlam = false;

    [SerializeField] GameObject slamCollider;

    private bool slamStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (powerSlam && Input.GetKey(KeyCode.Space) && !slamStarted)
        {
            StartCoroutine(SlamAttack());
        }
    }


    public bool hasPowerup = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerupBoost"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountrdownRoutine());
        }
        if (other.CompareTag("PowerupMissile"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SpawnMissile());
        }
        if (other.CompareTag("PowerupSlam"))
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerSlam());
        }
    }


    private IEnumerator SpawnMissile()
    {
        abovePlayer = transform.position + new Vector3(0, 5, 0);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(missile, abovePlayer, missile.transform.rotation);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator PowerupCountrdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    IEnumerator PowerSlam()
    {
        powerSlam = true;

        yield return new WaitForSeconds(5f);

        powerSlam = false;
    }

    IEnumerator SlamAttack()
    {
        slamStarted = true;

        Debug.Log("Fork");

        playerRb.AddForce(0, 100, 0, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);

        Instantiate(slamCollider, transform.position, transform.rotation);

        slamStarted = false;
    }

    public float powerupStrength = 15;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {

            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.transform.position - transform.position);
            Debug.Log("Collided with " + other.gameObject.name + " with powerup " + hasPowerup);

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}