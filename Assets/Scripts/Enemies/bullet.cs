using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float _destroyTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    IEnumerator CountTimer()
    {
        yield return new WaitForSeconds(_destroyTime);

        Destroy(gameObject);
    }
}
