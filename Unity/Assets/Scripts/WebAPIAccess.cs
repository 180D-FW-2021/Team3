using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPIAccess : MonoBehaviour {
    public GameObject ShooterObject;
    public Shooter ShooterInstance;
    public PlaneControl PlaneControlInstance;
    // Start is called before the first frame update
    void Start() {
        ShooterInstance = ShooterObject.GetComponent<Shooter>();
        PlaneControlInstance = GetComponent<PlaneControl>();
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", "Sparrow");
        form.AddField("score", ShooterInstance.score);
        form.AddField("balloons_popped", ShooterInstance.shotsHit);
        form.AddField("shot_accuracy", ShooterInstance.GetShotAccuracy().ToString());
        form.AddField("boosts_used", 3);

        using (UnityWebRequest www = UnityWebRequest.Post("https://aeroplay.herokuapp.com/api/insert", form)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Success! " + www.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("m")) {
            StartCoroutine(Upload());
            //Upload();
        }
        if (Input.GetKeyDown("n")) {
            Debug.Log(PlaneControlInstance.throttle);
        }
    }
}
