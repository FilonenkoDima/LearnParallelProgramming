namespace Mutex_
{
	public class BankAccount
	{
		public object padlock = new object();
		private int balance;

		public int Balance
		{
			get => balance;
			private set => balance = value;
		}
		public void Deposit(int amount)
		{
			// +=
			// op1: temp <- get_Balance() + amount
			// op2: set_Balance(temp)
			Interlocked.Add(ref balance, amount);
		}

		public void Withdraw(int amount)
		{
			Interlocked.Add(ref balance, -amount);
		}

		public void Transfer(BankAccount where, int amount)
		{
			Balance -= amount;
			where.Balance += amount;
		}
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			var tasks = new List<Task>();
			var ba = new BankAccount();
			var ba2 = new BankAccount();

			Mutex mutex = new Mutex();
			Mutex mutex2 = new Mutex();

			for (int i = 0; i < 10; i++)
			{
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex.WaitOne();
						try
						{
							ba.Deposit(100);
						}
						finally
						{
							if(haveLock)
								mutex.ReleaseMutex();
						}
					}
				}));

				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex.WaitOne();
						try
						{
							ba.Withdraw(100);
						}
						finally
						{
							if (haveLock)
								mutex.ReleaseMutex();
						}
					}
				}));

			}

			Task.WaitAll(tasks.ToArray());

			Console.WriteLine($"Final balance is {ba.Balance}.");
		
			
			for(int i = 0; i < 10; i++)
			{
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex.WaitOne();
						try
						{
							ba.Deposit(1);
						}
						finally
						{
							if (haveLock)
							{
								mutex.ReleaseMutex();
							}
						}
					}
				}));
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex2.WaitOne();
						try
						{
							ba2.Deposit(1);
						}
						finally
						{
							if (haveLock)
							{
								mutex2.ReleaseMutex();
							}
						}
					}
				}));
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for(int j = 0;j < 1000; j++)
					{
						bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
						try
						{
							ba.Transfer(ba2, 1);
						}
						finally
						{
							if (haveLock)
							{
								mutex.ReleaseMutex();
								mutex2.ReleaseMutex();
							}
						}
					}
				}));
			}


			Task.WaitAll(tasks.ToArray());

			Console.WriteLine($"Final balance ba is {ba.Balance}.");
			Console.WriteLine($"Final balance ba2 is {ba2.Balance}.");

		}
	}
}
