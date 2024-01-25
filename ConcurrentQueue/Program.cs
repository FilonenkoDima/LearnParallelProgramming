using System.Collections.Concurrent;

namespace ConcurrentQueue_
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var q = new ConcurrentQueue<int>();
			q.Enqueue(1);
			q.Enqueue(2);

			// 2 1 <- front

			int result;
			if (q.TryDequeue(out result))
			{
				Console.WriteLine($"Remove element {result}");
			}

			if(q.TryPeek(out result))
			{
				Console.WriteLine($"Front element is {result}");
			}
		}
	}
}
