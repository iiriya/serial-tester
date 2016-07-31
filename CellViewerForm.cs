//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="CellViewerForm.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the CellViewerForm form.</summary>
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
    /// A form used as viewer for the contents data grid cells.
    /// </summary>
    public sealed class CellViewerForm : Form
    {
        #region CellViewerForm Fields
        /// <summary>
        /// The main <see cref="System.System.Windows.Forms.TextBox">TextBox</see>.
        /// </summary>
        private TextBox viewerTextBox;
        #endregion

        #region CellViewerForm Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.CellViewerForm">CellViewerForm</see> class.
        /// </summary>
        public CellViewerForm() : base()
        {
            this.InitializeComponent();
        }
        #endregion

        #region CellViewerForm Properties
        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The content of the cell.</value>
        public string Value
        {
            get
            {
                return this.viewerTextBox.Text;
            }

            set
            {
                this.viewerTextBox.Text = value;
            }
        }
        #endregion

        #region CellViewerForm Methods
        /// <summary>
        /// Initializes the form components.
        /// </summary>
        private void InitializeComponent()
        {
            this.viewerTextBox = new TextBox();
            this.SuspendLayout();

            this.viewerTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.viewerTextBox.BackColor = Color.White;
            this.viewerTextBox.BorderStyle = BorderStyle.None;
            this.viewerTextBox.Location = new Point(13, 13);
            this.viewerTextBox.Multiline = true;
            this.viewerTextBox.Name = "viewerTextBox";
            this.viewerTextBox.ReadOnly = true;
            this.viewerTextBox.Size = new Size(571, 462);
            this.viewerTextBox.ScrollBars = ScrollBars.Both;
            this.viewerTextBox.TabIndex = 0;
            this.Controls.Add(this.viewerTextBox);

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(596, 487);
            this.Name = "CellViewerForm";
            this.Text = "Viewer";
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}