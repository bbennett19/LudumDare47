using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;

    public void SpawnProjectile() 
    {
        Instantiate(projectile, this.transform.position, this.transform.rotation);
    }
}
