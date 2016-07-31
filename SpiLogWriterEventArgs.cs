//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SpiLogWriterEventArgs.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains an EventArgs object designed for Spi logs.</summary>
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
    #endregion

    /// <summary>
    /// An <see cref="System.EventArgs">EventArgs</see> object designed for <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> logs.
    /// </summary>
    public class SpiLogWriterEventArgs : EventArgs
    {
        #region SpiLogWriterEventArgs Fields
        /// <summary>
        /// The source port.
        /// </summary>
        private string source = string.Empty;

        /// <summary>
        /// The log date.
        /// </summary>
        private DateTime date = DateTime.Now;

        /// <summary>
        /// The log entry.
        /// </summary>
        private string entry = string.Empty;

        /// <summary>
        /// The port direction.
        /// </summary>
        private PortDirection direction = PortDirection.In;
        #endregion

        #region SpiLogWriterEventArgs Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.SpiLogWriterEventArgs">SpiLogWriterEventArgs</see> class.
        /// </summary>
        public SpiLogWriterEventArgs() : base()
        {
        }
        #endregion

        #region SpiLogWriterEventArgs Properties
        /// <summary>
        /// Gets or sets the source port.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The source port.</value>
        public string Source
        {
            get
            {
                return this.source;
            }

            set
            {
                this.source = value;
            }
        }

        /// <summary>
        /// Gets or sets the log date.
        /// </summary>
        /// <value>Type: <see cref="System.DateTime">DateTime</see>. The log date.</value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        /// <summary>
        /// Gets or sets the log entry.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The log entry.</value>
        public string Entry
        {
            get
            {
                return this.entry;
            }

            set
            {
                this.entry = value;
            }
        }

        /// <summary>
        /// Gets or sets the port direction.
        /// </summary>
        /// <value>Type: <see cref="Iiriya.Apps.SerialTester.PortDirection">PortDirection</see>. <see cref="Iiriya.Apps.SerialTester.PortDirection.In">In</see> for inbound data; <see cref="Iiriya.Apps.SerialTester.PortDirection.Out">Out</see> for outbound data.</value>
        public PortDirection Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }
        #endregion
    }
}