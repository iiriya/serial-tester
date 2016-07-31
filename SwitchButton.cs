//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SwitchButton.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the SwitchButton button control.</summary>
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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// Provides a controllable <see cref="System.Windows.Forms.Button">Button</see>.
    /// </summary>
    public class SwitchButton : Button
    {
        #region SwitchButton Fields
        /// <summary>
        /// A value indicating whether the switch is On or Off.
        /// </summary>
        private bool switchValue = false;

        /// <summary>
        /// The <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is On.
        /// </summary>
        private string switchOnText = string.Empty;

        /// <summary>
        /// The <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is Off.
        /// </summary>
        private string switchOffText = string.Empty;

        /// <summary>
        /// The <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is On.
        /// </summary>
        private Color switchOnColor = Color.Red;

        /// <summary>
        /// The <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is Off.
        /// </summary>
        private Color switchOffColor = Color.Lime;
        #endregion

        #region SwitchButton Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.SwitchButton">SwitchButton</see> class.
        /// </summary>
        public SwitchButton() : base()
        {
        }
        #endregion

        #region SwitchButton Properties
        /// <summary>
        /// Gets or sets a value indicating whether the switch is On or Off.
        /// </summary>
        /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the switch is On; otherwise, <see cref="System.Boolean.False">False</see>.</value>
        public bool SwitchValue
        {
            get
            {
                return this.switchValue;
            }

            set
            {
                if (value)
                {
                    this.BackColor = this.SwitchOnColor;
                    this.Text = this.SwitchOnText;
                }
                else
                {
                    this.BackColor = this.SwitchOffColor;
                    this.Text = this.SwitchOffText;
                }

                this.Update();
                this.switchValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is On.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is On.</value>
        [DisplayName("SwitchOnText"), SettingsBindable(true)]
        public virtual string SwitchOnText
        {
            get
            {
                return this.switchOnText;
            }

            set
            {
                this.switchOnText = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is On.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The <see cref="System.Windows.Forms.Button.Text">Text</see> displayed when the switch is On.</value>
        [DisplayName("SwitchOffText"), SettingsBindable(true)]
        public virtual string SwitchOffText
        {
            get
            {
                return this.switchOffText;
            }

            set
            {
                this.switchOffText = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is Off.
        /// </summary>
        /// <value>Type: <see cref="System.Drawing.Color">Color</see>. The <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is Off.</value>
        [DisplayName("SwitchOnColor"), SettingsBindable(true)]
        public virtual Color SwitchOnColor
        {
            get
            {
                return this.switchOnColor;
            }

            set
            {
                this.switchOnColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is On.
        /// </summary>
        /// <value>Type: <see cref="System.Drawing.Color">Color</see>. The <see cref="System.Windows.Forms.Button.BackColor">BackColor</see> displayed when the switch is On.</value>
        [DisplayName("SwitchOffColor"), SettingsBindable(true)]
        public virtual Color SwitchOffColor
        {
            get
            {
                return this.switchOffColor;
            }

            set
            {
                this.switchOffColor = value;
            }
        }
        #endregion
    }
}