using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class EnterData : Form
    {
        private List<TextBox> Inputs = new List<TextBox>();
        public EnterData()
        {
            InitializeComponent();
            var propInfo = typeof(Patient).GetProperties();
            for (var i = 0; i < propInfo.Length; i++)
            {
                var property = propInfo[i];
                var textbox = CreateTextBox(i, property);
                Controls.Add(textbox);
                Inputs.Add(textbox);
            }
        }
        public bool? ShowForm()
        {
            var form = new EnterData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var patient = new Patient();
                foreach (var textbox in form.Inputs)
                {
                    patient.GetType().InvokeMember(textbox.Tag.ToString(),
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                        Type.DefaultBinder, patient, new object[] { textbox.Text });
                }
                // var result = Program.Controller.DataNetwork.Predict()?.Output;
                return false;
            }
            return null;
        }
        private void EnterData_Load(object sender, EventArgs e)
        {
        }
        private TextBox CreateTextBox(int number, PropertyInfo property)
        {
            var y = number * 25 + 12;
            var textbox = new TextBox
            {
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                Location = new Point(12, y),
                Name = "textBox" + number,
                Size = new Size(335, 20),
                TabIndex = number,
                Text = property.Name,
                Tag = property.Name,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 204),
                ForeColor = Color.Gray
            };
            textbox.GotFocus += Textbox_GotFocus;
            textbox.LostFocus += Textbox_LostFocus;

            return textbox;
        }
        private void Textbox_LostFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 204);
                textBox.ForeColor = Color.Gray;
            }
        }

        private void Textbox_GotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
                textBox.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
