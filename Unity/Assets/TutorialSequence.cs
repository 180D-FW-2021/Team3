using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    private bool tutorialFlag;
    public int waitTime = 1;
    public GameObject TutorialObject;
    public GameObject HoldObject;
    public GameObject TiltObject;
    public GameObject BoostObject;
    public GameObject ShootObject;
    public GameObject BalloonsObject;
    public GameObject GoldObject;
    public GameObject ThrottleObject;
    public GameObject EndOfTutorialObject;

    void Start()
    {
        TutorialObject.SetActive(false);
        if (!Player.newUser)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Gameplay.gameStarted && !tutorialFlag )//&& !Gameplay.keyboardMode)
        {
            TutorialObject.SetActive(true);
            tutorialFlag = true;
            StartCoroutine(StartTutorialSequence());
        }
    }

    public IEnumerator StartTutorialSequence()
    {
        TurnOffObjects();
        HoldObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        HoldObject.SetActive(false);
        TiltObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        TiltObject.SetActive(false);
        BoostObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        BoostObject.SetActive(false);
        ShootObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ShootObject.SetActive(false);
        BalloonsObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        BalloonsObject.SetActive(false);
        GoldObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        GoldObject.SetActive(false);
        ThrottleObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ThrottleObject.SetActive(false);
        EndOfTutorialObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    public void TurnOffObjects()
    {
        HoldObject.SetActive(false);
        TiltObject.SetActive(false);
        BoostObject.SetActive(false);
        ShootObject.SetActive(false);
        BalloonsObject.SetActive(false);
        GoldObject.SetActive(false);
        ThrottleObject.SetActive(false);
        EndOfTutorialObject.SetActive(false);
    }
}


