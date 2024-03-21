using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public float	speed = 0.02f;
	public float	waitSecond = 10.0f;

	Transform		tr;

    // Start is called before the first frame update
    void Start()
    {
		tr = GetComponent<Transform>();
		StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector2.down * speed);
    }

	void		OnTriggerEnter2D(Collider2D Collision)
	{
		Destroy(this.gameObject);
		Destroy(Collision.gameObject);
	}

	IEnumerator DestroySelf()
	{
		yield return (new WaitForSeconds(waitSecond));
		Destroy(this.gameObject);
	}
}
