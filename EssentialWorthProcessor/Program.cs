using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace EssentialWorthProcessor
{
	class Program
	{
		private static readonly string[] WoodTypes = new string[]
			{
				"oak",
				"birch",
				"spruce",
				"jungle",
				"darkoak",
				"acacia"
			};

		private static readonly string[] Colors = new string[]
			{
				"white",
				"orange",
				"magenta",
				"lightblue",
				"yellow",
				"lime",
				"pink",
				"gray",
				"lightgray",
				"cyan",
				"purple",
				"brown",
				"green",
				"red",
				"black"
			};

		static void Main(string[] args)
		{
			Console.WriteLine("Specify CSV file: ");
			string csvFile = Console.ReadLine().Replace("\"", "");

			var fileLines = File.ReadAllLines(csvFile);
			var outputText = new List<string>();

			// parse all lines
			foreach (var line in fileLines)
			{
				var item = line.Split(',')[0];
				string priceString = string.Join("", line.Split(',').Skip(1)).Replace("\"", "").Replace("$", "");
				float price = float.Parse(priceString);

				// check for wood macro replacement
				if (item.Contains("[wood]"))
				{
					foreach (var woodType in WoodTypes)
					{
						writeEntry(item.Replace("[wood]", woodType), price);
					}

					continue;
				}

				// check for color macro replacement
				if (item.Contains("[color]"))
				{
					foreach (var color in Colors)
					{
						writeEntry(item.Replace("[color]", color), price);
					}

					continue;
				}

				// otherwise write file normally
				writeEntry(item, price);
			}

			void writeEntry(string itemName, float price)
			{
				outputText.Add($"  {itemName}: {price.ToString("g")}");
			}

			string outputPath = Path.Combine(Environment.CurrentDirectory + "/worth.yml");
			File.WriteAllLines(outputPath, outputText);
			Console.WriteLine($"Done! Written to {outputPath}");
		}
	}
}
