using TMPro;
using UnityEngine;
using JMG.Timing;

namespace JMG.UI
{
	public class SwitchButton : MonoBehaviour
	{
		[SerializeField] private TimerPanel workTimerPanel, breakTimerPanel;
		private TextMeshProUGUI label;
		private TimeTracker timeTracker;

		private void Awake()
		{
			label = GetComponentInChildren<TextMeshProUGUI>();
			timeTracker = FindObjectOfType<TimeTracker>();
			UpdateAppearance();
		}

		public void Click()
		{
			if (workTimerPanel.GetTimer().IsRunning)
			{
				timeTracker.SwitchToBreakTimer();
			}
			else if (breakTimerPanel.GetTimer().IsRunning)
			{
				timeTracker.SwitchToWorkTimer();
			}

			UpdateAppearance();
		}

		private void UpdateAppearance()
		{
			if (workTimerPanel.GetTimer().IsRunning)
			{
				label.text = "Break time";
			}
			if (breakTimerPanel.GetTimer().IsRunning)
			{
				label.text = "Back to work";
			}
		}
	}
}