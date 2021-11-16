using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPIAccess : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("username", "Sparrow");
        form.AddField("score", 41);
        form.AddField("balloons_popped", 14);
        form.AddField("shot_accuracy", ".3");
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
    }
}
