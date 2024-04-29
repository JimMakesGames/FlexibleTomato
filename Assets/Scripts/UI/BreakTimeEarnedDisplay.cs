using UnityEngine;
using JMG.Timing;

namespace JMG.UI
{
	public class BreakTimeEarnedDisplay : MonoBehaviour
	{
		private TimerWork workTimer;
		private CanvasGroup canvasGroup;
		private TimeDisplay timeDisplay;
		private TimeTracker timeTracker;

		private void Awake()
		{
			workTimer = FindObjectOfType<TimerWork>();
			canvasGroup = GetComponent<CanvasGroup>();
			timeDisplay = GetComponentInChildren<TimeDisplay>();
			timeTracker = FindObjectOfType<TimeTracker>();
			workTimer.OnUpdateValue += UpdateTimeValue;
			workTimer.OnStart += Show;
			workTimer.OnPause += Hide;
			Hide();
		}

		public void Show()
		{
			canvasGroup.alpha = 1f;
		}

		public void Hide()
		{
			canvasGroup.alpha = 0f;
		}

		public void UpdateTimeValue()
		{
			timeDisplay.UpdateTimeValue(timeTracker.BreakTimeRemaining);
		}
	}
}