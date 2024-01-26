namespace Continuations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//	var task = Task.Factory.StartNew(() =>
			//	{
			//		Console.WriteLine("Boiling water");
			//	});

			//	var task2 = task.ContinueWith(t =>
			//	{
			//		Console.WriteLine($"Completed task {t.Id}, pour water into cup. ");
			//	});

			//	task2.Wait();

			var task = Task.Factory.StartNew(() => "Task 1");
			var task2 = Task.Factory.StartNew(() => "Task 2");

			//var task3 = Task.Factory.ContinueWhenAll(new[] {task, task2}, tasks =>
			//{
			//	Console.WriteLine("Tasks completed:");
			//	foreach (var task in tasks)
			//	{
			//		Console.WriteLine(" - " + task.Result);
			//	}
			//	Console.WriteLine("All task done");
			//});

			var task3 = Task.Factory.ContinueWhenAny(new[] {task, task2}, task =>
			{
				Console.WriteLine("Tasks completed:");
					Console.WriteLine(" - " + task.Result);
				Console.WriteLine("All task done");
			});

			task3.Wait();
		}
	}
}
