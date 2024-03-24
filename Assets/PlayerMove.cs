using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
	public float		speed = 3;
	// player move position limit
	public Vector2		limitPoint1;
	public Vector2		limitPoint2;
	// bullet object
	public GameObject	prefaBullet;

	Transform			tr;
	Vector2				mousePosition;
	int					hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

		StartCoroutine(FireBullet());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
		{
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (mousePosition.x < limitPoint1.x)
				mousePosition = new Vector2(limitPoint1.x, mousePosition.y);
			if (mousePosition.y > limitPoint1.y)
				mousePosition = new Vector2(mousePosition.x, limitPoint1.y);
			if (mousePosition.x > limitPoint2.x)
				mousePosition = new Vector2(limitPoint2.x, mousePosition.y);
			if (mousePosition.y < limitPoint2.y)
				mousePosition = new Vector2(mousePosition.x, limitPoint2.y);

			tr.position = Vector2.MoveTowards(tr.position, mousePosition, Time.deltaTime * speed);
		}
    }

	// draw limitPoint1 and limitPoint2 as square to check the boundary
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(limitPoint1, new Vector2(limitPoint2.x, limitPoint1.y));
		Gizmos.DrawLine(limitPoint1, new Vector2(limitPoint1.x, limitPoint2.y));
		Gizmos.DrawLine(new Vector2(limitPoint1.x, limitPoint2.y), limitPoint2);
		Gizmos.DrawLine(new Vector2(limitPoint2.x, limitPoint1.y), limitPoint2);
	}

	IEnumerator FireBullet()
	{
		while (true)
		{
			Instantiate(prefaBullet, tr.position, Quaternion.identity);

			yield return (new WaitForSeconds(0.3f));
		}
	}

	void	OnTriggerEnter2D(Collider2D collision)
	{
		// hitCounter++;
		// Destroy(collision.gameObject);
		// if (hitCounter > 4)
		SceneManager.LoadScene("GameOver");
	}
}
