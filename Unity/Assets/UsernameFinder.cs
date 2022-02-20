using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameFinder : MonoBehaviour
{
    void Start()
    {
        Text usernameComponent = gameObject.GetComponent<Text>();
        usernameComponent.text = Player.username;
    }
}
