using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody player;
    [SerializeField] float jumpStrength = 20f;
    [SerializeField] Vector3 gravity;

    private bool grounded = true;
    [SerializeField] bool hasDoubleJump = false;
    private int jumps = 0;
    public bool gameOver = false;
    private string currentSceneName;

    [SerializeField] Animator anim;

    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private ParticleSystem bloodParticle;
    private float startTimeEnd = 3f;
    private float startTime = 0f;


    private void Awake()
    {
        Physics.gravity = gravity;
        jumps = 0;
        currentSceneName = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (startTime < startTimeEnd)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            startTime += Time.deltaTime;
        }

        if (!gameOver && startTime > startTimeEnd)
        {
            if (Input.GetKeyDown("space") && grounded)
            {
                player.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

                if (!hasDoubleJump)
                {
                    grounded = false;
                }
                else
                {
                    jumps += 1;
                    if (jumps == 2)
                    {
                        grounded = false;
                        jumps = 0;
                    }
                }
            }
        }

        if (Input.GetKey("f"))
        {
            anim.SetFloat("Speed_f", 3f);
        }
        else
        {
            anim.SetFloat("Speed_f", 1f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        if (!gameOver)
        {
            dirtParticle.Play();
        }

        if (collision.transform.CompareTag("Obstacle"))
        {
            gameOver = true;
            GameOverOnce();
            Debug.Log("Game Over");
        }
    }

    private void GameOverOnce()
    {
        StartCoroutine(GameOver());
        anim.SetTrigger("Dead");
        bloodParticle.Play();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(currentSceneName);
    }
}
