using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public int initHealth;
	public int health;
	public float speed;
	public Text countText;
	public Text countText1;
	public Text gameStatus;
	private float curYPos;
	public healthBarController H;
	public int pick;
	public Transform effect;
	public Transform cam;
	private Rigidbody rb;
	private int count;
	public RectTransform gameOver;
	public RectTransform nxtLvl;
	public float fallDist = -8.0f;
	private string sep = "/";
	public float maxVelocity = 10f;


	private void main()
	{
		Screen.sleepTimeout = -1;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	private void Start()
	{
		//GameObject.Find("Image").GetComponent<healthBarController>();
		Screen.orientation = ScreenOrientation.LandscapeLeft;

		this.health = this.initHealth;
		this.rb = this.GetComponent<Rigidbody>();


		this.count = 0;
		this.SetCountText();
	}

	private void update()
	{
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown("space"))
			this.transform.Translate(Vector3.up * 100f * Time.deltaTime, Space.World);
		this.curYPos = this.transform.position.y;
		if ((double)this.curYPos < fallDist) {

			gameOver.gameObject.SetActive(true);
			nxtLvl.gameObject.SetActive(false);
			this.gameStatus.text = "you fell off the cliff!";
			//Application.LoadLevel ("YouFall");
			this.gameObject.SetActive(false);
		}
		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			
			Vector2 vector2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if ((double) vector2.sqrMagnitude > 1.0)
				vector2 = vector2.normalized;
			Vector3 lhs = Vector3.Cross(Vector3.up, this.cam.forward);
			Vector3 vector3 = Vector3.Cross(lhs, Vector3.up);
		this.rb.AddForce((lhs * vector2.x + vector3 * vector2.y) * this.speed);

			if (this.rb.velocity.magnitude > maxVelocity) {

				//this.rb.velocity = rb.velocity.normalized * maxVelocity;
				this.rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
			}

		}
		else
		{
			this.speed = 20f;
			Vector2 vector2 = new Vector2(Input.acceleration.x, Input.acceleration.y);
			if ((double) vector2.sqrMagnitude > 1.0)
				vector2 = vector2.normalized;
			Vector3 lhs = Vector3.Cross(Vector3.up, this.cam.forward);
			Vector3 vector3 = Vector3.Cross(lhs, Vector3.up);
			this.rb.AddForce((lhs * vector2.x + vector3 * vector2.y) * this.speed);

			if (this.rb.velocity.magnitude > maxVelocity) {

				//this.rb.velocity = rb.velocity.normalized * maxVelocity;
				this.rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
			}

		}

}
	

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "coin")
		{
			Object.Instantiate((Object) this.effect, this.transform.position, this.transform.rotation);
			other.gameObject.SetActive(false);
			this.count = this.count + 1;
			this.SetCountText();
		}
		if (other.gameObject.tag == "Enemy")
		{
			/*if (SystemInfo.deviceType != DeviceType.Desktop)
				Object.Instantiate((Object) this.effect, this.transform.position, this.transform.rotation);*/
			other.gameObject.SetActive(false);
			if (this.health > 1)
			{
				this.H.amount = this.H.amount - 0.2f;
				--this.health;
				if (H.amount == 1f) {

					this.H.iColor  = new Color32 (150, 216, 87, 255);

				} else if (H.amount == 0.8f) {
					this.H.iColor  = new Color32 (193, 216, 87, 255);
				} 

				else if (H.amount == 0.6f) {
					this.H.iColor  = new Color32 (231, 147, 58, 255);
				}

				else if (H.amount <= 0.5f) {
					this.H.iColor  = new Color32 (240, 35, 35, 255);

				}

			}
			else
			{
				this.H.amount = 0.0f;
				MonoBehaviour.print((object) "you got killed");
				this.gameObject.SetActive(false);
				this.gameStatus.text="you got killed!";
				gameOver.gameObject.SetActive(true);
				nxtLvl.gameObject.SetActive(false);
				//Object.Destroy((Object) this);

			

			}

		}
		if (other.gameObject.tag == "Heal")
		{
			other.gameObject.SetActive(false);
			if (this.health > 0)
			{
				this.H.amount = this.H.amount + 0.2f;
				++this.health;
				if (H.amount == 1f) {

					this.H.iColor  = new Color32 (150, 216, 87, 255);

				} else if (H.amount == 0.8f) {
					this.H.iColor  = new Color32 (193, 216, 87, 255);
				} 

				else if (H.amount == 0.6f) {
					this.H.iColor  = new Color32 (231, 147, 58, 255);
				}

				else if (H.amount <= 0.5f) {
					this.H.iColor  = new Color32 (240, 35, 35, 255);

				}
			}

		}
		if (!(other.gameObject.tag == "Hole"))
			return;
		MonoBehaviour.print((object) "Game Over!");
		//Application.LoadLevel("GameOver");
		this.gameObject.SetActive(false);
		//Object.Destroy((Object) this);
		this.gameStatus.text="you fell in the hole!";
		gameOver.gameObject.SetActive(true);
		nxtLvl.gameObject.SetActive(false);
	}



	private void SetCountText()
	{
		this.countText.text = "  : " + this.count.ToString()+sep+this.pick;
		this.countText1.text = "  : " + this.count.ToString()+sep+this.pick;

		if (this.count < this.pick)
			return;
		//Application.LoadLevel("WinScreen");
		this.gameStatus.text="You Won!";
		gameOver.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}

	private void OnTriggerStay(Collider other){    

		if(other.gameObject.tag == "Slider_up"){
			transform.parent = other.transform;

		}
	}

	private void OnTriggerExit(Collider other){       
		if(other.gameObject.tag == "Slider_up"){
			transform.parent = null;

		}
	}    

}

