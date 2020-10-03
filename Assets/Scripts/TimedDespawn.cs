using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDespawn : MonoBehaviour
{
    public float time;

    private float elapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= time)
        {
            Destroy(this.gameObject);
        }
    }
}
