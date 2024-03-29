﻿namespace CancellationAndExceptions
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var cts = new CancellationTokenSource();
			var token = cts.Token;

			var items = ParallelEnumerable.Range(1, 20);
			var results = items.WithCancellation(token).Select(i =>
			{
				double result = Math.Log10(i);

				//if (result > 1)
				//	throw new InvalidOperationException();

				Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
				return result;
			});

			try
			{
				foreach (var c in results)
				{
					if (c > 1)
						cts.Cancel();

					Console.WriteLine("result = " + c);
				}
			}
			catch (AggregateException ae)
			{
				ae.Handle(e =>
				{
					Console.WriteLine($"{e.GetType().Name}: {e.Message}");
					return true;
				});
			}
			catch (OperationCanceledException oce)
			{
				Console.WriteLine("Canceled");
			}
		}
	}
}
