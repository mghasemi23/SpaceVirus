using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().ReceiveHealing(10);
            Destroy(gameObject);
        }
    }

    public Vector2 Limit;
    public float MoveTime;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        var t = 0f;
        var start = transform.position;
        var end = RandomPosition();
        while (t < 1)
        {
            t += Time.deltaTime / MoveTime;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        StartCoroutine(Move());
    }

    private Vector3 RandomPosition()
    {
        var x = Limit.x * Random.Range(-1f, 1f);
        var y = Limit.y * Random.Range(-1f, 1f);
        return new Vector3(_startPosition.x + x, _startPosition.y + y);
    }
}
