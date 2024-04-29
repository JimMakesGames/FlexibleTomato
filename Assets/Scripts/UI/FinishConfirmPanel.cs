using JMG.Audio;
using JMG.Timing;
using UnityEngine;

namespace JMG.UI
{
	public class FinishConfirmPanel : MonoBehaviour
	{
		[SerializeField] private EndPanel endPanel;
		[SerializeField] private CanvasGroup timerCanvas;
		private CanvasGroup canvasGroup;
		private TimeTracker timeTracker;
		private ReminderSound reminderSound;

		private void Awake()
		{
			reminderSound = FindObjectOfType<ReminderSound>();
			timeTracker = FindObjectOfType<TimeTracker>();
			canvasGroup = GetComponent<CanvasGroup>();
			Hide();
		}

		public void Show()
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		public void Hide()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		public void ClickYes()
		{
			timerCanvas.alpha = 0f;
			timerCanvas.interactable = false;
			timerCanvas.blocksRaycasts = false;
			reminderSound.StopReminding();
			timeTracker.Finish();
			Hide();
			endPanel.Show();
		}

		public void ClickNo()
		{
			Hide();
		}
	}
}