using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnBalloons : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject BalloonPreFab1;
    public GameObject BalloonPreFab2;
    public GameObject BalloonPreFab3;
    public GameObject BalloonPreFab5;
    public GameObject BalloonPreFab10;

    void Start() {
        for (int balloonNum = 0; balloonNum < 100; balloonNum++) {
            Instantiate(BalloonPreFab1, new Vector3(Random.Range(-3000,3000),Random.Range(47,1000),Random.Range(-3000,3000)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < 50; balloonNum++) {
            Instantiate(BalloonPreFab2, new Vector3(Random.Range(-3000,3000),Random.Range(47,1000),Random.Range(-3000,3000)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < 25; balloonNum++) {
            Instantiate(BalloonPreFab3, new Vector3(Random.Range(-3000,3000),Random.Range(47,1000),Random.Range(-3000,3000)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < 10; balloonNum++) {
            Instantiate(BalloonPreFab5, new Vector3(Random.Range(-3000,3000),Random.Range(47,1000),Random.Range(-3000,3000)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < 5; balloonNum++) {
            Instantiate(BalloonPreFab10, new Vector3(Random.Range(-3000,3000),Random.Range(47,1000),Random.Range(-3000,3000)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
