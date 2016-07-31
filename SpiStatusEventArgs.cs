//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SpiStatusEventArgs.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the SpiStatusEventArgs class.</summary>
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
    /// Provides classes containing event data for <see cref="Iiriya.Apps.SerialTester.PortStatus">PortStatus</see> based events for the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> objects.
    /// </summary>
    public class SpiStatusEventArgs : EventArgs
    {
        #region SpiStatusEventArgs Fields
        /// <summary>
        /// The status of the serial port.
        /// </summary>
        private PortStatus status;
        #endregion

        #region SpiStatusEventArgs Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.SpiStatusEventArgs">SpiStatusEventArgs</see> class.
        /// </summary>
        public SpiStatusEventArgs() : this(PortStatus.Stopped)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.SpiStatusEventArgs">SpiStatusEventArgs</see> class.
        /// </summary>
        /// <param name="status">Type: <see cref="Iiriya.Apps.SerialTester.PortStatus">PortStatus</see>. The status of the serial port.</param>
        public SpiStatusEventArgs(PortStatus status) : base()
        {
            this.status = status;
        }
        #endregion

        #region SpiStatusEventArgs Properties
        /// <summary>
        /// Gets or sets the status of the serial port.
        /// </summary>
        public PortStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }
        #endregion
    }
}