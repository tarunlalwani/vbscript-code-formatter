using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using TARLABS.VBScriptFormatter.Properties;

namespace TARLABS.VBScriptFormatter
{
    public partial class frmMain : Form
    {
        TokenReader vbScript = new TokenReader();
        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbIndetationCharacter.SelectedIndex = 0;
            txtSourceCode.Language = Language.QTP;
            UpdateText();
        }


        void UpdateText()
        {
            txtSourceCode.Text = txtSourceCode.Text;
        }

        private void chkRemoveComments_CheckedChanged(object sender, EventArgs e)
        {
            vbScript.RemoveComments = chkRemoveComments.Checked;
            UpdateText();
        }

        private void lnkTARLABS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite();
        }

        private string originalFileName;
        private bool isReadOnly;

        private void LaunchWebSite()
        {
            Process.Start("http://www.tarlabs.com/");
        }


        private Image GetImageForTextBox(FastColoredTextBox box)
        {
            FastColoredTextBox boxClone = new FastColoredTextBox();

            var maxLineLength = box.Lines.Max((line) => line.Length);

            boxClone.ShowLineNumbers = box.ShowLineNumbers;

            boxClone.ShowScrollBars = false;
            boxClone.Language = Language.QTP;
            boxClone.Text = box.Text;
            boxClone.Width = (int) (maxLineLength * 1.25 * box.CharWidth);
            boxClone.Height = box.TextHeight + box.Lines.Count + 10;
            int width, height;
            width = boxClone.Width;
            height = boxClone.Height;

            Bitmap bp = new Bitmap(width + 10, height);
            Graphics g = Graphics.FromImage(bp);
            g.Clear(box.BackColor);
            g.Flush();
            g.Dispose();

            try
            {
                boxClone.DrawToBitmap(bp, new Rectangle(10, 0, width + 10, height));
            }
            catch (Exception)
            {
                bp.Dispose();
                return null;
            }
            finally
            {
                boxClone.Dispose();
            }

            return bp;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnInstallUFTPlugin_Click(object sender, EventArgs e)
        {
            Status("Not available in open source version");
        }

        private void iMAGEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status("Capturing Image to clipboard...");
            Update();
            var img = GetImageForTextBox(txtSourceCode);
            if (img == null)
            {
                ImageTooLarge();
                return;
            }

            Clipboard.SetData(DataFormats.Bitmap, img);
            img.Dispose();
            Status("Image copied to clipboard");
        }

        private void ImageTooLarge()
        {
            StatusError("The source code image is too large to be exported. Please shorten the source code before exporting");
        }

        private void StatusError(string text)
        {
            Status(text);
            statusBarInfo.ForeColor = Color.Red;
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Html, txtSourceCode.Html);
            Status("Copied to clipboard Html");
        }

        private void rTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Rtf, txtSourceCode.Rtf);
            Status("Copied to clipboard Rtf");
        }

        private void tEXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, txtSourceCode.Text);
            Status("Copied to clipboard text");
        }

        private void iMAGEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Status("Capturing image to file...");
            string fileName = GetSaveFileName("PNG File|*.png").FileName;

            if (fileName == "")
                return;

            var img = GetImageForTextBox(txtSourceCode);

            if (img == null)
            {
                ImageTooLarge();
                return;
            }

            img.Save(fileName, ImageFormat.Png);
            img.Dispose();
            //NeedPaidVersion();
            Status("Image captured to file - " + fileName);
        }

        private void visitWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchWebSite();
        }


        private SaveFileDialog GetSaveFileName(string fileTypes)
        {
            SaveFileDialog dialog = new SaveFileDialog
                {
                    Title = "Save source code as...",
                    CheckPathExists = true,
                    OverwritePrompt = true,
                    Filter = fileTypes,
                    FilterIndex = 0,
                    RestoreDirectory = true
                };

            dialog.ShowDialog(this);

            return dialog;
        }

        private OpenFileDialog GetOpenFileName(string fileTypes)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Open source code from...",
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = fileTypes,
                ReadOnlyChecked = false,
                ShowReadOnly = true,
                FilterIndex = 0,
                RestoreDirectory = true
            };

            dialog.ShowDialog(this);

            return dialog;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog =
                GetOpenFileName(
                    "All Files (*.txt, *.qfl, *.vbs)|*.txt;*.qfl;*.vbs|Text Files (*.txt)|*.txt|QuickTest Function Library (*.qfl)|*.qfl|VBScript files (*.vbs)|*.vbs");
            if (dialog.FileName == "")
                return;

            isReadOnly = dialog.ReadOnlyChecked || File.GetAttributes(dialog.FileName).HasFlag(FileAttributes.ReadOnly);

            saveToolStripMenuItem.Enabled = !isReadOnly;
            saveAsToolStripMenuItem.Enabled = true;
            originalFileName = dialog.FileName;

            TextReader reader = new StreamReader(originalFileName);
            txtSourceCode.Text = reader.ReadToEnd();
            reader.Dispose();

            Status("Open file completed - " + originalFileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalFileName == "")
                return;

            SaveCodeToFile(originalFileName, txtSourceCode.Text);
            txtSourceCode.Text = txtSourceCode.Text;
            Status("Save to file completed - " + originalFileName);
        }

        private void SaveCodeToFile(string fileName, string code)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.Write(code);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName =
                GetSaveFileName(
                    "VBScript Files (*.txt, *.qfl, *.vbs)|*.txt;*.qfl;*.vbs|All Files (*.*)|*.*")
                    .FileName;
            if (fileName == "") return;

            SaveCodeToFile(fileName, txtSourceCode.Text);
            txtSourceCode.Text = txtSourceCode.Text;
            originalFileName = fileName;
            Status("Save As to file completed - " + fileName);
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName("HTML File|*.html").FileName;

            if (fileName == "") return;

            SaveCodeToFile(fileName, txtSourceCode.Html);
            Status("Save to HTML file completed - " + fileName);
        }

        private void rTFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName("RTF File|*.rtf").FileName;

            if (fileName == "")
                return;

            SaveCodeToFile(fileName, txtSourceCode.Rtf);
            Status("Save to Rtf file completed - " + fileName);
        }

        private void tEXTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName("Text File|*.txt").FileName;

            if (fileName == "")
                return;

            SaveCodeToFile(fileName, txtSourceCode.Text);
            Status("Save to text file completed - " + fileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowLineNumbers_CheckedChanged(object sender, EventArgs e)
        {
            txtSourceCode.ShowLineNumbers = chkShowLineNumbers.Checked;
        }

        private void LoadStats()
        {
            vbScript.LoadStats();
            lblBlankLines.Text = "Blank Lines: " + vbScript.BlankLines.ToString();
            lblClasses.Text = "Classes: " + vbScript.Classes.ToString();
            lblComments.Text = "Comments: " + vbScript.Comments.ToString();
            lblConstants.Text = "Constants: " + vbScript.ConstVariables.ToString();
            lblFunctions.Text = "Functions: " + vbScript.Functions.ToString();
            lblVariables.Text = "Variables: " + vbScript.Variables.ToString();
        }

        private void formatCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Status("Started source code formatting...");
                errProvider.Clear();
                var formattedText = vbScript.FormatCode(txtSourceCode.Text);
                txtSourceCode.Text = formattedText;
                LoadStats();
                Status("Source code formatting completed");
            }
            catch (Exception)
            {
               StatusError("Error occured while formatting source code...");
            }
        }

        private void txtSourceCode_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            errProvider.Clear();
        }

        private void cmbIndetationCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbIndetationCharacter.Text)
            {
                case "1 Tab":
                    vbScript.IndentationString = "\t";
                    break;
                case "2 Tabs":
                    vbScript.IndentationString = "\t\t";
                    break;
                case "1 Space":
                    vbScript.IndentationString = " ";
                    break;
                case "2 Spaces":
                    vbScript.IndentationString = "  ";
                    break;
                case "3 Spaces":
                    vbScript.IndentationString = "   ";
                    break;
                case "4 Spaces":
                    vbScript.IndentationString = "    ";
                    break;

            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSourceCode.SelectAll();
            Status("Select All completed");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSourceCode.Copy();
            Status("Copy operation completed");
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSourceCode.Cut();
            Status("Cut operation completed");
        }

        private void Status(string text)
        {
            statusBarInfo.ForeColor = Color.Black;
            statusBarInfo.Text = text;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.IdentationCharacter = cmbIndetationCharacter.Text;
            Settings.Default.ShowLineNumbers = chkShowLineNumbers.Checked;
            Settings.Default.RemoveComments = chkRemoveComments.Checked;
            Settings.Default.Save();
        }

    }
}
