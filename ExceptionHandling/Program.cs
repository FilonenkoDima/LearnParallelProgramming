namespace ExceptionHandling
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Test2();
			}
			catch (AggregateException ae)
			{
				foreach (var e in ae.InnerExceptions)
				{
					Console.WriteLine($"Handled elsewhere: {e.GetType()}");
				}
			}
			Console.WriteLine("Main program done. ");
			Console.ReadLine();
		}

		private static void Test()
		{
			var t = Task.Factory.StartNew(() =>
			{
				throw new InvalidOperationException("Can`t do this!") { Source = "t" };
			});

			var t2 = Task.Factory.StartNew(() =>
			{
				throw new AccessViolationException("Can't access this") { Source = "t2" };
			});

			try
			{
				Task.WaitAll(t, t2);
			}
			catch (AggregateException ae)
			{
				foreach (var e in ae.InnerExceptions)
				{
					Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
				}
			}
		}

		private static void Test2()
		{
			var t = Task.Factory.StartNew(() =>
			{
				throw new InvalidOperationException("Can`t do this!") { Source = "t" };
			});

			var t2 = Task.Factory.StartNew(() =>
			{
				throw new AccessViolationException("Can't access this") { Source = "t2" };
			});

			try
			{
				Task.WaitAll(t, t2);
			}
			catch (AggregateException ae)
			{
				ae.Handle(e =>
				{
					if (e is InvalidOperationException)
					{
						Console.WriteLine("Invalid operation!");
						return true;
					}
					return false;
				});
			}
		}
	}
}
