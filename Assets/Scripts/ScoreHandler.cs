using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    //[SerializeField] private float scoreMultiplier;

    public const string Record = "Record";
    private float score;
    
    void Update()
    {
        score += Time.deltaTime; // * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy() {
        int currentHighScore = PlayerPrefs.GetInt(Record, 0);
    
        if(score > currentHighScore){
            PlayerPrefs.SetInt(Record, Mathf.FloorToInt(score));
        }
    
    }
}
