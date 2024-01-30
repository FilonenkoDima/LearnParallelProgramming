namespace ParallelInvokeForForEach
{
	internal class Program
	{

		public static IEnumerable<int> Range(int start, int end, int step)
		{
			for (int i = start; i < end; i += step)
			{
				yield return i;
			}
		}

		static void Main(string[] args)
		{

			int[] values = new int[100];
			var a = new Action(() => { Console.WriteLine("First " + Task.CurrentId); });
			var b = new Action(() => { Console.WriteLine("Second " + Task.CurrentId); });
			var c = new Action(() => { Console.WriteLine("Third " + Task.CurrentId); });

			Parallel.Invoke(a, b, c);

			var po = new ParallelOptions();

			Parallel.For(1, 11, i =>
			{
				Console.WriteLine($"{i * i}\t");
			});

			string[] words = { "oh", "what", "a", "night" };

			Parallel.ForEach(words, i => Console.WriteLine(i + " has length " + i.Length + " (task " + Task.CurrentId + ")"));

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
		}
	}
}
