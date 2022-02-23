using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements
{
    public static Achievement[] achievements;

    public static void SetLocalAchievements(DBFormat achievementData)
    {
        int[] achievementArray = achievementData.GetAchievementArray();
        for (int idNum = 0; idNum < achievements.Length; idNum++) 
        {
            //handle different achievements in different ways
            if ((idNum >= 0 && idNum < 24) || (idNum > 27 && idNum <= 38))
            {
                achievements[idNum].gotten = Convert01ToBool(achievementArray[idNum]);
            }
            if (idNum >= 24 && idNum <= 27) //the "play X amount of games" acheivements, store the amount of games played so far in the value field since database returns number of games rather than 0 or 1
            {
                achievements[idNum].value = achievementArray[idNum];
                achievements[idNum].gotten = CheckGamesPlayedAchievement(idNum, achievementArray[idNum]);
            }
        }
    }
    
    public static bool Convert01ToBool(int value)
    {
        return (value == 1);
    }

    public static bool CheckGamesPlayedAchievement(int idNum, int gamesPlayed)
    {
        switch (idNum)
        {
            case 24:
                if (gamesPlayed >= 1)
                {
                    return true;
                }
                break;
            case 25:
                if (gamesPlayed >= 3)
                {
                    return true;
                }
                break;
            case 26:
                if (gamesPlayed >= 5)
                {
                    return true;
                }
                break;
            case 27:
                if (gamesPlayed >= 10)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public static bool CheckIfGotten(int id)
    {
        return achievements[id].gotten;
    }

    public static int CheckForValue(int id)
    {
        return achievements[id].value;
    }

    public static void GetAchievement(int id)
    {
        achievements[id].gotten = true;
    }

    public static void IncrementValue(int id)
    {
        achievements[id].value = achievements[id].value + 1;
    }
}

[System.Serializable]
public class Achievement
{
    public int id;
    public string name;
    public string description;
    public bool gotten;
    public int value; //can be used for whatever
    public Achievement(int id, string inputName, string inputDescription)
    {
        name = inputName;
        description = inputDescription;
        gotten = false;
        value = -1;
    }
}

// https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[System.Serializable]
public class DBFormat
{
    public string username;
    public int id0;
    public int id1;
    public int id2;
    public int id3;
    public int id4;
    public int id5;
    public int id6;
    public int id7;
    public int id8;
    public int id9;
    public int id10;
    public int id11;
    public int id12;
    public int id13;
    public int id14;
    public int id15;
    public int id16;
    public int id17;
    public int id18;
    public int id19;
    public int id20;
    public int id21;
    public int id22;
    public int id23;
    public int id24;
    public int id25;
    public int id26;
    public int id27;
    public int id28;
    public int id29;
    public int id30;
    public int id31;
    public int id32;
    public int id33;
    public int id34;
    public int id35;
    public int id36;
    public int id37;
    public int id38;

    public int[] GetAchievementArray()
    {
        int[] achievementArray = new int[] {id0, id1, id2, id3, id4, id5, id6, id7, id8, id9, id10, id11, id12, id13, id14, id15, id16, id17, id18, id19, id20, id21, id22, id23, id24, id25, id26, id27, id28, id29, id30, id31, id32, id33, id34, id35, id36, id37, id38};
        return achievementArray;
    }
}