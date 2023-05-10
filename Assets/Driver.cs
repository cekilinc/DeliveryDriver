using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float normalSpeed = 20f;
    [SerializeField] float steeringSpeed = 300f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 30f;

    [SerializeField] float slowTime = 2f;
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        moveSpeed = slowSpeed;
        StartCoroutine(ResetMoveSpeed(slowTime));
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
