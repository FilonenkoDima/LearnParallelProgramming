namespace AsynchronousInitializationPattern
{
	public interface IAsyncInit
	{
		Task InitTask { get; }
	}

	public class MyClass : IAsyncInit
	{
		public MyClass()
		{
			InitTask = InitAsync();

		}
		public Task InitTask { get; }

		private async Task InitAsync()
		{
			await Task.Delay(1000);
		}
	}

	public class MyOtherClass : IAsyncInit
	{
		private readonly MyClass myClass;
		public MyOtherClass(MyClass myClass)
		{
			this.myClass = myClass;
			InitTask = InitAsync();
		}
		public Task InitTask { get; }

		private async Task InitAsync()
		{
			if (myClass is IAsyncInit ai)
			{
				await ai.InitTask;
			}
			await Task.Delay(1000);
		}
	}

	internal class Program
	{
		static async void Main(string[] args)
		{
			var myClass = new MyClass();

			if (myClass is IAsyncInit ai)
			{
				await ai.InitTask;
			}

			var oc = new MyOtherClass(myClass);
			await oc.InitTask;
		}
	}
}
