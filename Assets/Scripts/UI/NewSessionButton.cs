using JMG.Timing;
using UnityEngine;

public class NewSessionButton : MonoBehaviour
{
	[SerializeField] private CanvasGroup setupCanvas, endCanvas;
	private TimeTracker timeTracker;

	private void Awake() 
	{
		timeTracker = FindObjectOfType<TimeTracker>();
	}

	public void Click()
	{
		timeTracker.ResetAllTimers();

		setupCanvas.alpha = 1f;
		setupCanvas.interactable = true;
		setupCanvas.blocksRaycasts = true;
		
		endCanvas.alpha = 0f;
		endCanvas.interactable = false;
		endCanvas.blocksRaycasts = false;
	}
}
