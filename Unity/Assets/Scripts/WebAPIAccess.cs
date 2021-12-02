using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebAPIAccess
{
	// Start is called before the first frame update

	public static IEnumerator Upload(string username, int score, int balloons_popped, float shot_accuracy, int boosts_used)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("score", score);
		form.AddField("balloons_popped", balloons_popped);
		form.AddField("shot_accuracy", shot_accuracy.ToString());
		form.AddField("boosts_used", boosts_used);

		using (UnityWebRequest www = UnityWebRequest.Post("https://aeroplay.herokuapp.com/api/insert", form))
		{
			yield return www.SendWebRequest();
			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Success! " + www.downloadHandler.text);
			}
		}
	}
}
