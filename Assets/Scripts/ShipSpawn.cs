using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawn : MonoBehaviour
{
    public float yIncrease;
    public float timeToMoveIn;

    private float elapsed = 0f;
    private Vector2 targetPosition;

    private bool done = false;

    private void Awake() 
    {
        targetPosition = transform.position;
        transform.position = targetPosition + new Vector2(0, yIncrease);
        GameController.Instance.UpdateShipCount(1);  
    }

    // Update is called once per frame
    void Update()
    {
        if (!done) 
        {
            elapsed += Time.deltaTime;

            if (elapsed < timeToMoveIn) 
            {
                transform.position = transform.position = targetPosition + new Vector2(0, Mathf.Lerp(yIncrease, 0, elapsed / timeToMoveIn));
            }
            else 
            {
                transform.position = targetPosition;
                done = true;
            }
        }
    }

    private void OnDestroy() { 
        GameController.Instance.UpdateShipCount(-1);
    }
}
