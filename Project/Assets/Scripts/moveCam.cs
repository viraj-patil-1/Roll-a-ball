using UnityEngine;
using System.Collections;

public class moveCam : MonoBehaviour {

	public float Distance = 10f;
	public float xSpeed = 250f;
	public float ySpeed = 120f;
	public float yMinLimit = 10f;
	public float yMaxLimit = 60f;
	public GameObject target;
	public float x;
	public float y;
	public joyStick jostick;

	private void Start()
	{
		Vector3 eulerAngles = this.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
	}

	private void LateUpdate()
	{
		if (this.tag == "Camera1")
			this.target = GameObject.FindGameObjectWithTag("Player1");
		else if (this.tag == "Camera2")
			this.target = GameObject.FindGameObjectWithTag("Player2");
		if (!(bool) ((Object) this.target))
			return;
		this.x -= (float) ((double) this.jostick.Horizontal() * (double) this.xSpeed * 0.04);
		this.y += (float) ((double) this.jostick.Vertical() * (double) this.ySpeed * 0.04);

		this.y = moveCam.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
		Quaternion quaternion = Quaternion.Euler(this.y, this.x, 0.0f);
		Vector3 vector3_1 = new Vector3(0.0f, 0.0f, -this.Distance);
		Vector3 vector3_2 = quaternion * vector3_1 + this.target.transform.position;
		this.gameObject.transform.rotation = quaternion;
		this.gameObject.transform.position = vector3_2;
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if ((double) angle < -360.0)
			angle += 360f;
		if ((double) angle > 360.0)
			angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}
}
