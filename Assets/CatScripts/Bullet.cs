using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall") // º®¿¡ ÃÑ¾Ë ´êÀ¸¸é »ç¶óÁü
        {
            Destroy(gameObject);
        }
    }
}
