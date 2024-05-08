using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    private Jump over;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        over = player.GetComponent<Jump>();
    }

    void FixedUpdate()
    {
        if (!over.gameOver)
        {
            if (Input.GetKey("f"))
            {
                transform.Translate(Vector3.left * speed * 1.5f);

            }
            else
            {
                transform.Translate(Vector3.left * speed);
            }
        }
    }
}
