namespace JMG.Timing
{
	public class TimerWork : Timer
	{
		protected override void IncrementTime(float timePassed)
		{
			TimerValue += timePassed;
		}
	}
}