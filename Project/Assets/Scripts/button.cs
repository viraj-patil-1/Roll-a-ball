using UnityEngine;
using UnityEngine.UI;
public class button : MonoBehaviour
{
	public RectTransform pauseWin;
	public bool isActive = false;
		
	private void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = -1;
	}


	public void onclick(string level)
	{
		Time.timeScale = 1;
		Application.LoadLevel(level);
	}

	public void quit()
	{
		Application.Quit();

	}
	public void pause(){
		if (isActive == true) {
			unPause ();

		}
		else if (isActive==false)
		{

			Time.timeScale = 0;
			pauseWin.gameObject.SetActive (true);
			isActive = true;
		}
	}
	public void unPause(){
		Time.timeScale = 1;
		pauseWin.gameObject.SetActive(false);
		isActive = false;


	}
	public void restartGame(){
		this.unPause ();
		Application.LoadLevel(Application.loadedLevel);

	}

}
