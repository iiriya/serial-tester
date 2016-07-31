//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpForm.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the help form.</summary>
// <remarks>
//      Iiriya's Serial Tester.
//      
//      This project contains a Serial Ports Tester.
//      
//      This solution and all its components its owned by Lucio Benini and his companies.
//      This solution is designed in C# for Microsoft .NET Framework and works only under the Windows Operative System.
//      For Mono or other platforms integrations contact the main project owner.
//      The designer and his companies aren't responsible for any damage due to unauthorized installations or usages.
//      
//      This project is created and designed under Microsoft .NET 3.5 environment and runtime and requires the runtime version 3.5 or later.
//      
//      This project is licensed. Copyright © Lucio Benini 2016. All Rights Reserved.
// </remarks>
//-----------------------------------------------------------------------------------------------------------------------

namespace Iiriya.Apps.SerialTester
{
    #region Using Directives
    using System.Drawing;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// The help form.
    /// </summary>
    public class HelpForm : Form
    {
        #region HelpForm Fields
        /// <summary>
        /// The application name label.
        /// </summary>
        private Label appLabel;

        /// <summary>
        /// The copyright label.
        /// </summary>
        private Label copyrightLabel;

        /// <summary>
        /// The license label.
        /// </summary>
        private Label licenseLabel;

        /// <summary>
        /// The author's site label.
        /// </summary>
        private LinkLabel linkLabel;

        /// <summary>
        /// The description label.
        /// </summary>
        private Label descriptionLabel;
        #endregion

        #region HelpForm Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.HelpForm">HelpForm</see> class.
        /// </summary>
        public HelpForm()
        {
            this.InitializeComponent();
        }
        #endregion

        #region HelpForm Methods
        /// <summary>
        /// Initializes the visual components.
        /// </summary>
        private void InitializeComponent()
        {
            this.appLabel = new Label();
            this.copyrightLabel = new Label();
            this.licenseLabel = new Label();
            this.linkLabel = new LinkLabel();
            this.descriptionLabel = new Label();
            this.SuspendLayout();

            this.appLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.appLabel.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.appLabel.Location = new Point(12, 9);
            this.appLabel.Name = "appLabel";
            this.appLabel.Size = new Size(340, 35);
            this.appLabel.TabIndex = 0;
            this.appLabel.Text = "SerialTester";
            this.appLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.copyrightLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.copyrightLabel.Location = new Point(15, 80);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new Size(345, 23);
            this.copyrightLabel.TabIndex = 1;
            this.copyrightLabel.Text = "© 2016 Lucio Benini";
            this.copyrightLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.licenseLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.licenseLabel.Location = new Point(12, 146);
            this.licenseLabel.Name = "licenseLabel";
            this.licenseLabel.Size = new Size(348, 26);
            this.licenseLabel.TabIndex = 2;
            this.licenseLabel.Text = "This project is licensed under the GNU GPLv3";
            this.licenseLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.linkLabel.Location = new Point(15, 103);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new Size(345, 32);
            this.linkLabel.TabIndex = 3;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "http://iiriya.com";
            this.linkLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.descriptionLabel.Location = new Point(18, 44);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new Size(334, 25);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "A Simple COM Ports Tester";
            this.descriptionLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(364, 181);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.licenseLabel);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.appLabel);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new Size(380, 220);
            this.MinimumSize = new Size(380, 220);
            this.Name = "HelpForm";
            this.ShowIcon = false;
            this.Text = "Credits";
            this.ResumeLayout(false);
        }
        #endregion
    }
}