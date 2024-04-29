using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using JMG.Timing;
using JMG.Audio;

namespace JMG.UI
{
	public class SetupPanel : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI percentageValue, breakMinutesValue, workMinutesValue;
		[SerializeField] private Slider percentageSlider;
		[SerializeField] private Toggle soundToggle, subtractExcessToggle;
		[SerializeField] private CanvasGroup setupCanvas, timerCanvas;

		private int workMinutes, workMinutesMin, workMinutesMax, workMinutesStep, breakPercentage;
		private TimeTracker timeTracker;
		private ReminderSound reminderSound;
		
		private static string percentagePref = "Percentage";
		private static string workMinutesPref = "WorkMinutes";
		private static string soundOn = "SoundOn";
		private static string subtractExcess = "SubtractExcess";

		private void Awake()
		{						
			timeTracker = FindObjectOfType<TimeTracker>();
			reminderSound = FindObjectOfType<ReminderSound>();					
			
			SetDefaultValues();
			GetValuesFromPrefs();
			
			workMinutesValue.text = workMinutes.ToString();
			percentageSlider.value = breakPercentage;
			
			RefreshBreakMinutes();
			SliderUpdate();
			HideTimerCanvas();
			ShowSetupCanvas();
		}
		
		private void SetDefaultValues()
		{
			workMinutes = 25;
			workMinutesStep = 5;
			workMinutesMin = 10;
			workMinutesMax = 90;
			breakPercentage = 20;		
			soundToggle.isOn = true;
			subtractExcessToggle.isOn = true; 	
		}
		
		private void GetValuesFromPrefs()
		{
			breakPercentage = PlayerPrefs.GetInt(percentagePref, breakPercentage);
			workMinutes = PlayerPrefs.GetInt(workMinutesPref, workMinutes);
			
			if (PlayerPrefs.GetInt(soundOn, 1) == 1)			
				soundToggle.isOn = true;
			else
				soundToggle.isOn = false;
				
			if (PlayerPrefs.GetInt(subtractExcess, 1) == 1)
				subtractExcessToggle.isOn = true;
			else
				subtractExcessToggle.isOn = false;		
			
		}
		
		private void SetPrefsFromValues()
		{
			PlayerPrefs.SetInt(percentagePref, breakPercentage);
			PlayerPrefs.SetInt(workMinutesPref, workMinutes);

			if (soundToggle.isOn)
				PlayerPrefs.SetInt(soundOn, 1);
			else
				PlayerPrefs.SetInt(soundOn, 0);

			if (subtractExcessToggle.isOn)
				PlayerPrefs.SetInt(subtractExcess, 1);
			else
				PlayerPrefs.SetInt(subtractExcess, 0);
		}

		public void ClickReduceMinutes()
		{
			workMinutes -= workMinutesStep;
			if (workMinutes < workMinutesMin) workMinutes = workMinutesMin;
			workMinutesValue.text = workMinutes.ToString();
			RefreshBreakMinutes();
		}

		public void ClickIncreaseMinutes()
		{
			workMinutes += workMinutesStep;
			if (workMinutes > workMinutesMax) workMinutes = workMinutesMax;
			workMinutesValue.text = workMinutes.ToString();
			RefreshBreakMinutes();
		}

		public void SliderUpdate()
		{
			percentageValue.text = percentageSlider.value + "%";
			PlayerPrefs.SetFloat("", percentageSlider.value);
			RefreshBreakMinutes();			
		}
		
		public void ClickStart()
		{
			HideSetupCanvas();
			ShowTimerCanvas();
			
			timeTracker.SetWorkTimePreference(workMinutes * 60);
			timeTracker.SetWorkToBreakRatio(percentageSlider.value / 100f);
			
			if (soundToggle.isOn) 
				reminderSound.EnableReminding();
			else 			 
				reminderSound.DisableReminding();
				
			if (subtractExcessToggle.isOn)
				timeTracker.EnableSubtractBreakExcess();
			else
				timeTracker.DisableSubtractBreakExcess();

			timeTracker.SwitchToWorkTimer();
			
			SetPrefsFromValues();
		}

		private void ShowTimerCanvas()
		{
			timerCanvas.alpha = 1f;
			timerCanvas.interactable = true;
			timerCanvas.blocksRaycasts = true;
		}

		private void HideTimerCanvas()
		{
			timerCanvas.alpha = 0f;
			timerCanvas.interactable = false;
			timerCanvas.blocksRaycasts = false;
		}

		private void ShowSetupCanvas()
		{
			setupCanvas.alpha = 1f;
			setupCanvas.interactable = true;
			setupCanvas.blocksRaycasts = true;
		}

		private void HideSetupCanvas()
		{
			setupCanvas.alpha = 0f;
			setupCanvas.interactable = false;
			setupCanvas.blocksRaycasts = false;
		}

		private void RefreshBreakMinutes()
		{
			breakPercentage = (int)percentageSlider.value;
			int workSeconds = workMinutes * 60;
			float ratio = breakPercentage * 0.01f;
			int breakSeconds = Mathf.RoundToInt(workSeconds * ratio);
			TimeSpan timeSpan = TimeSpan.FromSeconds(breakSeconds);
			breakMinutesValue.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
		}
	}
}