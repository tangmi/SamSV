using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSV
{
	class CardCreator
	{

		private static MagickImage TEMPLATE = null;
		public static bool isDraftRun = true;

		private const int DRAFT_WIDTH = 300;
		private const int DRAFT_HEIGHT = 418;

		public static MagickGeometry DraftGeometry;

		public static void LoadTemplateImage(string path)
		{
			TEMPLATE = new MagickImage("CardFront.psd");
			DraftGeometry = new MagickGeometry(DRAFT_WIDTH, DRAFT_HEIGHT);
		}

		public static Card CreateCard(CardInfo cardInfo)
		{
			Card card = new Card(cardInfo);
			card.Image = CardCreator.GetImage(card);
			return card;
		}

		public static MagickImage GetImage(Card card)
		{
			if (TEMPLATE == null)
			{
				throw new InvalidOperationException("CardCreator has not loaded a template file yet.");
			}

			MagickImage image = TEMPLATE.Copy();

			using (MagickImage art = new MagickImage(card.Info.ArtLocation))
			{
				MagickGeometry artGeometry = new MagickGeometry(196, 412, 2100, 1537);
				artGeometry.Aspect = true; //don't constrain proportions when resizing images

				art.Resize(artGeometry);
				image.Composite(art, artGeometry);
			}


			MagickGeometry titleGeometry = new MagickGeometry(168, 160, 2154, 189);
			MagickImage title = CardCreator.GenerateTextLineImage(titleGeometry, card.Info.Title);
			image.Composite(title, titleGeometry, CompositeOperator.Multiply);

			MagickGeometry typeGeometry = new MagickGeometry(220, 1980, 2074, 160);
			MagickImage type = CardCreator.GenerateTextLineImage(typeGeometry, card.Info.Type);
			image.Composite(type, typeGeometry, CompositeOperator.Multiply);

			MagickGeometry descriptionGeometry = new MagickGeometry(205, 2187, 2101, 662);
			MagickImage description = CardCreator.GenerateTextBlockImage(descriptionGeometry, card.Info.Description);
			image.Composite(description, descriptionGeometry, CompositeOperator.Multiply);



			MagickGeometry moneyGeometry = new MagickGeometry(135, 2864, 360, 360);
			MagickImage money = CardCreator.GenerateStatImage(moneyGeometry, card.Info.Cost.ToString());
			image.Composite(money, moneyGeometry, CompositeOperator.Multiply);


			MagickGeometry fireGeometry = new MagickGeometry(2005, 2864, 360, 360);
			MagickImage fire = CardCreator.GenerateStatImage(fireGeometry, card.Info.Heat.ToString());
			image.Composite(fire, fireGeometry, CompositeOperator.Multiply);

			return image;

		}

		private static MagickImage GenerateTextImage(MagickGeometry geometry, string text, Gravity gravity)
		{
			MagickImage image = new MagickImage(Color.Transparent, geometry.Width, geometry.Height);

			if (text != "")
			{
				image.FillColor = Color.Black;
				image.Font = "Arial";
				image.Density = new MagickGeometry(72, 72);

				image.FontPointsize = 1;
				TypeMetric typeMetric = image.FontTypeMetrics(text);
				while (typeMetric.TextWidth < image.Width && typeMetric.TextHeight < image.Height)
				{
					image.FontPointsize++;
					typeMetric = image.FontTypeMetrics(text);
				}
				image.FontPointsize--;
				image.Annotate(text, gravity);
			}
			return image;
		}

		private static MagickImage GenerateTextLineImage(MagickGeometry geometry, string text)
		{
			return GenerateTextImage(geometry, text, Gravity.West);

		}

		private static MagickImage GenerateStatImage(MagickGeometry geometry, string text)
		{
			return GenerateTextImage(geometry, text, Gravity.Center);

		}

		private static MagickImage GenerateTextBlockImage(MagickGeometry geometry, string text)
		{
			MagickImage image = new MagickImage(Color.Transparent, geometry.Width, geometry.Height);
			if (text != "")
			{

				image.FillColor = Color.Black;
				image.Font = "Arial";
				image.Density = new MagickGeometry(72, 72);
				image.FontPointsize = geometry.Height / 6;


				TypeMetric typeMetric = image.FontTypeMetrics(text);
				string[] words = text.Split(' ');


				string[] lines = new string[words.Length];
				int lineIndex = 0;
				for (int index = 0; index < words.Length; index++)
				{
					string word = words[index];

					TypeMetric metric = image.FontTypeMetrics(lines[lineIndex] + " " + word);

					if (metric.TextWidth > geometry.Width)
					{
						lineIndex++;
						index--;
					}
					else
					{
						lines[lineIndex] += " " + word;
					}

				}

				string output = String.Join("\n", lines);

				/*
				TypeMetric typeMetric = image.FontTypeMetrics(text);
				while (typeMetric.TextWidth < image.Width && typeMetric.TextHeight < image.Height)
				{
					image.FontPointsize++;
					typeMetric = image.FontTypeMetrics(text);
				}
				image.FontPointsize--;
				 */

				image.Annotate(output, Gravity.Northwest);
			}
			return image;
		}

	}
}
