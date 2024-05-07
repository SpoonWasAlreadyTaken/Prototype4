using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    private Rigidbody enemyRb;
    private GameObject player;

    [SerializeField] private float bossGrowTime = 3f;

    public float speed = 1.0f;

    private float size = 3;

    private float timer = 0;

    private bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;

        if (timer >= bossGrowTime && !grown) 
        {
            StartCoroutine(BossGrow());
        }
    }


    IEnumerator BossGrow()
    {
        grown = true;

        for (int i = 0; i < 15; i++)
        {
            enemyRb.mass += 5f;
            speed += 5f;
            size += 0.2f;
            transform.localScale = new Vector3(size, size, size);
            yield return new WaitForSeconds(0.05f);
        }


        yield return new WaitForSeconds(5f);


        for (int i = 0; i < 15; i++)
        {
            enemyRb.mass -= 5f;
            speed -= 5f;
            size -= 0.2f;
            transform.localScale = new Vector3(size, size, size);
            yield return new WaitForSeconds(0.05f);
        }


        timer = 0;
        grown = false;
    }
}