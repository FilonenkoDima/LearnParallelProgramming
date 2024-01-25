namespace Reader_WriterLocks
{
	internal class Program
	{
		static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		static Random random = new Random();

		static void Main(string[] args)
		{
			int x = 0;

			var tasks = new List<Task>();
			for (int i = 0; i < 10; i++)
			{
				tasks.Add(Task.Factory.StartNew(() =>
				{
					//padlock.EnterReadLock();
					//padlock.EnterReadLock();

					padlock.EnterUpgradeableReadLock();

					if (i % 2 == 0)
					{
						padlock.EnterWriteLock();
						x = 123;
						padlock.ExitWriteLock();
					}
					Console.WriteLine($"Entered read lock, x = {x}. ");
					Thread.Sleep(5000);

					//padlock.ExitReadLock();
					//padlock.ExitReadLock();

					padlock.ExitUpgradeableReadLock();

					Console.WriteLine($"Exited read lock, x ={x}. ");
				}));
			}

			try
			{
				Task.WaitAll(tasks.ToArray());
			}
			catch (AggregateException ae)
			{
				ae.Handle(e =>
				{
					Console.WriteLine(e);
					return true;
				});
			}

			while (true)
			{
				Console.ReadKey();
				padlock.EnterReadLock();
				Console.WriteLine("Write lock acquired");
				int newValue = random.Next();
				x = newValue;
				Console.WriteLine($"Set x = {x}");
				padlock.ExitReadLock();
				Console.WriteLine($"Wrire lock released");
			}
		}
	}
}
