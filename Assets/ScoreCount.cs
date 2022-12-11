using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour {

    public int Score = 500;
    public float TimeToUpdate = 3f;
    private float tempScoreHolder = 0;
    private float tempTimeHolder = 0f;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(tempTimeHolder <= TimeToUpdate )
        {
            tempScoreHolder = Score * (tempTimeHolder / TimeToUpdate);
            tempTimeHolder += Time.deltaTime;
            textMesh.text = ((int)tempScoreHolder).ToString();
        }
    }
}
