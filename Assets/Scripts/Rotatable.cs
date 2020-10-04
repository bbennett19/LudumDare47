using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public float rotationRate;
    public float timeToSpeed;

    public float maxRotation;
    public Rigidbody2D rigidbody;

    private float rotateAmount = 0f;
    private float currentRotationRate = 0f;
    private float rotationModifier = 0f;
    private float elapsedInDirection;

    // Update is called once per frame
    void Update()
    {
        elapsedInDirection += Time.deltaTime;

        if ((rotationModifier == -1f || rotationModifier == 0f) && Input.GetKeyDown(KeyCode.A))
        {
            rotationModifier = 1f;
            elapsedInDirection = 0f;
        }
        else if ((rotationModifier == 1f || rotationModifier == 0f) && Input.GetKeyDown(KeyCode.D)) 
        {
            rotationModifier = -1f;
            elapsedInDirection = 0f;
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotationModifier = 0f;
        }
        
        currentRotationRate = Mathf.Lerp(currentRotationRate, rotationRate * rotationModifier, elapsedInDirection / timeToSpeed);
        rotateAmount += currentRotationRate * Time.deltaTime;
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
