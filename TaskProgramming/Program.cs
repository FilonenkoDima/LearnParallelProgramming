using System.Threading.Tasks;

namespace Demo
{
	internal static class Program
	{
		public static void Write(char c)
		{
			int i = 1000;
			while (i-- > 0)
			{
				Console.Write(c);
			}
		}

		public static void Write(object o)
		{
			int i = 1000;
			while (i-- > 0)
			{
				Console.Write(o);
			}
		}

		public static int TextLength(object o)
		{
			Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
			return o.ToString().Length;
		}

		public static void Main(string[] args)
		{
			string text1 = "testing", text2 = "this";
			var task1 = new Task<int>(TextLength, text1);
			task1.Start();
			Task<int> task2 = Task.Factory.StartNew<int>(TextLength, text2);

			Console.WriteLine($"Length of {text1} is {task1.Result}");
			Console.WriteLine($"Length of {text2} is {task2.Result}");

			Console.WriteLine("Main program done.");
			Console.ReadLine();
		}
	}
}