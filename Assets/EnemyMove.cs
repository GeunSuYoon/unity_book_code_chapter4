using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public float	speed = 0.02f;
	public float	waitSecond = 10.0f;

	Transform		tr;
	int				hitCounter = 0;
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

	void		OnTriggerEnter2D(Collider2D collision)
	{
		hitCounter++;
		Destroy(collision.gameObject);
		if (hitCounter > 1)
		{
			Destroy(this.gameObject);
			GameObject.Find("GameManager").GetComponent<Score>().score += 10;
		}
	}

	IEnumerator DestroySelf()
	{
		yield return (new WaitForSeconds(waitSecond));
		Destroy(this.gameObject);
	}
}
