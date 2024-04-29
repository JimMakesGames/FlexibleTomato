using System;
using TMPro;
using UnityEngine;
using JMG.Timing;

namespace JMG.UI
{
	public class TimerPanel : MonoBehaviour
	{
		public Action OnRefresh;

		[SerializeField] private TextMeshProUGUI timerText;
		[SerializeField] private Timer timer;

		private CanvasGroup canvasGroup;
		private TimeDisplay timeDisplay;

		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			timeDisplay = GetComponentInChildren<TimeDisplay>();
			HideTimer();
			timer.OnStart += ShowTimer;
			timer.OnPause += HideTimer;
			timer.OnUpdateValue += RefreshDisplay;
		}

		public Timer GetTimer()
		{
			return timer;
		}

		public void ShowTimer()
		{
			canvasGroup.alpha = 1f;
			RefreshDisplay();
		}

		public void HideTimer()
		{
			canvasGroup.alpha = 0f;
		}

		private void RefreshDisplay()
		{
			timeDisplay.UpdateTimeValue(timer.TimerValue);
			OnRefresh?.Invoke();
		}
	}
}