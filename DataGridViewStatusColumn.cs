//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridViewStatusColumn.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the DataGridViewStatusColumn element.</summary>
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
    /// A <see cref="System.Windows.Forms.DataGridViewColumn">DataGridViewColumn</see> element designed for the <see cref="Iiriya.Apps.SerialTester.PortStatus">PortStatus</see> enumerator.
    /// </summary>
    public class DataGridViewStatusColumn : DataGridViewColumn
    {
        #region DataGridViewStatusColumn Fields
        /// <summary>
        /// A value indicating whether the user can edit the column's cells.
        /// </summary>
        private bool readOnly = true;

        /// <summary>
        /// The template used to create new cells.
        /// </summary>
        private DataGridViewCell cellTemplate = new DataGridViewStatusCell();
        #endregion

        #region DataGridViewStatusColumn Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.DataGridViewStatusColumn">DataGridViewStatusColumn</see> class.
        /// </summary>
        public DataGridViewStatusColumn() : base()
        {
        }
        #endregion

        #region DataGridViewStatusColumn Properties
        /// <summary>
        /// Gets or sets the template used to create new cells.
        /// </summary>
        /// <value>Type: <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see>. A <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see> that all other cells in the column are modeled after. The default is null.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return this.cellTemplate;
            }

            set
            {
                this.cellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can edit the column's cells.
        /// </summary>
        /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the user cannot edit the column's cells; otherwise, <see cref="System.Boolean.False">False</see>.</value>
        public override bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }

            set
            {
                this.readOnly = value;
            }
        }
        #endregion

        #region DataGridViewStatusColumn DataGridViewStatusCell Class
        /// <summary>
        /// Represents an individual cell in a <see cref="System.Windows.Forms.DataGridView">DataGridView</see> control.
        /// </summary>
        protected class DataGridViewStatusCell : DataGridViewCell
        {
            #region DataGridViewStatusCell Fields
            /// <summary>
            /// A value indicating whether the cell's data can be edited.
            /// </summary>
            private bool readOnly = true;
            #endregion

            #region DataGridViewStatusCell Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="Iiriya.Apps.SerialTester.DataGridViewStatusColumn.DataGridViewStatusCell">DataGridViewStatusCell</see> class.
            /// </summary>
            public DataGridViewStatusCell() : base()
            {
            }
            #endregion

            #region DataGridViewStatusCell Properties
            /// <summary>
            /// Gets or sets a value indicating whether the cell's data can be edited.
            /// </summary>
            /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the cell's data cannot be edited; otherwise, <see cref="System.Boolean.False">False</see>.</value>
            public override bool ReadOnly
            {
                get
                {
                    return this.readOnly;
                }

                set
                {
                    this.readOnly = value;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the cell can be resized.
            /// </summary>
            /// <value>Type: <see cref="System.Boolean">Boolean</see>. <see cref="System.Boolean.True">True</see> if the cell can be resized; otherwise, <see cref="System.Boolean.False">False</see>.</value>
            public override bool Resizable
            {
                get
                {
                    return false;
                }
            }
            #endregion

            #region DataGridViewStatusCell Methods
            /// <summary>
            ///  Paints the current <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see>.
            /// </summary>
            /// <param name="graphics">Type: <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see>. The <see cref="System.Drawing.Graphics">Graphics</see> used to paint the <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see>.</param>
            /// <param name="clipBounds">Type: <see cref="System.Drawing.Rectangle">Rectangle</see>. A <see cref="System.Drawing.Rectangle">Rectangle</see> that represents the area of the <see cref="System.Windows.Forms.DataGridView">DataGridView</see> that needs to be repainted.</param>
            /// <param name="cellBounds">Type: <see cref="System.Drawing.Rectangle">Rectangle</see>. A <see cref="System.Drawing.Rectangle">Rectangle</see> that contains the bounds of the <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see> that is being painted.</param>
            /// <param name="rowIndex">Type: <see cref="System.Int32">Int32</see>. The row index of the cell that is being painted.</param>
            /// <param name="cellState">Type: <see cref="System.Windows.Forms.DataGridViewElementStates">DataGridViewElementStates</see>. A bitwise combination of <see cref="System.Windows.Forms.DataGridViewElementStates">DataGridViewElementStates</see> values that specifies the state of the cell.</param>
            /// <param name="value">Type: <see cref="System.Object">Object</see>. The data of the <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see> that is being painted.</param>
            /// <param name="formattedValue">Type: <see cref="System.Object">Object</see>. The formatted data of the <see cref="System.Windows.Forms.DataGridViewCell">DataGridViewCell</see> that is being painted.</param>
            /// <param name="errorText">Type: <see cref="System.String">String</see>. An error message that is associated with the cell.</param>
            /// <param name="cellStyle">Type: <see cref="System.Windows.Forms.DataGridViewCellStyle">DataGridViewCellStyle</see>. A <see cref="System.Windows.Forms.DataGridViewCellStyle">DataGridViewCellStyle</see> that contains formatting and style information about the cell.</param>
            /// <param name="advancedBorderStyle">Type: <see cref="System.Windows.Forms.DataGridViewAdvancedBorderStyle">DataGridViewAdvancedBorderStyle</see>. A <see cref="System.Windows.Forms.DataGridViewAdvancedBorderStyle">DataGridViewAdvancedBorderStyle</see> that contains border styles for the cell that is being painted.</param>
            /// <param name="paintParts">Type: <see cref="System.Windows.Forms.DataGridViewPaintParts">DataGridViewPaintParts</see>. A bitwise combination of the <see cref="System.Windows.Forms.DataGridViewPaintParts">DataGridViewPaintParts</see> values that specifies which parts of the cell need to be painted.</param>
            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                if (graphics != null)
                {
                    Rectangle r = new Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width - 4, cellBounds.Height - 4);

                    if (value != null)
                    {
                        PortStatus status = (PortStatus)value;

                        switch (status)
                        {
                            case PortStatus.Run:
                                graphics.FillRectangle(Brushes.Green, r);
                                break;
                            case PortStatus.Stopped:
                                graphics.FillRectangle(Brushes.Yellow, r);
                                break;
                            case PortStatus.Error:
                                graphics.FillRectangle(Brushes.Red, r);
                                break;
                            default:
                                graphics.FillRectangle(Brushes.White, r);
                                break;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}