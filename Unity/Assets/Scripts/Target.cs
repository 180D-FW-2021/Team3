using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public float index = 0f;
    public float health = 1f;
    public float deltaHeight = .5f;
    public int isBobbing;
    public int isWindAffected;
    public MeshRenderer rend;

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }

    void Die() 
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.Play();
        }
        if (rend) //heart edge case
        {
            rend.enabled = false;
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            meshCollider.enabled = false;
        }
        else
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
        }
        Destroy(gameObject, audio.clip.length);
    }

    public void Start()
    {
        index = UnityEngine.Random.Range(0f, 6.28f);
        rend = GetComponent<MeshRenderer>();
    }

    public float GetWindModifier()
    {
        switch(this.name)
        {
            case "Balloon1(Clone)":
                return 0f;
            case "Balloon2(Clone)":
                return .1f;
            case "Balloon3(Clone)":
                return .3f;
            case "Balloon5(Clone)":
                return .5f;
            case "Balloon10(Clone)":
                return 1f;
            default:
                return 0f;
        }
    }

    public void Update() 
    {
        index += Time.deltaTime;
        float y = deltaHeight * Mathf.Sin(index) * isBobbing;
        transform.localPosition = this.transform.localPosition + new Vector3(0,y,0) + SpawnBalloons.wind * GetWindModifier() * isWindAffected;
    }
}
