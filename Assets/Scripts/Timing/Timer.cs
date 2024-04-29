using System;
using UnityEngine;

namespace JMG.Timing
{
	public abstract class Timer : MonoBehaviour
	{
		public Action OnStart, OnPause, OnUpdateValue;
		public float TimerValue { get; protected set; }
		public bool IsRunning { get; private set; }
		private float updateValueDelay, updateTimeElapsed;	
		private float totalTimeElapsed;	

		private void Awake()
		{
			updateValueDelay = 0.5f;
			PauseTimer();
		}

		public void SetTimerValue(float time)
		{
			TimerValue = time;
		}

		public void StartTimer()
		{
			IsRunning = true;
			OnStart?.Invoke();
		}

		public void PauseTimer()
		{
			IsRunning = false;
			OnPause?.Invoke();
		}
		
		public float GetTotalTimeElapsed()
		{
			return totalTimeElapsed;
		}
		
		public void ResetTimer()
		{
		 	TimerValue = 0f;
			totalTimeElapsed = 0f;
		}

		protected abstract void IncrementTime(float timePassed);

		private void Update()
		{
			if (IsRunning == false) return;

			float timePassed = Time.deltaTime * DebugTool.timeMultiplier;

			IncrementTime(timePassed);
			
			totalTimeElapsed += timePassed;
			updateTimeElapsed += timePassed;

			if (updateTimeElapsed > updateValueDelay)
			{
				updateTimeElapsed = 0f;
				OnUpdateValue?.Invoke();
			}
		}
	}
}