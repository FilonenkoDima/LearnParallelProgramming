namespace BreakingCancellationsAndExceptions
{
	internal class Program
	{
		private static ParallelLoopResult result;

		internal static void Demo()
		{
			var cts = new CancellationTokenSource();

			ParallelOptions po = new ParallelOptions();
			po.CancellationToken = cts.Token;

			result = Parallel.For(0, 20, po, (x, state) =>
			{
				Console.WriteLine($"{x}[{Task.CurrentId}]");

				if (x == 10)
				{
					//throw new Exception();
					//state.Stop();
					//state.Break();
					cts.Cancel();
				}
			});

			Console.WriteLine();
			Console.WriteLine($"Was loop completed? {result.IsCompleted}");
			if(result.LowestBreakIteration.HasValue)
			{
				Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
			}
		}
		static void Main(string[] args)
		{
			try
			{
				Demo();
			}
			catch (AggregateException ae)
			{
				ae.Handle(e =>
				{
					Console.WriteLine(e.Message);
					return true;
				});
			}
			catch(OperationCanceledException oce)
			{
				Console.WriteLine(oce);
			}
		}
	}
}
