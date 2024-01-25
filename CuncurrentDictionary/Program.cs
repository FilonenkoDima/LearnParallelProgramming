using System.Collections.Concurrent;

namespace CuncurrentDictionary
{
	internal class Program
	{
		private static ConcurrentDictionary<string, string> capitals = 
			new ConcurrentDictionary<string, string>();

		public static void AddParis()
		{
			bool success = capitals.TryAdd("France", "Paris");
			string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";
			Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
		}

		static void Main(string[] args)
		{
			Task.Factory.StartNew(AddParis).Wait();
			AddParis();

			capitals["Ukraine"] = "Kharkiv";
			capitals.AddOrUpdate("Ukraine", "Kyiv", (k, old) => old + " --> Kyiv");

			Console.WriteLine(capitals["Ukraine"]);

			//capitals["Sweden"] = "Uppsala";
			var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
			Console.WriteLine(capOfSweden);

			const string toRemove = "Sweden";
			string removed;
			var didRemove = capitals.TryRemove(toRemove, out removed);
			if(didRemove )
			{
				Console.WriteLine($"Removed - {removed}");
			}
			else
			{
				Console.WriteLine($"Failed to Remove - {removed}");
			}

			foreach( var k in capitals ) { Console.WriteLine(k.Value + " - " + k.Key); }
		}
	}
}
