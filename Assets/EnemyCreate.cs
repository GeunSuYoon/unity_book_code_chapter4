using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
	public GameObject	prefaEnemy;

	public Vector2		limitMin;
	public Vector2		limitMax;

	public float		creatingMaxTime = 1.0f;
	public float		creatingMinTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(CreateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator	CreateEnemy()
	{
		while (true)
		{
			float r = Random.Range(limitMin.x, limitMax.x);
			Vector2 creatingPoint = new Vector2(r, limitMin.y);

			Instantiate(prefaEnemy, creatingPoint, Quaternion.identity);

			float creatingTime = Random.Range(creatingMaxTime, creatingMinTime);
			yield return (new WaitForSeconds(creatingTime));
		}
	}

	private void	OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(limitMin, limitMax);
	}
}
