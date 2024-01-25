namespace SpinLockingAndLockRecursion
{
	internal class Program
	{
		private static SpinLock sl = new SpinLock(true);
		public static void LockRecursion(int x)
		{
			bool lockTaken = false;
			try
			{
				sl.Enter(ref lockTaken);
			}
			catch (LockRecursionException e)
			{
				Console.WriteLine("Exception: " + e);
			}
			finally 
			{
				if (lockTaken)
				{
					Console.WriteLine($"Took a lock, x = {x}");
					LockRecursion(x-1);
					sl.Exit();
				} 
				else
				{
					Console.WriteLine($"Failed to take a lock, x = {x}");
				}
			}
		}
		static void Main(string[] args)
		{
			LockRecursion(5);
		}
	}
}
