using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public float rotationRate;
    public float maxRotation;
    public Rigidbody2D rigidbody;

    private float rotateAmount = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            rotateAmount += rotationRate * Time.deltaTime;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            rotateAmount += -rotationRate * Time.deltaTime;
        }
    }

    void FixedUpdate() 
    {
        rigidbody.MoveRotation(ClampAngle(rigidbody.rotation + rotateAmount, -maxRotation, maxRotation));
        rotateAmount = 0f;
    }

    private float ClampAngle(float value, float min, float max) 
    {
        if (value > 180f) {
            value -= 360f;
        }

        return Mathf.Clamp(value, min, max);
    }
}
