namespace ManualResetEventAndAutoResetEvent
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//var evt = new ManualResetEventSlim();
			var evt = new AutoResetEvent(false);

			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Boiling water");
				evt.Set();
			});

			var makeTea = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Waiting for water...");
				evt.WaitOne();
				Console.WriteLine("Here is your tea");
				var ok = evt.WaitOne(1000);
				if(ok)
				{
					Console.WriteLine("Enjoy your tea");
				}
				else
				{
					Console.WriteLine("No tea for you");
				}
			});

			makeTea.Wait();	
		}
	}
}
