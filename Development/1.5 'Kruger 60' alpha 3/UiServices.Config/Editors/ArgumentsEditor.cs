// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Common.Controls;
using Project.DvbIpTv.UiServices.Configuration.Settings.TvPlayers;

namespace Project.DvbIpTv.UiServices.Configuration.Editors
{
    public partial class ArgumentsEditor : UserControl
    {
        private ListItemsManager<string> ItemsManager;

        public ArgumentsEditor()
        {
            InitializeComponent();
        } // constructor

        public string[] Arguments
        {
            get
            {
                var arguments = new string[listArguments.Items.Count];
                for (int index = 0; index < arguments.Length; index++)
                {
                    arguments[index] = listArguments.Items[0].ToString();
                } // for

                return arguments;
            }
            set
            {
                if (value != null)
                {
                    listArguments.Items.AddRange(value);
                }
                else
                {
                    listArguments.Items.Clear();
                } // if-else
            } // set
        } // Arguments

        public bool IsDataChanged
        {
            get;
            private set;
        } // IsDataChanged

        private void ArgumentsEditor_Load(object sender, EventArgs e)
        {
            ItemsManager = new ListItemsManager<string>(listArguments, buttonRemove, buttonMoveUp, buttonMoveDown);
            listArguments.DisplayMember = null;
            listArguments.ValueMember = null;
            buttonEdit.Enabled = false;
        } // ArgumentsEditor_Load

        private void listArguments_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEdit.Enabled = (listArguments.SelectedIndex >= 0);
        } // listArguments_SelectedIndexChanged

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //TODO: implement
            MessageBox.Show(this, "Not yet implemented!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        } // buttonEdit_Click

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            ItemsManager.RemoveSelection();
            IsDataChanged = true;
        } // buttonRemove_Click

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //TODO: implement
            MessageBox.Show(this, "Not yet implemented!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //IsDataChanged = true;
        } // buttonAdd_Click

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionUp();
            IsDataChanged = true;
        } // buttonMoveUp_Click

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionDown();
            IsDataChanged = true;
        } // buttonMoveDown_Click

    } // class ArgumentsEditor
} // namespace
