using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebAPIAccess
{
	// Start is called before the first frame update

	public static IEnumerator Upload(string username, int score, int balloons_popped, float shot_accuracy, int boosts_used, string game_map, string control)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("score", score);
		form.AddField("balloons_popped", balloons_popped);
		form.AddField("shot_accuracy", shot_accuracy.ToString());
		form.AddField("boosts_used", boosts_used);
		form.AddField("game_map", game_map);
		form.AddField("control", control);

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

	public static IEnumerator GetPlayerData(string username, Action<string> callback = null)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		using (UnityWebRequest www = UnityWebRequest.Post("https://aeroplay.herokuapp.com/api/player/data", form))
		{
			yield return www.SendWebRequest();
			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			else {
				if (callback != null)
				{
					callback(www.downloadHandler.text);
				}
			}
		}
	}

	public static IEnumerator InsertPlayerData(string username, string hash, string salt, Action<InsertStatus> callback = null)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("hash", hash);
		form.AddField("salt", salt);

		using (UnityWebRequest www = UnityWebRequest.Post("https://aeroplay.herokuapp.com/api/player/insert", form))
		{
			yield return www.SendWebRequest();
			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			else{
				if (callback != null)
				{
					InsertStatus insertStatus = new InsertStatus();
					insertStatus.dbResponse = www.downloadHandler.text;
					insertStatus.username = username;
					callback(insertStatus);
				}
			}
		}
	}
}
