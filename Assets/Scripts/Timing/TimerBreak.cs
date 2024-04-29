namespace JMG.Timing
{
	public class TimerBreak : Timer
	{
		protected override void IncrementTime(float timePassed)
		{
			TimerValue -= timePassed;
		}
	}
}