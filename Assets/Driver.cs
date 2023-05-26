using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float normalSpeed = 10f;
    [SerializeField] float steeringSpeed = 200f;
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float slowSpeed = 2f;
    [SerializeField] float boostSpeed = 20f;

    [SerializeField] float slowTime = 3f;

    bool speedUpEntered = false;

    float steerAmount;
    float moveAmount;

    Coroutine activeCoroutine;
    
    /* void setNormalSpeed ()
    {
        moveSpeed = normalSpeed;
    } */

    void Start() 
    {
        StartCoroutine(startCountdown(3));
        //below does the same job
        //StartCoroutine("Countdown",3);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        speedUpEntered = false;
        moveSpeed = slowSpeed;
        //Invoke starts after specified time
        //Invoke("setNormalSpeed",slowTime);


        //You can pass arguments to a coroutine
        if (activeCoroutine!=null)
        {
            StopCoroutine(activeCoroutine);
        }
        activeCoroutine = StartCoroutine(ResetMoveSpeed(slowTime));
    }

    IEnumerator startCountdown (int seconds)
    {
        while (seconds > 0)
        {
            Debug.Log(seconds.ToString());
            seconds--;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Go!");
        moveSpeed = normalSpeed;
    }    

    IEnumerator ResetMoveSpeed(float slowTime)
    {
        //yield return new WaitForSeconds(slowTime);
        //if(!speedUpEntered)
        //{moveSpeed = normalSpeed;}
        float elapsedTime=0f;
        while (elapsedTime<slowTime && !speedUpEntered)
        {
            moveSpeed = Mathf.Lerp(slowSpeed,normalSpeed,elapsedTime/slowTime);
            Debug.Log(moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(!speedUpEntered)
        {moveSpeed=normalSpeed;}
    } 

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "SpeedUp")
        {
            moveSpeed = boostSpeed;
            speedUpEntered = true;
        }
    }


    void Update()
    {
        if (!Countdown.levelEnded)
        {
            steerAmount = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
            moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        }
        else
        {
            steerAmount = 0;
            moveAmount = 0;
        }
        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
    }
}
