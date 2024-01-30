namespace ThreadLocalStorage
{
	internal class Program
	{
		static void Main(string[] args)
		{
			object o = new object();
			int sum = 0;
			lock (o)
			{
				Parallel.For(1, 1001, x =>
				{
					sum += x;
				});
				Console.WriteLine(sum);
			}

			lock (o)
			{
				sum = 0;
				Parallel.For(1, 1001, x =>
				{
					Interlocked.Add(ref sum, x);
				});
				Console.WriteLine(sum);
			}

			lock (o)
			{
				sum = 0;

				Parallel.For(1, 1001,
					() => 0,
					(x, state, tls) =>
					{
						tls += x;
					Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
						return tls;
					},
					partialSum =>
					{
						Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
						Interlocked.Add(ref sum, partialSum);
					});

				Console.WriteLine(sum);
			}
		}
	}
}
