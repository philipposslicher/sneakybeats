using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManage : MonoBehaviour {

    public TextMeshProUGUI scoreBoard;
    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public void AddScore (int amount) {

        score += amount;
        scoreBoard.text = "Score: " + score;

    } 
}
