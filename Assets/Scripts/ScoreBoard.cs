using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int _score;
    DateTime _startTime;
    public int Score { 
        get {
            return _score + (int) (DateTime.Now - _startTime).TotalSeconds;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _startTime = DateTime.Now;
    }

    private void Update()
    {
        var text = GetComponent<Text>();
        text.text = Score.ToString();
    }

    public void AddScore(int scorePerHit)
    {
        _score += scorePerHit;
    }
}
