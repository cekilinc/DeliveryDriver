using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float steeringSpeed = 300f;
    void Start()
    {

    }

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
    }
}
