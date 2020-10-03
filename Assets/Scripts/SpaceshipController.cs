using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public int health;
    public int points;

    void Awake() {
        GameController.Instance.UpdateShipCount(1);    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player")) 
        {
            health -= 1;

            if (health == 0) 
            {
                ScoreController.Instance.UpdateScore(points);
                GameController.Instance.UpdateShipCount(-1);
                Destroy(this.gameObject);
            }
        }    
    }
}
