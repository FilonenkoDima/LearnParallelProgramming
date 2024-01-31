﻿using Nito.AsyncEx;

namespace AsynchronousLazyInitialization
{
	internal class Program
	{
		public class Stuff
		{
			private static int value;
			private readonly Lazy<Task<int>> AutoIncValue =
				new Lazy<Task<int>>(async () =>
				{
					await Task.Delay(1000).ConfigureAwait(false);
					return value++;
				});


			private readonly Lazy<Task<int>> AutoIncValue2 =
				new Lazy<Task<int>>(() => Task.Run(async () =>
				{
					await Task.Delay(1000).ConfigureAwait(false);
					return value++;
				}));

			// Nito.AsyncEx
			public AsyncLazy<int> AutoIncValue3
				= new AsyncLazy<int>(async () =>
				{
					await Task.Delay(1000).ConfigureAwait(false);
					return value++;
				});

			public async Task UseValue()
			{
				int value = await AutoIncValue.Value;
			}

		}

		static async Task Main(string[] args)
		{

		}
	}
}
