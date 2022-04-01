using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float _bulletDamage = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(_bulletDamage);
        }
    }
}
