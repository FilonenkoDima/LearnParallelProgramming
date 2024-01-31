using System.Net;

namespace UsingAsyncAndAwait
{
	public partial class Form1 : Form
	{
		public int CalculateValue()
		{
			Thread.Sleep(5000);
			return 123;
		}
		public Task<int> CalculateValueAsync()
		{
			return Task.Factory.StartNew(() =>
			{
				Thread.Sleep(5000);
				return 123;
			});
		}
		public async Task<int> CalculateValueWithoutThreadAsync()
		{
			await Task.Delay(5000);
			return 123;
		}
		public Form1()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private async void BtnCalculate_Click(object sender, EventArgs e)
		{
			/// CalculateValue

			//int n = CalculateValue();
			//LblResult.Text = n.ToString();



			/// CalculateValueAsync

			//var calculation = CalculateValueAsync();
			//calculation.ContinueWith(t =>
			//{
			//	LblResult.Text = t.Result.ToString();
			//}, TaskScheduler.FromCurrentSynchronizationContext());



			/// CalculateValueWithoutThreadAsync

			int value = await CalculateValueWithoutThreadAsync();
			LblResult.Text = value.ToString();

			await Task.Delay(5000);

			using (var wc = new WebClient())
			{
				string data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
				LblResult.Text = data.Split('\n')[0].Trim();
			}
		}
	}
}
