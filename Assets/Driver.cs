using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float normalSpeed = 10f;
    [SerializeField] float steeringSpeed = 200f;
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float slowSpeed = 5f;
    [SerializeField] float boostSpeed = 20f;

    [SerializeField] float slowTime = 2f;
    
    /* void setNormalSpeed ()
    {
        moveSpeed = normalSpeed;
    } */

    void Start() 
    {
        StartCoroutine(Countdown(3));
        //below does the same job
        //StartCoroutine("Countdown",3);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        moveSpeed = slowSpeed;
        //Invoke starts after specified time
        //Invoke("setNormalSpeed",slowTime);


        //You can pass arguments to a coroutine
        StartCoroutine(ResetMoveSpeed(slowTime));
    }

    IEnumerator Countdown (int seconds)
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
        yield return new WaitForSeconds(slowTime);
        moveSpeed = normalSpeed;
    } 

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "SpeedUp")
        {
            moveSpeed = boostSpeed;
        }
    }


    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
    }
}
