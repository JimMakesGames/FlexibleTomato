using UnityEngine;
using JMG.Timing;

namespace JMG.UI
{
	public class ReminderDisplay : MonoBehaviour
	{
		[SerializeField] private CanvasGroup reminderPopupWork, reminderPopupBreak;
		private TimerWork workTimer;
		private TimerBreak breakTimer;
		private TimeTracker timeTracker;

		private void Awake()
		{
			workTimer = FindObjectOfType<TimerWork>();
			breakTimer = FindObjectOfType<TimerBreak>();
			timeTracker = FindObjectOfType<TimeTracker>();
			timeTracker.OnWorkTimeExceeded += OnWorkTimeExceeded;
			timeTracker.OnBreakTimeExceeded += OnBreakTimeExceeded;
			workTimer.OnStart += OnWorkTimerStart;
			breakTimer.OnStart += OnBreakTimerStart;
			reminderPopupWork.alpha = 0f;
			reminderPopupBreak.alpha = 0f;
		}

		private void OnWorkTimerStart()
		{
			reminderPopupWork.alpha = 0f;
		}

		private void OnBreakTimerStart()
		{
			reminderPopupBreak.alpha = 0f;
		}

		private void OnWorkTimeExceeded()
		{
			reminderPopupBreak.alpha = 1f;
		}

		private void OnBreakTimeExceeded()
		{			
			reminderPopupWork.alpha = 1f;			
		}
	}
}