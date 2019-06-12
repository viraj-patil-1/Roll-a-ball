using UnityEngine;
using UnityEngine.UI;

public class healthBarController : MonoBehaviour
{
	public float amount = 1f;
	public Color32 iColor ;

	private void Start()
	{
		
	}

	private void Update()
	{
		this.GetComponent<Image> ().fillAmount = this.amount;
		this.GetComponent<Image> ().color = this.iColor;

	}
}
