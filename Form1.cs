using System;
using System.Windows.Forms;
using System.IO;

namespace DescQ
{
    public partial class Form1 : Form
    {
        public static string ControlData = "Hello Control Data";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the ToolTips and associate with the Form container.
            ToolTip toolTip1 = new ToolTip
            {
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button1, "This button activates a command");
            toolTip1.SetToolTip(this.textBox1, "Type the command in here");
            toolTip1.SetToolTip(this.button3, "Open a text file in Notepad");
            toolTip1.SetToolTip(this.button2, "Choose background for the RichTextBox");
        }

        private void Processcommand()
        {
            String action;
            String datapath = Application.UserAppDataPath + "\\";

            action = textBox1.Text;

            switch (action)
            {
                case "cap":
                    if (FormBorderStyle == FormBorderStyle.None)
                    {
                        FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    }
                    else
                    {
                        FormBorderStyle = FormBorderStyle.None;
                    }
                    break;
                case "x":
                    Application.Exit();
                    break;
                case "help":
                    System.Diagnostics.Process.Start("notepad.exe ", datapath + "help.txt");
                    break;
            }

            if (action.StartsWith("data"))
            {
                MessageBox.Show(datapath);
            }
        }

        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Processcommand();
            }
        }

        private void On_Button1_Click(object sender, EventArgs e)
        {
            /*            messagebox.show("this button also activates a command", "did you know",
                                        messageboxbuttons.okcancel, messageboxicon.information);
            */
            Processcommand();
        }

        private void On_TextBox_Enter(object sender, EventArgs e)
        {   // clear the placeholder text
            textBox1.Text = "";
        }

        private void On_Button_ColorChoose(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog1.Color;
            }
        }

        private void On_Button_FileChooser(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        System.Diagnostics.Process.Start("notepad.exe", filePath);
                    }
                }
                catch (System.Security.SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void On_Read_Controls(object sender, EventArgs e)
        {
            /* access the current state of the controls on this form
             and display them in another form */

            // concatenate control values into ControlData public string variable
            string data = "";
            string str;

            if (checkBox1.CheckState == CheckState.Checked)
            {
                data += "Over 18 was Checked";
            } else
            {
                data += "Over 18 NOT checked";
            }
            str = dateTimePicker1.Value.ToString();
            data += Environment.NewLine + str + Environment.NewLine;
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                // append the checked items in the checkedListBox
                data += "Food: " + itemChecked.ToString() + " is checked." + Environment.NewLine;
            }
            if (radioButton1.Checked)
            {
                data += "Sex is Male" + Environment.NewLine;
            }
            if (radioButton2.Checked)
            {
                data += "Sex is Female" + Environment.NewLine;
            }

            data += openFileDialog1.FileName + Environment.NewLine;

            data += "background of RichTextBox: " + richTextBox1.BackColor + Environment.NewLine;

            ControlData = data;

            // show the second form
            Form2 form2 = new Form2();
            form2.Show();
        }
    }

 }
