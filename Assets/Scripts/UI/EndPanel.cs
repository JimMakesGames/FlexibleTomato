using System;
using JMG.Timing;
using TMPro;
using UnityEngine;

namespace JMG.UI
{
	public class EndPanel : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private TextMeshProUGUI workTime, breakTime, workPercentage, breakPercentage;
		private TimeTracker timeTracker;

		private void Awake()
		{
			timeTracker = FindObjectOfType<TimeTracker>();
			Hide();
		}

		public void Show()
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;

			float workSeconds = timeTracker.GetTotalWorkSeconds();
			float breakSeconds = timeTracker.GetTotalBreakSeconds();

			TimeSpan workTimeSpan = TimeSpan.FromSeconds(workSeconds);
			TimeSpan breakTimeSpan = TimeSpan.FromSeconds(breakSeconds);
			workTime.text = string.Format("{0:00}:{1:00}:{2:00}", workTimeSpan.Hours, workTimeSpan.Minutes, workTimeSpan.Seconds);
			breakTime.text = string.Format("{0:00}:{1:00}:{2:00}", breakTimeSpan.Hours, breakTimeSpan.Minutes, breakTimeSpan.Seconds);

			float totalSeconds = workSeconds + breakSeconds;
			float percentageWork = workSeconds / totalSeconds;
			int percentageWorkInt = Mathf.RoundToInt(percentageWork * 100f);
			int percentageBreakInt = 100 - percentageWorkInt;

			workPercentage.text = percentageWorkInt.ToString() + "%";
			breakPercentage.text = percentageBreakInt.ToString() + "%";
		}

		public void Hide()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}
	}
}