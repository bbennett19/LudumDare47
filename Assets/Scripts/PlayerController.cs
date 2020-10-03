using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timeBetweenShots;
    public ProjectileSpawner leftGun;
    public ProjectileSpawner rightGun;
    private float timeSinceLastShot = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= timeBetweenShots) 
        {
            leftGun.SpawnProjectile();
            rightGun.SpawnProjectile();
            timeSinceLastShot = 0f;
        }
    }
}
