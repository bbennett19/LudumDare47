using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public int health;
    public int points;

    public List<Sprite> damageIndication;
    public SpriteRenderer spriteRenderer;
    public AudioClip explosionSound;
    public AudioClip hitSound;
    public ParticleSystem explosionBits;
    public GameObject player;
    public float orbitRadius = 5;
    public Vector2 orbitSpeedRange;

    private int damageIndicationIndex = 0;
    private bool orbiting = false;
    private float orbitAngle;
    private float orbitSpeed;

    private void Awake() 
    {
        orbitSpeed = Random.Range(orbitSpeedRange.x, orbitSpeedRange.y);
    }

    private void Start() {
        GameOverManager.Instance.RegisterSpaceship(this);
        player = GameObject.Find("Player");
    }

    private void OnDestroy() 
    {
        GameOverManager.Instance.UnregisterSpaceship(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!orbiting)
        {
            if (other.gameObject.tag.Equals("Player")) 
            {
                health--;
                damageIndicationIndex++;

                if (health == 0) 
                {
                    ScoreController.Instance.UpdateScore(points);
                    AudioManager.Instance.PlayOneShot(explosionSound);
                    Instantiate(explosionBits, transform.position, Quaternion.Euler(90, 0, 0));
                    Destroy(this.gameObject);
                }
                else 
                {
                    AudioManager.Instance.PlayOneShot(hitSound);
                    spriteRenderer.sprite = damageIndication[damageIndicationIndex];
                }
            }    
        }
    }

    private void Update() {
        if (orbiting)
        {
            orbitAngle += orbitSpeed * Time.deltaTime;
            Vector2 targetPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad * orbitAngle), Mathf.Sin(Mathf.Deg2Rad * orbitAngle)) * orbitRadius;
            targetPosition += new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 30f * Time.deltaTime);
        }
    }

    public void StartGameOver() 
    {
        orbiting = true;
        orbitAngle = Vector2.SignedAngle(player.transform.position, transform.position);
    }
}
