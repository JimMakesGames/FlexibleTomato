using System;
using UnityEngine;

namespace JMG.Timing
{
	public class TimeTracker : MonoBehaviour
	{
		public Action OnWorkTimeExceeded, OnBreakTimeExceeded;
		private TimerWork workTimer;
		private TimerBreak breakTimer;

		public float BreakTimeRemaining { get; private set; }
		private float workTimeElapsed, breakTimeLeftover, workTimePreference;
		private float ratio;
		private bool isSubstractExcessBreakEnabled;

		private void Awake()
		{
			workTimer = FindObjectOfType<TimerWork>();
			breakTimer = FindObjectOfType<TimerBreak>();
			workTimer.OnUpdateValue += UpdateWorkTime;
			breakTimer.OnUpdateValue += UpdateBreakTime;
		}
		
		public void EnableSubtractBreakExcess()
		{
			isSubstractExcessBreakEnabled = true;
		}
		
		public void DisableSubtractBreakExcess()
		{
			isSubstractExcessBreakEnabled = false;
		}
		
		public void SetWorkTimePreference(float workTimePreference)
		{
			this.workTimePreference = workTimePreference;
		}
		
		public void SetWorkToBreakRatio(float ratio)
		{
			this.ratio = ratio;
		}

		public void SwitchToBreakTimer()
		{
			breakTimer.SetTimerValue(BreakTimeRemaining);
			breakTimer.StartTimer();
			workTimer.PauseTimer();			
		}

		public void SwitchToWorkTimer()
		{
			if (isSubstractExcessBreakEnabled)
				breakTimeLeftover = breakTimer.TimerValue;
			else
				breakTimeLeftover = 0f;
				
			workTimer.SetTimerValue(0);
			workTimer.StartTimer();
			breakTimer.PauseTimer();
		}
		
		public void Finish()
		{
			workTimer.PauseTimer();
			breakTimer.PauseTimer();
		}
		
		public float GetTotalWorkSeconds()
		{
			return workTimer.GetTotalTimeElapsed();			
		}
		
		public float GetTotalBreakSeconds()
		{
			return breakTimer.GetTotalTimeElapsed();
		}
		
		public void ResetAllTimers()
		{
			workTimer.ResetTimer();
			breakTimer.ResetTimer();
			workTimeElapsed = 0f;
			breakTimeLeftover = 0f;
			BreakTimeRemaining = 0f;
		}

		private void UpdateWorkTime()
		{
			workTimeElapsed = workTimer.TimerValue;
			BreakTimeRemaining = (workTimeElapsed * ratio) + breakTimeLeftover;

			if (workTimeElapsed > workTimePreference)
			{
				OnWorkTimeExceeded?.Invoke();
			}
		}

		private void UpdateBreakTime()
		{
			BreakTimeRemaining = breakTimer.TimerValue;

			if (BreakTimeRemaining < 0f)
			{
				OnBreakTimeExceeded?.Invoke();
			}
		}
	}
}