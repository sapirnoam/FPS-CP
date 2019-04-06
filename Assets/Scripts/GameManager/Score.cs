using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score = 0;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Money:" + " " + score.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
            scoreText.text = "Money: " + score.ToString() + "$";
    }
}
