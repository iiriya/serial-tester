//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="Spi.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the Spi object.</summary>
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
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.IO.Ports;
    #endregion

    /// <summary>
    /// Provides an interface for a <see cref="System.IO.Ports.SerialPort">SerialPort</see>.
    /// </summary>
    public class Spi : IDisposable, INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Spi Fields
        /// <summary>
        /// The base serial port.
        /// </summary>
        private SerialPort port;

        /// <summary>
        /// The port status.
        /// </summary>
        private PortStatus status = PortStatus.Stopped;

        /// <summary>
        /// The response type.
        /// </summary>
        private SpiResponse responseType = SpiResponse.Text;

        /// <summary>
        /// A value indicating whether the port is listened by the logger.
        /// </summary>
        private bool listen;
        #endregion

        #region Spi Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> class.
        /// </summary>
        /// <param name="name">Type: <see cref="System.String">String</see>. The port name.</param>
        public Spi(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.port = new SerialPort(name);
                this.PortSetup();
            }
            else
            {
                throw new ArgumentNullException("name");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> class as wrapper for a serial port.
        /// </summary>
        /// <param name="sp">Type: <see cref="System.IO.Ports.SerialPort">SerialPort</see>. A serial port.</param>
        public Spi(SerialPort sp)
        {
            if (sp != null)
            {
                if (!string.IsNullOrEmpty(sp.PortName))
                {
                    this.port = sp;

                    if (sp.IsOpen)
                    {
                        this.status = PortStatus.Run;
                    }

                    this.PortSetup();
                }
                else
                {
                    throw new ArgumentException("The port name is null or not valid.", "sp");
                }
            }
            else
            {
                throw new ArgumentNullException("sp");
            }
        }
        #endregion

        #region Spi Destructors
        /// <summary>
        /// Finalizes an instance of the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> class.
        /// </summary>
        ~Spi()
        {
            this.Dispose(false);
        }
        #endregion

        #region Spi Events
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Occurs when data has been received and is ready to be logged.
        /// </summary>
        public event EventHandler<SpiLogWriterEventArgs> LogReceived;

        /// <summary>
        /// Occurs when data has been sent and is ready to be logged.
        /// </summary>
        public event EventHandler<SpiLogWriterEventArgs> LogSent;

        /// <summary>
        /// Occurs when the port status is changed.
        /// </summary>
        public event EventHandler<SpiStatusEventArgs> StatusChanged;
        #endregion

        #region Spi Properties
        /// <summary>
        /// Gets the port name for communications.
        /// </summary>
        /// <value>Type: <see cref="System.String">String</see>. The port name for communications.</value>
        [ReadOnly(true), DisplayName("Name")]
        public string Name
        {
            get
            {
                return this.port.PortName;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the port is listened by the logger.
        /// </summary>
        /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the port is listened; otherwise <see cref="System.Boolean.False">False</see>.</value>
        [DisplayName("Listen")]
        public bool Listen
        {
            get
            {
                return this.listen;
            }

            set
            {
                this.OnPropertyChanging("Listen");
                this.listen = value;
                this.OnPropertyChanged("Listen");
            }
        }

        /// <summary>
        /// Gets or sets the serial baud rate.
        /// </summary>
        /// <value>Type: <see cref="System.Int32">Int32</see>. The serial baud rate.</value>
        [DisplayName("Baud Rate"), TypeConverter(typeof(BaudRateConverter))]
        public int BaudRate
        {
            get
            {
                return this.port.BaudRate;
            }

            set
            {
                this.OnPropertyChanging("BaudRate");
                this.port.BaudRate = Math.Abs(value);
                this.OnPropertyChanged("BaudRate");
            }
        }

        /// <summary>
        /// Gets or sets the parity-checking protocol.
        /// </summary>
        /// <value>Type: <see cref="System.IO.Ports.Parity">Parity</see>. One of the <see cref="System.IO.Ports.Parity">Parity</see> values that represents the parity-checking protocol. The default is <see cref="System.IO.Ports.Parity.None">None</see>.</value>
        [DisplayName("Parity"), TypeConverter(typeof(EnumConverter))]
        public Parity Parity
        {
            get
            {
                return this.port.Parity;
            }

            set
            {
                this.OnPropertyChanging("Parity");
                this.port.Parity = value;
                this.OnPropertyChanged("Parity");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Data Terminal Ready (DTR) signal during serial communication is enabled or not.
        /// </summary>
        /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> to enable Data Terminal Ready (DTR); otherwise, <see cref="System.Boolean.False">False</see>. The default is <see cref="System.Boolean.False">False</see>.</value>
        [DisplayName("DTR")]
        public bool Dtr
        {
            get
            {
                return this.port.DtrEnable;
            }

            set
            {
                this.OnPropertyChanging("Dtr");
                this.port.DtrEnable = value;
                this.OnPropertyChanged("Dtr");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.
        /// </summary>
        /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> to enable Request to Transmit (RTS); otherwise, <see cref="System.Boolean.False">False</see>. The default is <see cref="System.Boolean.False">False</see>.</value>
        [DisplayName("RTS")]
        public bool Rts
        {
            get
            {
                return this.port.RtsEnable;
            }

            set
            {
                this.OnPropertyChanging("Rts");
                this.port.RtsEnable = value;
                this.OnPropertyChanged("Rts");
            }
        }

        /// <summary>
        /// Gets or sets the port status.
        /// </summary>
        /// <value>Type: <see cref="Iiriya.Apps.SerialTester.PortStatus">PortStatus</see>. <see cref="Iiriya.Apps.SerialTester.PortStatus.Run">Run</see> to indicate that the port is opened and working; <see cref="Iiriya.Apps.SerialTester.PortStatus.Stopped">Stopped</see> to indicate the port is closed; <see cref="Iiriya.Apps.SerialTester.PortStatus.Stopped">Stopped</see> to indicate the port has an error.</value>
        [DisplayName("Status"), TypeConverter(typeof(EnumConverter))]
        public PortStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.OnPropertyChanging("Status");
                this.status = value;
                this.OnPropertyChanged("Status");
                this.OnStatusChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets the standard length of data bits per byte.
        /// </summary>
        /// <value>Type: <see cref="System.Int32">Int32</see>. The data bits length.</value>
        [DisplayName("Data Bits"), TypeConverter(typeof(Int32Converter))]
        public int DataBits
        {
            get
            {
                return this.port.DataBits;
            }

            set
            {
                this.OnPropertyChanging("DataBits");
                this.port.DataBits = value;
                this.OnPropertyChanged("DataBits");
            }
        }

        /// <summary>
        /// Gets or sets the response type.
        /// </summary>
        /// <value>Type: <see cref="Iiriya.Apps.SerialTester.SpiResponse">SpiResponse</see>. The response type.</value>
        [DisplayName("Response Type")]
        public SpiResponse ResponseType
        {
            get
            {
                return this.responseType;
            }

            set
            {
                this.responseType = value;
            }
        }
        #endregion

        #region Spi Methods
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Opens a new serial port connection.
        /// </summary>
        /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the port has been successfully opened; otherwise <see cref="System.Boolean.False">False</see>.</returns>
        public bool Open()
        {
            if (this.Listen)
            {
                try
                {
                    this.port.Open();
                    this.Status = PortStatus.Run;
                    return true;
                }
                catch (ArgumentException)
                {
                    this.Status = PortStatus.Error;
                }
                catch (IOException)
                {
                    this.Status = PortStatus.Error;
                }
                catch (InvalidOperationException)
                {
                    this.Status = PortStatus.Error;
                }
                catch (UnauthorizedAccessException)
                {
                    this.Status = PortStatus.Error;
                }
            }

            return false;
        }

        /// <summary>
        /// Closes the port connection.
        /// </summary>
        public void Close()
        {
            try
            {
                this.port.Close();
                this.Status = PortStatus.Stopped;
            }
            catch (InvalidOperationException)
            {
                this.Status = PortStatus.Error;
            }
            catch (IOException)
            {
                this.Status = PortStatus.Error;
            }

            this.LogReceived = null;
            this.LogSent = null;
        }

        /// <summary>
        /// Sends the given <paramref name="value"/> to the port.
        /// </summary>
        /// <param name="value">Type: <see cref="System.Byte">Byte</see>. A <see cref="System.Byte">Byte</see> to send.</param>
        public void Send(byte value)
        {
            if (this.port.IsOpen)
            {
                try
                {
                    this.port.BaseStream.WriteByte(value);
                    this.OnLogSent("Byte: " + value.ToString(CultureInfo.InvariantCulture));
                }
                catch (IOException)
                {
                    this.Status = PortStatus.Error;
                }
                catch (NotSupportedException)
                {
                    this.Status = PortStatus.Error;
                }
                catch (ObjectDisposedException)
                {
                    this.Status = PortStatus.Error;
                }
            }
        }

        /// <summary>
        /// Sends the given <paramref name="value"/> to the port.
        /// </summary>
        /// <param name="value">Type: <see cref="System.String">String</see>. A <see cref="System.String">String</see> to send.</param>
        public void Send(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (this.port.IsOpen)
                {
                    try
                    {
                        this.port.Write(value);
                        this.OnLogSent(value);
                    }
                    catch (IOException)
                    {
                        this.Status = PortStatus.Error;
                    }
                    catch (TimeoutException)
                    {
                        this.Status = PortStatus.Error;
                    }
                }
            }
        }

        /// <summary>
        /// Sends the given <paramref name="value"/> to the port.
        /// </summary>
        /// <param name="value">Type: <see cref="System.Byte">Byte</see>[]. An array of <see cref="System.Byte">Byte</see> objects to send.</param>
        public void Send(byte[] value)
        {
            if (value != null)
            {
                if (value.Length > 0)
                {
                    if (this.port.IsOpen)
                    {
                        try
                        {
                            this.port.Write(value, 0, value.Length);

                            string text = string.Empty;

                            foreach (byte b in value)
                            {
                                text += ' ' + b.ToString("X2", CultureInfo.InvariantCulture);
                            }

                            this.OnLogSent("Bytes: " + text);
                            return;
                        }
                        catch (ArgumentException)
                        {
                            this.Status = PortStatus.Error;
                        }
                        catch (IOException)
                        {
                            this.Status = PortStatus.Error;
                        }
                        catch (TimeoutException)
                        {
                            this.Status = PortStatus.Error;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prepares the port.
        /// </summary>
        protected void PortSetup()
        {
            this.port.ErrorReceived += delegate(object sender, SerialErrorReceivedEventArgs e)
            {
                this.Status = PortStatus.Error;
            };

            this.port.DataReceived += delegate(object sender, SerialDataReceivedEventArgs e)
            {
                switch (this.ResponseType)
                {
                    case SpiResponse.Text:
                        this.OnLogReceived(this.port.ReadExisting());
                        break;
                    case SpiResponse.Bytes:
                        byte[] buffer = new byte[this.port.BytesToRead];
                        this.port.Read(buffer, 0, this.port.BytesToRead);
                        this.OnLogReceived(BitConverter.ToString(buffer, 0));
                        break;
                }
            };
        }

        /// <summary>
        /// Raises the <see cref="Iiriya.Apps.SerialTester.Spi.LogReceived">LogReceived</see> event.
        /// </summary>
        /// <param name="data">Type: <see cref="System.String">String</see>. The received data.</param>
        protected virtual void OnLogReceived(string data)
        {
            if (this.LogReceived != null)
            {
                this.LogReceived(this, new SpiLogWriterEventArgs
                {
                    Date = DateTime.Now,
                    Source = this.Name,
                    Entry = data,
                    Direction = PortDirection.In
                });
            }
        }

        /// <summary>
        /// Raises the <see cref="Iiriya.Apps.SerialTester.Spi.LogSent">LogSent</see> event.
        /// </summary>
        /// <param name="data">Type: <see cref="System.String">String</see>. The received sent.</param>
        protected virtual void OnLogSent(string data)
        {
            if (this.LogSent != null)
            {
                this.LogSent(this, new SpiLogWriterEventArgs
                {
                    Date = DateTime.Now,
                    Source = this.Name,
                    Entry = data,
                    Direction = PortDirection.Out
                });
            }
        }

        /// <summary>
        /// Raises the <see cref="Iiriya.Apps.SerialTester.Spi.StatusChanged">StatusChanged</see> event.
        /// </summary>
        /// <param name="portStatus">Type: <see cref="Iiriya.Apps.SerialTester.Spi.PortStatus">PortStatus</see>. The received sent.</param>
        protected virtual void OnStatusChanged(PortStatus portStatus)
        {
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new SpiStatusEventArgs(portStatus));
            }
        }

        /// <summary>
        /// Raises the <see cref="Iiriya.Apps.SerialTester.Spi.PropertyChanging">PropertyChanging</see> event.
        /// </summary>
        /// <param name="propertyName">Type: <see cref="System.String">String</see>. The property name.</param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (this.PropertyChanging != null)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="Iiriya.Apps.SerialTester.Spi.PropertyChanged">PropertyChanged</see> event.
        /// </summary>
        /// <param name="propertyName">Type: <see cref="System.String">String</see>. The property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">Type: <see cref="System.Boolean">Boolean</see>. If <see cref="System.Boolean.True">True</see> to release both managed and unmanaged resources; <see cref="System.Boolean.False">False</see> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.port != null)
            {
                if (disposing)
                {
                    try
                    {
                        this.port.Dispose();
                    }
                    catch (ObjectDisposedException)
                    {
                        this.Status = PortStatus.Error;
                    }
                    catch (IOException)
                    {
                        this.Status = PortStatus.Error;
                    }

                    this.port = null;
                }
            }
        }
        #endregion

        #region Spi BaudRateConverter Class
        /// <summary>
        /// A <see cref="System.ComponentModule.TypeConverter">TypeConverter</see> designed for Baud Rate.
        /// </summary>
        protected class BaudRateConverter : TypeConverter
        {
            #region BaudRateConverter Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.Spi.BaudRateConverter">BaudRateConverter</see> class.
            /// </summary>
            public BaudRateConverter() : base()
            {
            }
            #endregion

            #region BaudRateConverter Methods
            /// <summary>
            /// Converts the given object to the type of this converter, using the specified context and culture information.
            /// </summary>
            /// <param name="context">Type: <see cref="System.ComponentModel.ITypeDescriptorContext">ITypeDescriptorContext</see>. An <see cref="System.ComponentModel.ITypeDescriptorContext">ITypeDescriptorContext</see> that provides a format context.</param>
            /// <param name="culture">Type: <see cref="System.Globalization.CultureInfo">CultureInfo</see>. The <see cref="System.Globalization.CultureInfo">CultureInfo</see> to use as the current culture.</param>
            /// <param name="value">Type: <see cref="System.Object">Object</see>. The <see cref="System.Object">Object</see> to convert.</param>
            /// <returns>Type: <see cref="System.Object">Object</see>. An <see cref="System.Object">Object</see> that represents the converted value.</returns>
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                int i = 0;

                if (value != null)
                {
                    if (!int.TryParse(value.ToString(), out i))
                    {
                        return 0;
                    }
                }

                return i;
            }

            /// <summary>
            /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified <paramref name="context"/>.
            /// </summary>
            /// <param name="context">Type: <see cref="System.ComponentModel.ITypeDescriptorContext">ITypeDescriptorContext</see>. An <see cref="System.ComponentModel.ITypeDescriptorContext">ITypeDescriptorContext</see> that provides a format context.</param>
            /// <param name="sourceType">Type: <see cref="System.Type">Type</see>. A <see cref="System.Type">Type</see> that represents the type you want to convert from.</param>
            /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if this converter can perform the conversion; otherwise, <see cref="System.Boolean.False">False</see>.</returns>
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return true;
            }
            #endregion
        }
        #endregion
    }
}