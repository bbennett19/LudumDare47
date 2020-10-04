using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timeBetweenShots;
    public ProjectileSpawner leftGun;
    public ProjectileSpawner rightGun;
    public AudioClip shootSound;
    public GameObject leftPupil;
    public GameObject rightPupil;
    private float eyeRadius = 0.03f;
    private float timeSinceLastShot = float.MaxValue;
    private Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        timeSinceLastShot = timeBetweenShots;
        GameOverManager.Instance.RegisterCharacter(this);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= timeBetweenShots) 
        {
            leftGun.SpawnProjectile();
            rightGun.SpawnProjectile();
            AudioManager.Instance.PlayOneShot(shootSound);
            timeSinceLastShot = 0f;
        }

        Vector3 pos = rigidbody2.velocity.normalized * eyeRadius;
        pos.z = -1;
        leftPupil.transform.localPosition = pos;
        rightPupil.transform.localPosition = pos;
    }

    public void GameOver() 
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(GameOverInternal());
    }

    private IEnumerator GameOverInternal() 
    {
        float elapsed = 0f;
        Vector2 offScreen = new Vector2(0, 15);

        while(elapsed < 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, 10f * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } 

        while(elapsed < 9f) 
        {
            transform.position = Vector2.MoveTowards(transform.position, offScreen, 30f * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
