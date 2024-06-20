using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRA.ModelLayer.Core;

namespace CRA.ModelLayer.DCC
{
    public partial class TypeSelection : Form
    {
        public TypeSelection()
        {
            InitializeComponent();

            listBoxTypes.Items.AddRange(VarInfoValueTypes.Values.Select(vt => vt.Name).ToArray());
        }

        private VarInfoValueTypes valueType;

        internal TextBox TypeTextBox;

        

        private void TypeSelection_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (valueType.RequiresSizeInTypeDefinition)
            {
                textBoxType.Text = valueType.ParsingPrefix + textBoxSize.Text.Trim() + valueType.ParsingPostfix;
            }
            TypeTextBox.Text = textBoxType.Text;
        }

        private void listBoxTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            valueType = VarInfoValueTypes.Values.Where(vt => vt.Name.Equals((string)listBoxTypes.SelectedItem)).Single();
            textBoxSize.Enabled = valueType.RequiresSizeInTypeDefinition;
            textBoxType.Text = valueType.ParsingPrefix + valueType.ParsingPostfix;
            if (!valueType.RequiresSizeInTypeDefinition)
            {
                this.Close();
            }
        }

        private void TypeSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBoxType.Text == null || textBoxType.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please chose a type", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else if (valueType.RequiresSizeInTypeDefinition)
            {
                bool showError = false;
                if (textBoxSize.Text == null || textBoxSize.Text.Trim().Equals(""))
                {
                    showError = true;
                }
                else
                {
                    try
                    {
                        int.Parse(textBoxSize.Text.Trim());
                    }
                    catch (FormatException)
                    {
                        showError = true;
                    }
                }
                if (showError)
                {
                    MessageBox.Show("Please chose a valid size for the type", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
            }
        }
    }
}
