using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    //The game object's Transform  
    private Transform goTransform;  

    //the throttle increment to the current velocity  
    private float increment=0.0f;  
    //this variable stores the vertical axis values  
    private float vertAxis=0.0f;  
    //the throttle  
    public float throttle =1f;  
    

    // Start is called before the first frame update
    void Start()
    {
        //get this game object's Transform  
        goTransform = this.GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        vertAxis = Input.GetAxis("Vertical") * Time.deltaTime;
        if(vertAxis > 0) {
            increment = 0.1f;
        } else {
            increment = -0.1f;
        }

        //after releasing the vertical axis, add the increment the throttle  
        if(Input.GetButtonUp("Vertical"))  
        {  
            throttle = throttle+increment;  
        }  

        //set the throttle limit between -0.05f (reverse) and 50f (max speed)  
        throttle=Mathf.Clamp(throttle, 0f, 2f);  

        //translates the game object based on the throttle  
        goTransform.Translate(throttle * Vector3.forward);  
  
        //rotates the game object, based on horizontal input  
        goTransform.Rotate(Vector3.up * Input.GetAxis("Horizontal")); 
        goTransform.Rotate(Vector3.right * Input.GetAxis("Yaw"));
    }
}
