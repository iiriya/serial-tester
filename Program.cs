//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the main Program class.</summary>
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
    using System;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// The main Program class.
    /// </summary>
    public static class Program
    {
        #region Program Fields
        /// <summary>
        /// A list of serial ports.
        /// </summary>
        private static SpiList ports;
        #endregion

        #region Program Properties
        /// <summary>
        /// Gets a list of serial ports.
        /// </summary>
        public static SpiList Ports
        {
            get
            {
                if (ports == null)
                {
                    ports = new SpiList();
                    ports.Refresh();
                }

                return ports;
            }
        }
        #endregion

        #region Program Methods
        /// <summary>
        /// The main application entry-point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += delegate(object sender, EventArgs e)
            {
                if (ports != null)
                {
                    ports.Dispose();
                }
            };

            using (MainForm form = new MainForm())
            {
                Application.Run(form);
            }
        }
        #endregion
    }
}