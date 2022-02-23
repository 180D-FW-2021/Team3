using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAchievements: MonoBehaviour
{
    public void Start() 
    {
        GetAchievements();
    }

    public void GetAchievements() {
        StartCoroutine(WebAPIAccess.GetAchievements(SetAchievements));
    }

    public void SetAchievements(string data) {
        Achievements.achievements = JsonHelper.FromJson<Achievement>(data);
    }
}