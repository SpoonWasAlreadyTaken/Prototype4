using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;

    [SerializeField] private TextMeshProUGUI scoreTXT;

    [SerializeField] private Jump over;

    private void Awake()
    {
        scoreTXT.text = "Score = 0";
        score = 0;
    }

    void FixedUpdate()
    {
        if (!over.gameOver)
        {
            if (Input.GetKey("f"))
            {
                score += 2;
            }
            else
            {
                score += 1;
            }

            scoreTXT.text = "Score = " + score.ToString();
        }
    }
}
