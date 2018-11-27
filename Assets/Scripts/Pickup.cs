using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    
    public int value;

    private GameManage gameManage;

	// Use this for initialization
	void Start () {

        gameManage = FindObjectOfType<GameManage> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D (Collider2D other) {

        if (other.CompareTag("Player")) {

            gameManage.AddScore(value);
            Destroy(gameObject);

        }

    }
}
