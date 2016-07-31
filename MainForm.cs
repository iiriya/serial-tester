//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the main application form.</summary>
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
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.IO.Ports;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// The main application form.
    /// </summary>
    public class MainForm : Form
    {
        #region MainForm Fields
        /// <summary>
        /// The log stream.
        /// </summary>
        private Stream log;

        /// <summary>
        /// The log view.
        /// </summary>
        private DataGridView logView;

        /// <summary>
        /// The ports details view.
        /// </summary>
        private DataGridView portsView;

        /// <summary>
        /// The command button.
        /// </summary>
        private SwitchButton commandButton;

        /// <summary>
        /// The refresh button.
        /// </summary>
        private Button refreshButton;

        /// <summary>
        /// The clear log button.
        /// </summary>
        private Button clearButton;

        /// <summary>
        /// The column that contains the port listen checkbox in the ports viewer.
        /// </summary>
        private DataGridViewCheckBoxColumn dgvPortListen;

        /// <summary>
        /// The column that contains the port status in the ports viewer.
        /// </summary>
        private DataGridViewStatusColumn dgvPortStatus;

        /// <summary>
        /// The column that contains the port name in the ports viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvPortName;

        /// <summary>
        /// The column that contains the port baud rate in the ports viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvPortBaudRate;

        /// <summary>
        /// The column that contains the port parity in the ports viewer.
        /// </summary>
        private DataGridViewComboBoxColumn dgvPortParity;

        /// <summary>
        /// The column that contains the port DTR option viewer.
        /// </summary>
        private DataGridViewCheckBoxColumn dgvPortDtr;

        /// <summary>
        /// The column that contains the port RTS option in the ports viewer.
        /// </summary>
        private DataGridViewCheckBoxColumn dgvPortRts;

        /// <summary>
        /// The column that contains the port data bits length in the ports viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvPortDataBits;

        /// <summary>
        /// The column that contains the port response type in the ports viewer.
        /// </summary>
        private DataGridViewComboBoxColumn dgvPortResponseType;

        /// <summary>
        /// The column that contains the log source in the log viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvLogSource;

        /// <summary>
        /// The column that contains the log date in the log viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvLogDate;

        /// <summary>
        /// The column that contains the log type in the log viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvLogType;

        /// <summary>
        /// The column that contains the log value in the log viewer.
        /// </summary>
        private DataGridViewTextBoxColumn dgvLogValue;

        /// <summary>
        /// The <see cref="System.Windows.Form.TextBox">TextBox</see> that contains the text to send.
        /// </summary>
        private TextBox sendTextTextBox;

        /// <summary>
        /// The <see cref="System.Windows.Form.Button">Button</see> that sends the text value.
        /// </summary>
        private Button sendTextButton;

        /// <summary>
        /// The <see cref="System.Windows.Form.TextBox">TextBox</see> that contains the bytes to send.
        /// </summary>
        private TextBox sendBytesTextBox;

        /// <summary>
        /// The <see cref="System.Windows.Form.Button">Button</see> that sends the bytes value.
        /// </summary>
        private Button sendBytesButton;

        /// <summary>
        /// The <see cref="System.Windows.Form.ComboBox">ComboBox</see> that contains the choice selector for the parser bytes parser.
        /// </summary>
        private ComboBox sendBytesComboBox;

        /// <summary>
        /// The <see cref="Iiriya.Apps.SerialTester.SwitchButton">SwitchButton</see> used to toggle the <see cref="System.Windows.Form.TopMost">TopMost</see> property.
        /// </summary>
        private SwitchButton alwaysOnTopButton;

        /// <summary>
        /// The <see cref="Iiriya.Apps.SerialTester.SwitchButton">SwitchButton</see> used to toggle the log file writing status.
        /// </summary>
        private SwitchButton logToFileButton;

        /// <summary>
        /// The button that opens <see cref="Iiriya.Apps.SerialTester.MainForm.logFileDialog">logFileDialog</see> dialog.
        /// </summary>
        private SwitchButton logFileButton;

        /// <summary>
        /// The dialog used to determine where the log file will be saved.
        /// </summary>
        private SaveFileDialog logFileDialog;
        #endregion

        #region MainForm Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.MainForm">MainForm</see> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();

            this.SetupPortsView();

            this.logFileButton.Click += delegate(object sender, EventArgs e)
            {
                this.logFileDialog.ShowDialog(this);
                this.logFileButton.SwitchValue = this.logFileDialog.CheckPathExists;

                if (this.logFileButton.SwitchValue)
                {
                    if (this.log != null)
                    {
                        this.log.Close();
                        this.log.Dispose();
                    }

                    this.log = this.logFileDialog.OpenFile();
                }
            };

            this.logToFileButton.Click += delegate(object sender, EventArgs e)
            {
                if (this.logToFileButton.SwitchValue)
                {
                    this.logToFileButton.SwitchValue = false;
                }
                else
                {
                    this.logToFileButton.SwitchValue = this.logFileButton.SwitchValue;
                }
            };

            this.refreshButton.Click += delegate(object sender, EventArgs e)
            {
                this.RefreshPorts();
            };

            this.alwaysOnTopButton.Click += delegate(object sender, EventArgs e)
            {
                this.alwaysOnTopButton.SwitchValue = !this.alwaysOnTopButton.SwitchValue;
                this.TopMost = this.alwaysOnTopButton.SwitchValue;
            };

            this.commandButton.Click += delegate(object sender, EventArgs e)
            {
                if (this.commandButton.SwitchValue)
                {
                    Program.Ports.CloseAll();
                    this.commandButton.SwitchValue = false;
                }
                else
                {
                    this.commandButton.SwitchValue = this.PortsListen();
                }

                this.portsView.DataSource = null;
                this.SetupPortsView();
            };

            this.clearButton.Click += delegate(object sender, EventArgs e)
            {
                this.logView.Rows.Clear();
            };

            this.logView.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                using (CellViewerForm form = new CellViewerForm())
                {
                    form.Value = this.logView.Rows[e.RowIndex].Cells["dgvLogValue"].FormattedValue.ToString();
                    form.ShowDialog(this);
                }
            };

            this.sendBytesTextBox.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                e.Handled = this.KeyHandled(e.KeyChar);
            };

            this.sendTextButton.Click += delegate(object sender, EventArgs e)
            {
                if (!string.IsNullOrEmpty(this.sendTextTextBox.Text))
                {
                    Program.Ports.SendAll(this.sendTextTextBox.Text);
                }
            };

            this.sendBytesButton.Click += delegate(object sender, EventArgs e)
            {
                NumberStyles style = this.sendBytesComboBox.SelectedItem == null ? NumberStyles.Any : (NumberStyles)this.sendBytesComboBox.SelectedItem;
                Program.Ports.ParseBytesAndSend(this.sendBytesTextBox.Text, style);
            };

            this.portsView.AutoGenerateColumns = false;
            this.logView.AutoGenerateColumns = false;
        }
        #endregion

        #region MainForm Delegates
        /// <summary>
        /// A callback for log writing.
        /// </summary>
        /// <param name="source">Type: <see cref="System.String">String</see>. The port source.</param>
        /// <param name="value">Type: <see cref="System.String">String</see>. The log entry.</param>
        /// <param name="direction">Type: <see cref="Iiriya.Apps.SerialTester.PortDirection">PortDirection</see>. The port direction.</param>
        private delegate void WriteCallback(string source, string value, PortDirection direction);
        #endregion

        #region MainForm Methods
        /// <summary>
        /// Determines whether the given <paramref name="value"/> was handled.
        /// </summary>
        /// <param name="value">Type: <see cref="System.Char">Char</see>. The value to check.</param>
        /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the parameter has been handled; otherwise, <see cref="System.Boolean.False">False</see>.</returns>
        public bool KeyHandled(char value)
        {
            NumberStyles style = this.sendBytesComboBox.SelectedItem == null ? NumberStyles.Any : (NumberStyles)this.sendBytesComboBox.SelectedItem;

            if (style == NumberStyles.Number || style == NumberStyles.Integer)
            {
                return !char.IsNumber(value) && !char.IsControl(value) && !char.IsWhiteSpace(value);
            }
            else
            {
                if (char.IsLetter(value))
                {
                    return !(char.ToUpperInvariant(value) >= 'A' && char.ToUpperInvariant(value) <= 'F');
                }
                else
                {
                    return !char.IsNumber(value) && !char.IsControl(value) && !char.IsWhiteSpace(value);
                }
            }
        }

        /// <summary>
        /// Refreshes the port in the ports view.
        /// </summary>
        public void RefreshPorts()
        {
            Program.Ports.Refresh();

            this.portsView.DataSource = null;
            this.SetupPortsView();
        }

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <param name="source">Type: <see cref="System.String">String</see>. The port source.</param>
        /// <param name="value">Type: <see cref="System.String">String</see>. The log entry.</param>
        /// <param name="direction">Type: <see cref="Iiriya.Apps.SerialTester.PortDirection">PortDirection</see>. The port direction.</param>
        public void WriteLog(string source, string value, PortDirection direction)
        {
            if (this.logView.InvokeRequired)
            {
                WriteCallback d = new WriteCallback(this.WriteLog);
                this.Invoke(d, new object[] { source, value, direction });
            }
            else
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                value = value.Trim(new char[] { '\r', '\n' });

                string date = DateTime.Now.ToString();
                string bound = direction == PortDirection.In ? "IN" : "OUT";
                this.logView.Rows.Add(new object[] { source, date, bound, value.Replace(Environment.NewLine, " ") });
                this.logView.FirstDisplayedScrollingRowIndex = this.logView.Rows.Count - 1;

                if (this.logToFileButton.SwitchValue)
                {
                    byte[] s = ASCIIEncoding.ASCII.GetBytes("[" + date + "] " + source + ":" + bound + Environment.NewLine + "-----------------" + Environment.NewLine);
                    this.log.Write(s, 0, s.Length);
                    s = ASCIIEncoding.ASCII.GetBytes(value.Replace("\r", string.Empty).Replace("\n", Environment.NewLine) + Environment.NewLine + Environment.NewLine);
                    this.log.Write(s, 0, s.Length);
                }
            }
        }

        /// <summary>
        /// Listens to the selected ports.
        /// </summary>
        /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if at least one port is listening; otherwise, <see cref="System.Boolean.False">False</see>.</returns>
        protected bool PortsListen()
        {
            bool started = false;

            foreach (DataGridViewRow row in this.portsView.Rows)
            {
                string key = row.Cells["dgvPortName"].Value.ToString();
                Spi spi;

                if (Program.Ports.TryGetValue(key, out spi))
                {
                    if (spi.Open())
                    {
                        spi.LogReceived += delegate(object s, SpiLogWriterEventArgs args)
                        {
                            this.WriteLog(args.Source, args.Entry, args.Direction);
                        };

                        spi.LogSent += delegate(object s, SpiLogWriterEventArgs args)
                        {
                            this.WriteLog(args.Source, args.Entry, args.Direction);
                        };

                        if (!started)
                        {
                            started = spi.Status == PortStatus.Run;
                        }
                    }
                }
            }

            return started;
        }

        /// <summary>
        /// Sets the current view for the selected ports.
        /// </summary>
        protected void SetupPortsView()
        {
            this.portsView.DataSource = typeof(SpiList);
            this.portsView.DataSource = Program.Ports;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if managed resources should be disposed; otherwise, <see cref="System.Boolean.False">False</see>.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.log != null)
                {
                    this.log.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">Type: <see cref="System.Windows.Forms.Message">Message</see>. A <see cref="System.Windows.Forms.Message">Message</see>, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">Type: <see cref="System.Windows.Forms.Keys">Keys</see>. One of the <see cref="System.Windows.Forms.Keys">Keys</see> values that represents the key to process.</param>
        /// <returns>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the keystroke was processed and consumed by the control; otherwise, <see cref="System.Boolean.False">False</see> to allow further processing.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    this.RefreshPorts();
                    return true;
                case Keys.F1:
                    using (HelpForm form = new HelpForm())
                    {
                        form.ShowDialog(this);
                    }

                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Initializes the visual components.
        /// </summary>
        private void InitializeComponent()
        {
            this.logView = new DataGridView();
            this.portsView = new DataGridView();
            this.dgvLogSource = new DataGridViewTextBoxColumn();
            this.dgvLogDate = new DataGridViewTextBoxColumn();
            this.dgvLogType = new DataGridViewTextBoxColumn();
            this.dgvLogValue = new DataGridViewTextBoxColumn();
            this.dgvPortListen = new DataGridViewCheckBoxColumn();
            this.dgvPortStatus = new DataGridViewStatusColumn();
            this.dgvPortName = new DataGridViewTextBoxColumn();
            this.dgvPortBaudRate = new DataGridViewTextBoxColumn();
            this.dgvPortParity = new DataGridViewComboBoxColumn();
            this.dgvPortDtr = new DataGridViewCheckBoxColumn();
            this.dgvPortRts = new DataGridViewCheckBoxColumn();
            this.dgvPortDataBits = new DataGridViewTextBoxColumn();
            this.dgvPortResponseType = new DataGridViewComboBoxColumn();
            this.sendTextButton = new Button();
            this.sendTextTextBox = new TextBox();
            this.clearButton = new Button();
            this.refreshButton = new Button();
            this.sendBytesButton = new Button();
            this.sendBytesTextBox = new TextBox();
            this.logFileDialog = new SaveFileDialog();
            this.sendBytesComboBox = new ComboBox();
            this.logFileButton = new SwitchButton();
            this.logToFileButton = new SwitchButton();
            this.alwaysOnTopButton = new SwitchButton();
            this.commandButton = new SwitchButton();

            ((ISupportInitialize)this.logView).BeginInit();
            ((ISupportInitialize)this.portsView).BeginInit();
            this.SuspendLayout();

            this.logView.AllowUserToAddRows = false;
            this.logView.AllowUserToOrderColumns = true;
            this.logView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.logView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.logView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.logView.BorderStyle = BorderStyle.None;
            this.logView.Columns.AddRange(new DataGridViewColumn[] { this.dgvLogSource, this.dgvLogDate, this.dgvLogType, this.dgvLogValue });
            this.logView.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.logView.Location = new Point(12, 211);
            this.logView.Name = "logView";
            this.logView.ReadOnly = true;
            this.logView.RowHeadersVisible = false;
            this.logView.RowTemplate.Resizable = DataGridViewTriState.False;
            this.logView.ShowEditingIcon = false;
            this.logView.Size = new Size(896, 418);
            this.logView.TabIndex = 14;

            this.dgvLogSource.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgvLogSource.HeaderText = "Source";
            this.dgvLogSource.MaxInputLength = 256;
            this.dgvLogSource.MinimumWidth = 65;
            this.dgvLogSource.Name = "dgvLogSource";
            this.dgvLogSource.ReadOnly = true;
            this.dgvLogSource.Width = 66;

            this.dgvLogDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvLogDate.HeaderText = "Date";
            this.dgvLogDate.MaxInputLength = 256;
            this.dgvLogDate.MinimumWidth = 120;
            this.dgvLogDate.Name = "dgvLogDate";
            this.dgvLogDate.ReadOnly = true;
            this.dgvLogDate.Width = 120;

            this.dgvLogType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvLogType.HeaderText = string.Empty;
            this.dgvLogType.MaxInputLength = 256;
            this.dgvLogType.MinimumWidth = 35;
            this.dgvLogType.Name = "dgvLogType";
            this.dgvLogType.ReadOnly = true;
            this.dgvLogType.Width = 35;

            this.dgvLogValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvLogValue.HeaderText = "Value";
            this.dgvLogValue.Name = "dgvLogValue";
            this.dgvLogValue.ReadOnly = true;
            this.dgvLogValue.SortMode = DataGridViewColumnSortMode.NotSortable;

            this.portsView.AllowUserToAddRows = false;
            this.portsView.AllowUserToDeleteRows = false;
            this.portsView.AllowUserToResizeColumns = false;
            this.portsView.AllowUserToResizeRows = false;
            this.portsView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.portsView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.portsView.BackgroundColor = Color.White;
            this.portsView.BorderStyle = BorderStyle.None;
            this.portsView.Columns.AddRange(new DataGridViewColumn[] { this.dgvPortListen, this.dgvPortStatus, this.dgvPortName, this.dgvPortBaudRate, this.dgvPortParity, this.dgvPortDtr, this.dgvPortRts, this.dgvPortDataBits, this.dgvPortResponseType });
            this.portsView.GridColor = Color.WhiteSmoke;
            this.portsView.Location = new Point(12, 12);
            this.portsView.MinimumSize = new Size(730, 139);
            this.portsView.MultiSelect = false;
            this.portsView.Name = "portsView";
            this.portsView.RowHeadersVisible = false;
            this.portsView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.portsView.Size = new Size(730, 139);
            this.portsView.TabIndex = 0;

            this.dgvPortListen.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvPortListen.DataPropertyName = "Listen";
            this.dgvPortListen.HeaderText = "Listen";
            this.dgvPortListen.Name = "dgvPortListen";
            this.dgvPortListen.Width = 45;

            this.dgvPortStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvPortStatus.DataPropertyName = "Status";
            this.dgvPortStatus.HeaderText = string.Empty;
            this.dgvPortStatus.MinimumWidth = 20;
            this.dgvPortStatus.Name = "dgvPortStatus";
            this.dgvPortStatus.ReadOnly = true;
            this.dgvPortStatus.Resizable = DataGridViewTriState.False;
            this.dgvPortStatus.Width = 20;

            this.dgvPortName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPortName.DataPropertyName = "Name";
            this.dgvPortName.FillWeight = 85.22337F;
            this.dgvPortName.HeaderText = "Name";
            this.dgvPortName.Name = "dgvPortName";
            this.dgvPortName.ReadOnly = true;

            this.dgvPortBaudRate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPortBaudRate.DataPropertyName = "BaudRate";
            this.dgvPortBaudRate.FillWeight = 85.22337F;
            this.dgvPortBaudRate.HeaderText = "Baud Rate";
            this.dgvPortBaudRate.Name = "dgvPortBaudRate";

            this.dgvPortParity.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPortParity.DataPropertyName = "Parity";
            this.dgvPortParity.FillWeight = 85.22337F;
            this.dgvPortParity.MinimumWidth = 70;
            this.dgvPortParity.FlatStyle = FlatStyle.Flat;
            this.dgvPortParity.HeaderText = "Parity";
            this.dgvPortParity.Items.AddRange(new object[] { Parity.None, Parity.Even, Parity.Mark, Parity.Odd, Parity.Space });
            this.dgvPortParity.Name = "dgvPortParity";

            this.dgvPortDtr.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvPortDtr.DataPropertyName = "Dtr";
            this.dgvPortDtr.HeaderText = "DTR";
            this.dgvPortDtr.MinimumWidth = 35;
            this.dgvPortDtr.Name = "dgvPortDtr";
            this.dgvPortDtr.Resizable = DataGridViewTriState.False;
            this.dgvPortDtr.ToolTipText = "Data Terminal Ready";
            this.dgvPortDtr.Width = 35;

            this.dgvPortRts.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvPortRts.DataPropertyName = "Rts";
            this.dgvPortRts.HeaderText = "RTS";
            this.dgvPortRts.MinimumWidth = 35;
            this.dgvPortRts.Width = 35;
            this.dgvPortRts.Name = "dgvPortRts";
            this.dgvPortRts.Resizable = DataGridViewTriState.False;
            this.dgvPortRts.ToolTipText = "Request to Send";

            this.dgvPortDataBits.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPortDataBits.DataPropertyName = "DataBits";
            this.dgvPortDataBits.HeaderText = "Data Bits";
            this.dgvPortDataBits.MaxInputLength = 5;
            this.dgvPortDataBits.Name = "dgvPortDataBits";

            this.dgvPortResponseType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dgvPortResponseType.DataPropertyName = "ResponseType";
            this.dgvPortResponseType.HeaderText = "Response Type";
            this.dgvPortResponseType.MinimumWidth = 64;
            this.dgvPortResponseType.FlatStyle = FlatStyle.Flat;
            this.dgvPortResponseType.Items.AddRange(new object[] { SpiResponse.Text, SpiResponse.Bytes });
            this.dgvPortResponseType.Name = "dgvPortResponseType";

            this.sendTextButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.sendTextButton.BackColor = Color.FromArgb(224, 224, 224);
            this.sendTextButton.FlatAppearance.BorderSize = 0;
            this.sendTextButton.FlatStyle = FlatStyle.Flat;
            this.sendTextButton.Location = new Point(813, 157);
            this.sendTextButton.Name = "sendTextButton";
            this.sendTextButton.Size = new Size(95, 21);
            this.sendTextButton.TabIndex = 10;
            this.sendTextButton.Text = "Send Text";
            this.sendTextButton.UseVisualStyleBackColor = false;

            this.sendTextTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.sendTextTextBox.BorderStyle = BorderStyle.None;
            this.sendTextTextBox.Location = new Point(12, 157);
            this.sendTextTextBox.Multiline = true;
            this.sendTextTextBox.Name = "sendTextTextBox";
            this.sendTextTextBox.Size = new Size(795, 21);
            this.sendTextTextBox.TabIndex = 9;

            this.clearButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.clearButton.BackColor = Color.FromArgb(224, 224, 224);
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.FlatStyle = FlatStyle.Flat;
            this.clearButton.Location = new Point(748, 70);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new Size(160, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear Log";
            this.clearButton.UseVisualStyleBackColor = false;

            this.refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.refreshButton.BackColor = Color.FromArgb(224, 224, 224);
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = FlatStyle.Flat;
            this.refreshButton.Location = new Point(748, 99);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new Size(160, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Refresh Ports List";
            this.refreshButton.UseVisualStyleBackColor = false;

            this.sendBytesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.sendBytesButton.BackColor = Color.FromArgb(224, 224, 224);
            this.sendBytesButton.FlatAppearance.BorderSize = 0;
            this.sendBytesButton.FlatStyle = FlatStyle.Flat;
            this.sendBytesButton.Location = new Point(813, 184);
            this.sendBytesButton.Name = "sendBytesButton";
            this.sendBytesButton.Size = new Size(95, 21);
            this.sendBytesButton.TabIndex = 13;
            this.sendBytesButton.Text = "Send Bytes";
            this.sendBytesButton.UseVisualStyleBackColor = false;

            this.sendBytesTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.sendBytesTextBox.BorderStyle = BorderStyle.None;
            this.sendBytesTextBox.CharacterCasing = CharacterCasing.Upper;
            this.sendBytesTextBox.Location = new Point(12, 184);
            this.sendBytesTextBox.Multiline = true;
            this.sendBytesTextBox.Name = "sendBytesTextBox";
            this.sendBytesTextBox.Size = new Size(685, 21);
            this.sendBytesTextBox.TabIndex = 11;

            this.logFileDialog.DefaultExt = "txt";
            this.logFileDialog.FileName = "SerialTesterLog";
            this.logFileDialog.Filter = "TXT Files|*.txt";

            this.sendBytesComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.sendBytesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.sendBytesComboBox.FlatStyle = FlatStyle.Flat;
            this.sendBytesComboBox.ForeColor = SystemColors.WindowText;
            this.sendBytesComboBox.FormattingEnabled = true;
            this.sendBytesComboBox.Items.AddRange(new object[] { NumberStyles.Any, NumberStyles.HexNumber, NumberStyles.Integer, NumberStyles.Number });
            this.sendBytesComboBox.SelectedItem = NumberStyles.Any;
            this.sendBytesComboBox.Location = new Point(703, 184);
            this.sendBytesComboBox.Name = "sendBytesComboBox";
            this.sendBytesComboBox.Size = new Size(104, 21);
            this.sendBytesComboBox.TabIndex = 12;

            this.logFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.logFileButton.BackColor = Color.Lime;
            this.logFileButton.FlatAppearance.BorderSize = 0;
            this.logFileButton.FlatStyle = FlatStyle.Flat;
            this.logFileButton.Image = global::Iiriya.Apps.SerialTester.Properties.Resources.doc;
            this.logFileButton.Location = new Point(885, 12);
            this.logFileButton.Name = "logFileButton";
            this.logFileButton.Size = new Size(23, 23);
            this.logFileButton.SwitchOffColor = Color.Lime;
            this.logFileButton.SwitchOffText = string.Empty;
            this.logFileButton.SwitchOnColor = Color.Red;
            this.logFileButton.SwitchOnText = string.Empty;
            this.logFileButton.SwitchValue = false;
            this.logFileButton.TabIndex = 2;
            this.logFileButton.UseVisualStyleBackColor = false;

            this.logToFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.logToFileButton.BackColor = Color.Lime;
            this.logToFileButton.FlatAppearance.BorderSize = 0;
            this.logToFileButton.FlatStyle = FlatStyle.Flat;
            this.logToFileButton.Location = new Point(748, 12);
            this.logToFileButton.Name = "logToFileButton";
            this.logToFileButton.Size = new Size(131, 23);
            this.logToFileButton.SwitchOffColor = Color.Lime;
            this.logToFileButton.SwitchOffText = "Start Log To File";
            this.logToFileButton.SwitchOnColor = Color.Red;
            this.logToFileButton.SwitchOnText = "Stop Log To File";
            this.logToFileButton.SwitchValue = false;
            this.logToFileButton.TabIndex = 1;
            this.logToFileButton.Text = "Start Log To File";
            this.logToFileButton.UseVisualStyleBackColor = false;

            this.alwaysOnTopButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.alwaysOnTopButton.BackColor = Color.FromArgb(224, 224, 224);
            this.alwaysOnTopButton.FlatAppearance.BorderSize = 0;
            this.alwaysOnTopButton.FlatStyle = FlatStyle.Flat;
            this.alwaysOnTopButton.Location = new Point(748, 41);
            this.alwaysOnTopButton.Name = "alwaysOnTopButton";
            this.alwaysOnTopButton.Size = new Size(160, 23);
            this.alwaysOnTopButton.SwitchOffColor = Color.FromArgb(224, 224, 224);
            this.alwaysOnTopButton.SwitchOffText = "Always On Top";
            this.alwaysOnTopButton.SwitchOnColor = Color.Silver;
            this.alwaysOnTopButton.SwitchOnText = "Not On Top";
            this.alwaysOnTopButton.SwitchValue = false;
            this.alwaysOnTopButton.TabIndex = 3;
            this.alwaysOnTopButton.Text = "Always On Top";
            this.alwaysOnTopButton.UseVisualStyleBackColor = false;

            this.commandButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.commandButton.BackColor = Color.Lime;
            this.commandButton.FlatAppearance.BorderSize = 0;
            this.commandButton.FlatStyle = FlatStyle.Flat;
            this.commandButton.Location = new Point(748, 128);
            this.commandButton.Name = "commandButton";
            this.commandButton.Size = new Size(160, 23);
            this.commandButton.SwitchOffColor = Color.Lime;
            this.commandButton.SwitchOffText = "Connect";
            this.commandButton.SwitchOnColor = Color.Red;
            this.commandButton.SwitchOnText = "Disconnect";
            this.commandButton.SwitchValue = false;
            this.commandButton.TabIndex = 6;
            this.commandButton.Text = "Connect";
            this.commandButton.UseVisualStyleBackColor = false;

            this.Controls.Add(this.sendBytesComboBox);
            this.Controls.Add(this.logFileButton);
            this.Controls.Add(this.logToFileButton);
            this.Controls.Add(this.alwaysOnTopButton);
            this.Controls.Add(this.sendBytesButton);
            this.Controls.Add(this.sendBytesTextBox);
            this.Controls.Add(this.sendTextButton);
            this.Controls.Add(this.sendTextTextBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.commandButton);
            this.Controls.Add(this.portsView);
            this.Controls.Add(this.logView);

            this.Icon = global::Iiriya.Apps.SerialTester.Properties.Resources.app;
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.ClientSize = new Size(920, 640);
            this.MinimumSize = new Size(936, 679);
            this.Name = "MainForm";
            this.Text = "Iiriya\'s SerialTester";
            ((ISupportInitialize)this.logView).EndInit();
            ((ISupportInitialize)this.portsView).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}