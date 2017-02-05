using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace DataGrabber
{
    class Program
    {
	   static void Main(string[] args)
	   {
		  Task t = new Task(DownloadPageAsync);
		  t.Start();
		  Console.WriteLine("Downloading page..");
		  Console.ReadLine();
	   }

	   static async void DownloadPageAsync()
	   {
		  string page = "http://www.momox.de/buch-ankauf/xxx/9780596009205";

		  using (HttpClient client = new HttpClient())
		  using (HttpResponseMessage response = await client.GetAsync(page))
		  using (HttpContent content = response.Content)
		  {
			 string result = await content.ReadAsStringAsync();
			 string path = @"c:\temp\MyTest.txt";
			 HtmlDocument doc = new HtmlDocument();
			 doc.LoadHtml(result);
			 string price = doc.GetElementbyId("offerDefault").InnerHtml;
			 doc.LoadHtml(price);
			 var value = doc.DocumentNode.SelectNodes("//div[@class='offerPrice']")
				.Select(x => x.InnerText)
				.ToList();

			 if (!File.Exists(path))
			 {
				// Create a file to write to.
				string createText = result;
				File.WriteAllText(path, createText);
			 }



			 Console.WriteLine(result);
		  }
	   }
    }
}
