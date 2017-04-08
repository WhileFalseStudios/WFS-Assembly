/* -------------------------------------------------------------------
*   Copyright © 2017 While False Studios
*   CodeForm.cs created by Andrew on 2017-4-7 
*   Purpose: Code editor
--------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AssemblyTester
{
    public delegate void EditorClosed(string finalCode);

    public partial class CodeForm : Form
    {
        string code;

        public EditorClosed OnEditorClosed;

        public CodeForm(string existingCode)
        {
            InitializeComponent();
            code = existingCode;
        }

        private void CodeForm_Load(object sender, EventArgs e)
        {
            textBoxCode.Text = code;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            OnEditorClosed(textBoxCode.Text);
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxCode.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, textBoxCode.Text);
            }
        }
    }
}
