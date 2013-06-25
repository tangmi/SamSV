using ImageMagick;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamSV
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private Dictionary<String, Card> cards;
		private string selectedCard = "";
        private void Form1_Load(object sender, EventArgs e)
        {
			cards = new Dictionary<String, Card>();

			
			string dir = @".\art";

			if (Directory.Exists(dir))
			{
				string[] files = Directory.GetFiles(dir);

				for (int i = 0; i < files.Length; i++)
				{
					string path = files[i];

					if (path.EndsWith(".psd"))
					{
						Card card = new Card(new CardInfo(path));
						string artpath = path.Substring(0, path.Length - 4);
						if (File.Exists(artpath + ".txt"))
						{
							string[] lines = File.ReadAllLines(artpath + ".txt");
							for (int j = 0; j < lines.Length; j++)
							{
								string[] parts = lines[j].Split('=');
								string key = parts[0].Trim();
								string value = parts[1].Trim();

								switch (key.ToLower())
								{
									case "title": card.Info.Title = value; 
										break;
									case "type": card.Info.Type = value;
										break;
									case "flavortext": card.Info.FlavorText = value;
										break;
									case "description": card.Info.Description = value;
										break;
									case "heat": card.Info.Heat = int.Parse(value);
										break;
									case "cost": card.Info.Cost = int.Parse(value);
										break;
								}
							}
						}
						else
						{

							string[] lines = {
								"title=" + artpath,
								"type=",
								"flavortext=",
								"description=",
								"heat=0",
								"cost=0"
							};
							File.WriteAllLines(artpath + ".txt", lines);
						}
						cards.Add(card.Info.Title, card);
					}
				}

			}
			



			//set up list
			// Set the view to show details.
			this.listView.View = View.Details;

			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.Sorting = SortOrder.Ascending;
			this.listView.MultiSelect = false;

			// Create columns for the items and subitems.
			this.listView.Columns.Add("Title", 150, HorizontalAlignment.Left);
			this.listView.Columns.Add("Type", 100, HorizontalAlignment.Left);
			this.listView.Columns.Add("Flavor Text", 100, HorizontalAlignment.Left);
			this.listView.Columns.Add("Cost", 40, HorizontalAlignment.Center);
			this.listView.Columns.Add("Heat", 40, HorizontalAlignment.Center);
			this.listView.Columns.Add("Description", 500, HorizontalAlignment.Left);

			foreach (KeyValuePair<String, Card> etr in cards)
			{
				Card item = etr.Value;
				ListViewItem listItem = new ListViewItem(item.Info.Title);
				listItem.SubItems.Add(item.Info.Type);
				listItem.SubItems.Add(item.Info.FlavorText);
				listItem.SubItems.Add(item.Info.Cost.ToString());
				listItem.SubItems.Add(item.Info.Heat.ToString());
				listItem.SubItems.Add(item.Info.Description);

				this.listView.Items.Add(listItem);
			}

			this.listView.Refresh();

			

			CardCreator.LoadTemplateImage("CardFront.psd");


        }

        

		private void updateCardTextBoxes(CardInfo cardInfo)
		{
			this.cardArtLocation.Text = cardInfo.ArtLocation;
			this.cardTitle.Text = cardInfo.Title;
			this.cardType.Text = cardInfo.Type;
			this.cardDescription.Text = cardInfo.Description;
			this.cardFlavorText.Text = cardInfo.FlavorText;
			this.cardHeat.Value = cardInfo.Heat;
			this.cardCost.Value = cardInfo.Cost;
			this.cardBox.Refresh();
		}

		private Card selectCardByTitle(string title)
		{
			Card card;
			cards.TryGetValue(title, out card);
			this.selectedCard = title;
			return card;
		}

		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			string item = null;
			foreach (ListViewItem i in this.listView.SelectedItems)
			{
				item = i.Text;
				break;
			}
			if (item != null)
			{
				Card card = this.selectCardByTitle(item);


				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				this.UpdatePreview(card);
				


				stopwatch.Stop();


				this.statusText.Text = "Rendered in " + stopwatch.Elapsed;
				this.statusStrip.Refresh();

				stopwatch.Reset();

				//card.Render();
			}
		}

		private void cardSave_Click(object sender, EventArgs e)
		{
			Card card = this.selectCardByTitle(this.selectedCard);

			card.Info.Title = this.cardTitle.Text;
			card.Info.Type = this.cardType.Text;
			card.Info.FlavorText = this.cardFlavorText.Text;
			card.Info.Description = this.cardDescription.Text;
			card.Info.Heat = int.Parse(this.cardHeat.Text);
			card.Info.Cost = int.Parse(this.cardCost.Text);

			string[] lines = {
								"title=" + card.Info.Title,
								"type=" + card.Info.Type,
								"flavortext=" + card.Info.FlavorText,
								"description=" + card.Info.Description,
								"heat=" + card.Info.Heat,
								"cost=" + card.Info.Cost
							};
			string artpath = card.Info.ArtLocation.Substring(0, card.Info.ArtLocation.Length - 4);
			File.WriteAllLines(artpath + ".txt", lines);

			card.RegenerateImage();
			this.pictureBox.Image = card.GetPreview();
			this.pictureBox.Refresh();

			ListViewItem item = this.listView.SelectedItems[0];
			item.Text = card.Info.Title;
			item.SubItems[0].Text = card.Info.Title;
			item.SubItems[1].Text = card.Info.Type;
			item.SubItems[2].Text = card.Info.FlavorText;
			item.SubItems[3].Text = card.Info.Cost.ToString();
			item.SubItems[4].Text = card.Info.Heat.ToString();
			item.SubItems[5].Text = card.Info.Description;
			this.listView.Refresh();
		}


		private void renderButton_Click(object sender, EventArgs e)
		{

			this.progressBar.Value = 0;
			this.progressBar.Style = ProgressBarStyle.Marquee;
			this.progressBar.Refresh();

			this.progressBar.Maximum = cards.Keys.ToArray().Length;
			this.progressBar.Step = 1;


			/*
			Thread renderThread = new Thread(this.RenderAll);
			renderThread.Start();
			 */

			foreach (KeyValuePair<String, Card> etr in cards)
			{
				CardCreator.isDraftRun = false;
				etr.Value.Render();

				this.UpdatePreview(etr.Value);

				this.progressBar.Style = ProgressBarStyle.Continuous;
				this.progressBar.PerformStep();
				this.progressBar.Refresh();
			}

			
		}

		private void UpdatePreview(Card card)
		{
			this.updateCardTextBoxes(card.Info);
			this.pictureBox.Image = card.GetPreview();
			this.pictureBox.Refresh();
		}

		private void renderSelectedButton_Click(object sender, EventArgs e)
		{
			CardCreator.isDraftRun = false;
			Card card = this.selectCardByTitle(this.listView.SelectedItems[0].Text);
			card.Render();
			this.UpdatePreview(card);
		}



		
    }
}
