using UnityEngine;
using UnityEngine.EventSystems;

public class joyStick : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
{
	public RectTransform bgImg;
	public RectTransform joystickImg;
	public Vector3 inputVec;
	public Vector2 pos;

	private void Start()
	{
		this.bgImg = this.GetComponent<RectTransform>();
		this.joystickImg = this.transform.GetChild(0).GetComponent<RectTransform>();
	}

	public void OnDrag(PointerEventData ped)
	{
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.bgImg, ped.position, ped.pressEventCamera, out this.pos))
			return;
		this.pos.x = this.pos.x / this.bgImg.sizeDelta.x;
		this.pos.y = this.pos.y / this.bgImg.sizeDelta.y;
		this.inputVec = new Vector3(this.pos.x, 0.0f, this.pos.y);
		this.inputVec = (double) this.inputVec.magnitude <= 1.0 ? this.inputVec : this.inputVec.normalized;
		this.joystickImg.anchoredPosition = (Vector2) new Vector3(this.inputVec.x * (this.bgImg.sizeDelta.x / 3f), this.inputVec.z * (this.bgImg.sizeDelta.y / 3f));
	}

	public void OnPointerDown(PointerEventData ped)
	{
		this.OnDrag(ped);
	}

	public void OnPointerUp(PointerEventData ped)
	{
		this.inputVec = Vector3.zero;
		this.joystickImg.anchoredPosition = (Vector2) Vector3.zero;
	}

	public float Horizontal()
	{
		if ((double) this.inputVec.x != 0.0)
			return this.inputVec.x;
		return this.inputVec.x;
	}

	public float Vertical()
	{
		if ((double) this.inputVec.z != 0.0)
			return this.inputVec.z;
		return this.inputVec.z;
	}
}
