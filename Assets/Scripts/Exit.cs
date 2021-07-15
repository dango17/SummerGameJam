using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : CrowdMaster
{
    public float destroyTimer;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            Destroy(collision.gameObject);
        }

    }

    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Block"))
        {
            destroyTimer -= Time.deltaTime;

            if (destroyTimer <= 0)
            {
                this.transform.position += Vector3.down * 100;
                blocked = true;
                destroyTimer = 3f;
            }

        }
    }
}
