using System;
using System.Collections;
using JMG.Timing;
using UnityEngine;

namespace JMG.Audio
{
	public class ReminderSound : MonoBehaviour
	{
		private Coroutine reminderCoroutine;
		private Action Notification;
		private float timeInterval = 60f;
		private bool isRunning, isReminderEnabled;
		private TimeTracker timeTracker;
		private TimerWork workTimer;
		private TimerBreak breakTimer;
		private SoundPlayer soundPlayer;

		private void Awake()
		{
			soundPlayer = FindObjectOfType<SoundPlayer>();
			workTimer = FindObjectOfType<TimerWork>();
			breakTimer = FindObjectOfType<TimerBreak>();
			timeTracker = FindObjectOfType<TimeTracker>();
			timeTracker.OnWorkTimeExceeded += StartRemindingWork;
			timeTracker.OnBreakTimeExceeded += StartRemindingBreak;
			workTimer.OnStart += StopReminding;
			breakTimer.OnStart += StopReminding;
		}
		
		public void EnableReminding()
		{
			isReminderEnabled = true;
		}
		
		public void DisableReminding()
		{
			isReminderEnabled = false;
		}
		
		private void StartRemindingWork()
		{
			StartReminding(soundPlayer.PlayWorkReminder);
		}
		
		private void StartRemindingBreak()
		{
			StartReminding(soundPlayer.PlayBreakReminder);
		}

		public void StartReminding(Action Notification)
		{
			if (isReminderEnabled == false) return;
			if (isRunning) return;
			isRunning = true;
			this.Notification = Notification;
			reminderCoroutine = StartCoroutine(Remind());
		}

		public void StopReminding()
		{
			if (isRunning)
			{
				StopCoroutine(reminderCoroutine);
				isRunning = false;
			}
		}

		private IEnumerator Remind()
		{
			while (true)
			{
				Notification();
				yield return new WaitForSeconds(timeInterval / DebugTool.timeMultiplier);
			}
		}
	}
}