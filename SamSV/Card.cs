using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSV
{
	class Card
	{
		public MagickImage Image { get; set; }
		public CardInfo Info { get; set; }
		private Bitmap CachedPreview;

		public Card(CardInfo info)
		{
			this.Image = null;
			this.Info = info;
			this.CachedPreview = null;
		}

		public Bitmap GetPreview()
		{
			if (this.Image == null)
			{
				this.Image = CardCreator.GetImage(this);
				return this.GetPreview();
			}
			else
			{
				if (this.CachedPreview == null)
				{
					using (MagickImage temp = this.Image.Copy())
					{
						temp.Resize(CardCreator.DraftGeometry);
						return temp.ToBitmap();
					}
				}
				else
				{
					return this.CachedPreview;
				}
			}
		}

		public void RegenerateImage()
		{
			this.Image = CardCreator.GetImage(this);
		}

		public void Render()
		{
			if (this.Image == null)
			{
				this.Image = CardCreator.GetImage(this);
				this.Render();
			}
			else
			{
				string cardPath = @".\rendered\" + this.Info.Title.Replace('\\', ' ') + ".png";
				if (CardCreator.isDraftRun)
				{
					//resize, then convert
					Image.Resize(CardCreator.DraftGeometry);
					Image.Write(cardPath);
				}
				else
				{
					//convert, then resize
					Image.Write(cardPath);
				}
			}
			
		}
	}
}
