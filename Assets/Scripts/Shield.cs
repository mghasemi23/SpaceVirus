using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public void Time(float time)
    {
        StartCoroutine(DestroyShield(time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().moveSpeed = collision.gameObject.GetComponent<Enemy>().moveSpeed / 2;
        }
    }

    private IEnumerator DestroyShield(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
