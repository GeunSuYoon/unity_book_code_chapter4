using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
	public float	speed = 0.01f;
	public float	waitSecond = 5.0f;
	
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
        tr.Translate(Vector2.up * speed);
    }

	IEnumerator DestroySelf()
	{
		yield return new WaitForSeconds(waitSecond);
		Destroy(this.gameObject);
	}
}
