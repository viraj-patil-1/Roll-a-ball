using UnityEngine;

public class Rotater : MonoBehaviour
{
	public float xRot;
	public float yRot;
	public float zRot;
	public float speed;

	private void Update()
	{
		this.transform.Rotate(new Vector3(this.xRot, this.yRot, this.zRot) * Time.deltaTime*speed);
	}
}