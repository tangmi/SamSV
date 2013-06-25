namespace SamSV
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.cardSave = new System.Windows.Forms.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.cardTitle = new System.Windows.Forms.TextBox();
			this.cardType = new System.Windows.Forms.TextBox();
			this.cardDescription = new System.Windows.Forms.TextBox();
			this.cardHeat = new System.Windows.Forms.NumericUpDown();
			this.cardCost = new System.Windows.Forms.NumericUpDown();
			this.cardTitleLabel = new System.Windows.Forms.Label();
			this.cardTypeLabel = new System.Windows.Forms.Label();
			this.cardDescriptionLabel = new System.Windows.Forms.Label();
			this.cardHeatLabel = new System.Windows.Forms.Label();
			this.cardCostLabel = new System.Windows.Forms.Label();
			this.cardFlavorTextLabel = new System.Windows.Forms.Label();
			this.cardFlavorText = new System.Windows.Forms.TextBox();
			this.cardBox = new System.Windows.Forms.GroupBox();
			this.cardArtLocation = new System.Windows.Forms.Label();
			this.cardArtLocationLabel = new System.Windows.Forms.Label();
			this.renderButton = new System.Windows.Forms.Button();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cardHeat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cardCost)).BeginInit();
			this.cardBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
			this.statusStrip.Location = new System.Drawing.Point(0, 693);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1016, 22);
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusText
			// 
			this.statusText.Name = "statusText";
			this.statusText.Size = new System.Drawing.Size(118, 17);
			this.statusText.Text = "toolStripStatusLabel1";
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(300, 418);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(333, 436);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(251, 10);
			this.progressBar.TabIndex = 2;
			// 
			// cardSave
			// 
			this.cardSave.Location = new System.Drawing.Point(190, 215);
			this.cardSave.Name = "cardSave";
			this.cardSave.Size = new System.Drawing.Size(104, 23);
			this.cardSave.TabIndex = 11;
			this.cardSave.Text = "Save/Preview";
			this.cardSave.UseVisualStyleBackColor = true;
			this.cardSave.Click += new System.EventHandler(this.cardSave_Click);
			// 
			// listView
			// 
			this.listView.Location = new System.Drawing.Point(318, 12);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(441, 359);
			this.listView.TabIndex = 4;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			// 
			// cardTitle
			// 
			this.cardTitle.Location = new System.Drawing.Point(72, 38);
			this.cardTitle.Name = "cardTitle";
			this.cardTitle.Size = new System.Drawing.Size(222, 20);
			this.cardTitle.TabIndex = 5;
			// 
			// cardType
			// 
			this.cardType.Location = new System.Drawing.Point(72, 64);
			this.cardType.Name = "cardType";
			this.cardType.Size = new System.Drawing.Size(222, 20);
			this.cardType.TabIndex = 6;
			// 
			// cardDescription
			// 
			this.cardDescription.Location = new System.Drawing.Point(72, 90);
			this.cardDescription.Multiline = true;
			this.cardDescription.Name = "cardDescription";
			this.cardDescription.Size = new System.Drawing.Size(222, 67);
			this.cardDescription.TabIndex = 7;
			// 
			// cardHeat
			// 
			this.cardHeat.Location = new System.Drawing.Point(72, 215);
			this.cardHeat.Name = "cardHeat";
			this.cardHeat.Size = new System.Drawing.Size(86, 20);
			this.cardHeat.TabIndex = 10;
			// 
			// cardCost
			// 
			this.cardCost.Location = new System.Drawing.Point(72, 189);
			this.cardCost.Name = "cardCost";
			this.cardCost.Size = new System.Drawing.Size(86, 20);
			this.cardCost.TabIndex = 9;
			// 
			// cardTitleLabel
			// 
			this.cardTitleLabel.AutoSize = true;
			this.cardTitleLabel.Location = new System.Drawing.Point(6, 41);
			this.cardTitleLabel.Name = "cardTitleLabel";
			this.cardTitleLabel.Size = new System.Drawing.Size(27, 13);
			this.cardTitleLabel.TabIndex = 10;
			this.cardTitleLabel.Text = "Title";
			// 
			// cardTypeLabel
			// 
			this.cardTypeLabel.AutoSize = true;
			this.cardTypeLabel.Location = new System.Drawing.Point(6, 67);
			this.cardTypeLabel.Name = "cardTypeLabel";
			this.cardTypeLabel.Size = new System.Drawing.Size(31, 13);
			this.cardTypeLabel.TabIndex = 11;
			this.cardTypeLabel.Text = "Type";
			// 
			// cardDescriptionLabel
			// 
			this.cardDescriptionLabel.AutoSize = true;
			this.cardDescriptionLabel.Location = new System.Drawing.Point(6, 93);
			this.cardDescriptionLabel.Name = "cardDescriptionLabel";
			this.cardDescriptionLabel.Size = new System.Drawing.Size(60, 13);
			this.cardDescriptionLabel.TabIndex = 12;
			this.cardDescriptionLabel.Text = "Description";
			// 
			// cardHeatLabel
			// 
			this.cardHeatLabel.AutoSize = true;
			this.cardHeatLabel.Location = new System.Drawing.Point(6, 217);
			this.cardHeatLabel.Name = "cardHeatLabel";
			this.cardHeatLabel.Size = new System.Drawing.Size(30, 13);
			this.cardHeatLabel.TabIndex = 13;
			this.cardHeatLabel.Text = "Heat";
			// 
			// cardCostLabel
			// 
			this.cardCostLabel.AutoSize = true;
			this.cardCostLabel.Location = new System.Drawing.Point(6, 191);
			this.cardCostLabel.Name = "cardCostLabel";
			this.cardCostLabel.Size = new System.Drawing.Size(28, 13);
			this.cardCostLabel.TabIndex = 14;
			this.cardCostLabel.Text = "Cost";
			// 
			// cardFlavorTextLabel
			// 
			this.cardFlavorTextLabel.AutoSize = true;
			this.cardFlavorTextLabel.Location = new System.Drawing.Point(6, 166);
			this.cardFlavorTextLabel.Name = "cardFlavorTextLabel";
			this.cardFlavorTextLabel.Size = new System.Drawing.Size(56, 13);
			this.cardFlavorTextLabel.TabIndex = 15;
			this.cardFlavorTextLabel.Text = "Flavor text";
			// 
			// cardFlavorText
			// 
			this.cardFlavorText.Location = new System.Drawing.Point(72, 163);
			this.cardFlavorText.Name = "cardFlavorText";
			this.cardFlavorText.Size = new System.Drawing.Size(222, 20);
			this.cardFlavorText.TabIndex = 8;
			// 
			// cardBox
			// 
			this.cardBox.Controls.Add(this.cardArtLocation);
			this.cardBox.Controls.Add(this.cardArtLocationLabel);
			this.cardBox.Controls.Add(this.cardTitleLabel);
			this.cardBox.Controls.Add(this.cardCostLabel);
			this.cardBox.Controls.Add(this.cardFlavorTextLabel);
			this.cardBox.Controls.Add(this.cardSave);
			this.cardBox.Controls.Add(this.cardCost);
			this.cardBox.Controls.Add(this.cardHeatLabel);
			this.cardBox.Controls.Add(this.cardHeat);
			this.cardBox.Controls.Add(this.cardFlavorText);
			this.cardBox.Controls.Add(this.cardTitle);
			this.cardBox.Controls.Add(this.cardType);
			this.cardBox.Controls.Add(this.cardTypeLabel);
			this.cardBox.Controls.Add(this.cardDescription);
			this.cardBox.Controls.Add(this.cardDescriptionLabel);
			this.cardBox.Location = new System.Drawing.Point(12, 436);
			this.cardBox.Name = "cardBox";
			this.cardBox.Size = new System.Drawing.Size(300, 244);
			this.cardBox.TabIndex = 17;
			this.cardBox.TabStop = false;
			this.cardBox.Text = "Card Text";
			// 
			// cardArtLocation
			// 
			this.cardArtLocation.AutoSize = true;
			this.cardArtLocation.Location = new System.Drawing.Point(69, 16);
			this.cardArtLocation.Name = "cardArtLocation";
			this.cardArtLocation.Size = new System.Drawing.Size(92, 13);
			this.cardArtLocation.TabIndex = 18;
			this.cardArtLocation.Text = "(no card selected)";
			// 
			// cardArtLocationLabel
			// 
			this.cardArtLocationLabel.AutoSize = true;
			this.cardArtLocationLabel.Location = new System.Drawing.Point(6, 16);
			this.cardArtLocationLabel.Name = "cardArtLocationLabel";
			this.cardArtLocationLabel.Size = new System.Drawing.Size(20, 13);
			this.cardArtLocationLabel.TabIndex = 17;
			this.cardArtLocationLabel.Text = "Art";
			// 
			// renderButton
			// 
			this.renderButton.Location = new System.Drawing.Point(601, 404);
			this.renderButton.Name = "renderButton";
			this.renderButton.Size = new System.Drawing.Size(121, 90);
			this.renderButton.TabIndex = 18;
			this.renderButton.Text = "Render All";
			this.renderButton.UseVisualStyleBackColor = true;
			this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1016, 715);
			this.Controls.Add(this.renderButton);
			this.Controls.Add(this.cardBox);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.pictureBox);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "MainWindow";
			this.Text = "Sam\'s Silent Vacuum";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cardHeat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cardCost)).EndInit();
			this.cardBox.ResumeLayout(false);
			this.cardBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button cardSave;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.TextBox cardTitle;
		private System.Windows.Forms.TextBox cardType;
		private System.Windows.Forms.TextBox cardDescription;
		private System.Windows.Forms.NumericUpDown cardHeat;
		private System.Windows.Forms.NumericUpDown cardCost;
		private System.Windows.Forms.Label cardTitleLabel;
		private System.Windows.Forms.Label cardTypeLabel;
		private System.Windows.Forms.Label cardDescriptionLabel;
		private System.Windows.Forms.Label cardHeatLabel;
		private System.Windows.Forms.Label cardCostLabel;
		private System.Windows.Forms.Label cardFlavorTextLabel;
		private System.Windows.Forms.TextBox cardFlavorText;
		private System.Windows.Forms.GroupBox cardBox;
		private System.Windows.Forms.Label cardArtLocation;
		private System.Windows.Forms.Label cardArtLocationLabel;
		private System.Windows.Forms.Button renderButton;
    }
}

