using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SPTMiniLauncher
{
    public partial class Modlist : Form
    {
        public Modlist()
        {
            InitializeComponent();
        }

        private void Modlist_Load(object sender, EventArgs e)
        {
            string[] mods = Directory.GetDirectories(boxServerPlaceholder.Text);
            boxServerList.Items.Clear();

            foreach (string item in mods)
            {
                boxServerList.Items.Add(Path.GetFileName(item));
            }

            boxServerSeparator.Select();
        }

        private void boxServerOption_MouseEnter(object sender, EventArgs e)
        {
            boxServerOption.ForeColor = Color.DodgerBlue;
        }

        private void boxServerOption_MouseLeave(object sender, EventArgs e)
        {
            boxServerOption.ForeColor = Color.LightGray;
        }

        private void boxServerOption_MouseDown(object sender, MouseEventArgs e)
        {

            if ((Control.MouseButtons & MouseButtons.Right) != 0)
            {
                if (boxServerOption.Text.ToLower() == "open")
                {
                    boxServerOption.Text = "Add";
                }
                else if (boxServerOption.Text.ToLower() == "add")
                {
                    boxServerOption.Text = "Remove";
                }
                else if (boxServerOption.Text.ToLower() == "remove")
                {
                    boxServerOption.Text = "Open";
                }
            }
            else
            {
                if (boxServerOption.Text.ToLower() == "add")
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    dialog.IsFolderPicker = true;

                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string fullPath = Path.GetFullPath(dialog.FileName);

                        if (Directory.Exists(fullPath))
                        {

                        }
                    }

                }
                else if (boxServerOption.Text.ToLower() == "remove")
                {


                }
                else if (boxServerOption.Text.ToLower() == "open")
                {
                    if (boxServerList.Items.Count > 0)
                    {
                        try
                        {
                            Process.Start("explorer.exe", Path.Combine(boxServerPlaceholder.Text, boxServerList.Text));
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }

                }
            }
        }

        private void boxServerOption_Click(object sender, EventArgs e)
        {
        }
    }
}
