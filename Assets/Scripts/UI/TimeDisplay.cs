using System;
using TMPro;
using UnityEngine;

namespace JMG.UI
{
	public class TimeDisplay : MonoBehaviour
	{
		private TextMeshProUGUI timeValueText;

		private void Awake()
		{
			timeValueText = GetComponent<TextMeshProUGUI>();
		}

		public void UpdateTimeValue(float timeValue)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(timeValue);

			if (timeValue < 0f)
			{
				timeValueText.text = "-";
				timeValueText.color = Color.red;
			}
			else
			{
				timeValueText.text = "";
				timeValueText.color = Color.white;
			}
			
			if (timeSpan.Hours == 0)
			{
				timeValueText.text += string.Format("{0:00}:{1:00}", Mathf.Abs(timeSpan.Minutes), Mathf.Abs(timeSpan.Seconds));
			}
			else
			{
				timeValueText.text += string.Format("{0:0}:{1:00}:{2:00}", Mathf.Abs(timeSpan.Hours), Mathf.Abs(timeSpan.Minutes), Mathf.Abs(timeSpan.Seconds));
			}

		}
	}
}