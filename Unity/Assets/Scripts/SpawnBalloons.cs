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

    public int spawnRange;
    public int xOffset;
    public int zOffset;
    public int yMin;
    public int yMax;

    public int num1Point;
    public int num2Point;
    public int num3Point;
    public int num5Point;
    public int num10Point;

    public static Vector3 wind;

    void Start() {
        wind = new Vector3(Random.Range(-.3f, .3f), Random.Range(-.1f, .1f), Random.Range(-.3f, .3f));

        for (int balloonNum = 0; balloonNum < num1Point; balloonNum++) {
            Instantiate(BalloonPreFab1, new Vector3(Random.Range(-spawnRange + xOffset,spawnRange + xOffset),Random.Range(yMin,yMax),Random.Range(-spawnRange + zOffset, spawnRange + zOffset)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < num2Point; balloonNum++) {
            Instantiate(BalloonPreFab2, new Vector3(Random.Range(-spawnRange + xOffset,spawnRange + xOffset),Random.Range(yMin,yMax),Random.Range(-spawnRange + zOffset, spawnRange + zOffset)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < num3Point; balloonNum++) {
            Instantiate(BalloonPreFab3, new Vector3(Random.Range(-spawnRange + xOffset,spawnRange + xOffset),Random.Range(yMin,yMax),Random.Range(-spawnRange + zOffset, spawnRange + zOffset)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < num5Point; balloonNum++) {
            Instantiate(BalloonPreFab5, new Vector3(Random.Range(-spawnRange + xOffset,spawnRange + xOffset),Random.Range(yMin,yMax),Random.Range(-spawnRange + zOffset, spawnRange + zOffset)), Quaternion.identity);
        }
        for (int balloonNum = 0; balloonNum < num10Point; balloonNum++) {
            Instantiate(BalloonPreFab10, new Vector3(Random.Range(-spawnRange + xOffset,spawnRange + xOffset),Random.Range(yMin,yMax),Random.Range(-spawnRange + zOffset, spawnRange + zOffset)), Quaternion.identity);
        }
    }
}
