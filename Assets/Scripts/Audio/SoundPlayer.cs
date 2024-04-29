using UnityEngine;

namespace JMG.Audio
{
	public class SoundPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource click, breakReminder, workReminder;

		public void PlayClick()
		{
			click.Play();
		}

		public void PlayBreakReminder()
		{
			breakReminder.Play();
		}

		public void PlayWorkReminder()
		{
			workReminder.Play();
		}
	}
}