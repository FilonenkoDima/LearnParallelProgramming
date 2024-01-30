namespace AsParallelAndParallelQuery
{
	internal class Program
	{
		static void Main(string[] args)
		{
			const int count = 50;

			var items = Enumerable.Range(0, count).ToArray();
			var results = new int[count];

			items.AsParallel().ForAll(x =>
			{
				int newValue = x * x * x;
				Console.WriteLine($"{newValue} ({Task.CurrentId})");
				results[x] = newValue;
			});
			Console.WriteLine();
			Console.WriteLine();

			foreach (var item in results)
			{
				Console.WriteLine(item);
			}
				Console.WriteLine();
				Console.WriteLine();

			var cubes = items.AsParallel().AsOrdered().Select(x => x*x*x);

			foreach (var item in cubes)
				Console.WriteLine(item);
		}
	}
}
