public class InputDialog : Form
{
    private TextBox textBoxInput;
    private Label labelPrompt;
    private Button buttonOK;
    private Button buttonCancel;

    public string InputText => textBoxInput.Text;

    public InputDialog(string prompt)
    {
        InitializeComponent(prompt);
    }
    
    private void InitializeComponent(string prompt)
    {
        labelPrompt = new Label();
        labelPrompt.Text = prompt;
        labelPrompt.Location = new System.Drawing.Point(10, 10);
        labelPrompt.AutoSize = true;
        this.Controls.Add(labelPrompt);

        textBoxInput = new TextBox();
        textBoxInput.Location = new System.Drawing.Point(10, 40);
        textBoxInput.Width = 200;
        this.Controls.Add(textBoxInput);

        buttonOK = new Button();
        buttonOK.Text = "OK";
        buttonOK.Location = new System.Drawing.Point(10, 80);
        buttonOK.Click += new EventHandler(buttonOK_Click);
        this.Controls.Add(buttonOK);

        buttonCancel = new Button();
        buttonCancel.Text = "Cancel";
        buttonCancel.Location = new System.Drawing.Point(120, 80);
        buttonCancel.Click += new EventHandler(buttonCancel_Click);
        this.Controls.Add(buttonCancel);

        this.ClientSize = new System.Drawing.Size(230, 120);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.AcceptButton = buttonOK;
        this.CancelButton = buttonCancel;
        this.Text = "Input Dialog";
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}