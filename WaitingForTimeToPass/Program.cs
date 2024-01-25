namespace WaitingForTimeToPass
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var cts = new CancellationTokenSource();
			var token = cts.Token;
			var t = new Task(() =>
			{
				Console.WriteLine("Press anykey to disarm; you have 5 seconds");
				bool cancelled = token.WaitHandle.WaitOne(5000);
				Console.WriteLine(cancelled ? "Bomb disarmed" : "BOOM");
			}, token);

			t.Start();

			Console.ReadKey();
			cts.Cancel();

			Console.WriteLine("Main program done. ");
		}
	}
}
