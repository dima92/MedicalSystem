using NeuralNetworks;
using System;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pictureConverter = new PictureConverter();
                var inputs = pictureConverter.Convert(openFileDialog.FileName);
                var result = Program.Controller.ImageNetwork.Predict(inputs).Output;
            }
        }
        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enterdataForm = new EnterData();
            var result = enterdataForm.ShowForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
