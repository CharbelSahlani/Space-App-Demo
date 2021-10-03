using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	public bool clockwise;
	public bool speedOrTime;
	[Tooltip("Speed: No or Length of day: Yes")]
	public float rotationSpeed;

	public float speed;
	private float direction = 1f;

    void Start()
    {
		if (speedOrTime)
			speed = 360f / (rotationSpeed * 36f);
		else
			speed = rotationSpeed;
    }

    void Update()
	{
		if (direction < 1f)
			direction += Time.deltaTime;

		if (clockwise)
			transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
		else
			transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
	}
}
