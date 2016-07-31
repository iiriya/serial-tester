//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SpiList.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the SpiList collection.</summary>
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO.Ports;
    #endregion

    /// <summary>
    /// A collection for the <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see> objects.
    /// </summary>
    public class SpiList : KeyedCollection<string, Spi>, IDisposable
    {
        #region SpiList Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see> class.
        /// </summary>
        public SpiList() : base()
        {
        }
        #endregion

        #region SpiList Destructors
        /// <summary>
        /// Finalizes an instance of the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see> class.
        /// </summary>
        ~SpiList()
        {
            this.Dispose(false);
        }
        #endregion

        #region SpiList Methods
        /// <summary>
        /// Checks which ports are connected and updates the collection.
        /// </summary>
        public void Refresh()
        {
            string[] ports = SerialPort.GetPortNames();
            Collection<int> rems = new Collection<int>();

            for (int i = 0; i < this.Count; i++)
            {
                bool found = false;

                foreach (string name in ports)
                {
                    if (found = name == this.Items[i].Name)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    rems.Add(i);
                }
            }

            foreach (int i in rems)
            {
                this.RemoveItem(i);
            }

            foreach (string name in ports)
            {
                if (!this.Contains(name))
                {
                    this.Add(new Spi(name));
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Closes all the ports in the collection.
        /// </summary>
        public void CloseAll()
        {
            foreach (Spi spi in this.Items)
            {
                spi.Close();
            }
        }

        /// <summary>
        /// Sends the given <paramref name="value"/> to all ports.
        /// </summary>
        /// <param name="value">Type: <see cref="System.String">String</see>. The value to send.</param>
        /// <param name="style">Type: <see cref="System.Globalization.NumberStyles">NumberStyles</see>. The parser options.</param>
        public void ParseBytesAndSend(string value, NumberStyles style)
        {
            if (!string.IsNullOrEmpty(value))
            {
                List<byte> bytes = new List<byte>();

                foreach (string s in value.Split(new char[] { ' ', ',', ';', '-', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    byte b;

                    if (byte.TryParse(s, style, CultureInfo.InvariantCulture, out b))
                    {
                        bytes.Add(b);
                    }
                }

                this.SendAll(bytes.ToArray());
            }
        }

        /// <summary>
        /// Sends a <paramref name="value"/> to the ports in the collection.
        /// </summary>
        /// <param name="value">Type: <see cref="System.Byte">Byte</see>. The value to send.</param>
        public void SendAll(byte value)
        {
            foreach (Spi spi in this.Items)
            {
                spi.Send(value);
            }
        }

        /// <summary>
        /// Sends a <paramref name="value"/> to the ports in the collection.
        /// </summary>
        /// <param name="value">Type: <see cref="System.String">String</see>. The value to send.</param>
        public void SendAll(string value)
        {
            foreach (Spi spi in this.Items)
            {
                spi.Send(value);
            }
        }

        /// <summary>
        /// Sends a <paramref name="value"/> to the ports in the collection.
        /// </summary>
        /// <param name="value">Type: <see cref="System.Byte">Byte</see>. The value to send.</param>
        public void SendAll(byte[] value)
        {
            foreach (Spi spi in this.Items)
            {
                spi.Send(value);
            }
        }

        /// <summary>
        /// Gets the <paramref name="value"/> associated with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Type: <see cref="System.String">String</see>. The key of the <paramref name="value"/> to get.</param>
        /// <param name="value">Type: <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see>. When this method returns, contains the value associated with the specified <paramref name="key"/>, if the <paramref name="key"/> is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see> contains an element with the specified <paramref name="key"/>; otherwise, <see cref="System.Boolean.False">False</see>.</returns>
        public bool TryGetValue(string key, out Spi value)
        {
            value = null;

            if (!string.IsNullOrEmpty(key))
            {
                if (this.Contains(key))
                {
                    value = this[key];
                }
            }

            return value != null;
        }

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/> of the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see>.
        /// </summary>
        /// <param name="index">Type: <see cref="System.Int32">Int32</see>. The index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            Spi item = this.Items[index];
            base.RemoveItem(index);
            item.Dispose();
        }

        /// <summary>
        /// Removes all elements from the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see>.
        /// </summary>
        protected override void ClearItems()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                this.Items[i].Dispose();
            }

            base.ClearItems();
        }

        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <param name="item">The element from which to extract the key.</param>
        /// <returns>Type: <see cref="Iiriya.Apps.SerialTester.Spi">Spi</see>. The key for the specified element.</returns>
        protected override string GetKeyForItem(Spi item)
        {
            if (item != null)
            {
                return item.Name;
            }
            else
            {
                throw new ArgumentNullException("item");
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Iiriya.Apps.SerialTester.SpiList">SpiList</see> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">Type: <see cref="System.Boolean">Boolean</see>. If <see cref="System.Boolean.True">True</see> to release both managed and unmanaged resources; <see cref="System.Boolean.False">False</see> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = this.Count - 1; i >= 0; i--)
                {
                    this.RemoveItem(i);
                }
            }
        }
        #endregion
    }
}