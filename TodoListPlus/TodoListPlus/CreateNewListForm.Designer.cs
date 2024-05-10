namespace TodoListPlus
{
    partial class CreateNewListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addTodoListLabel = new Label();
            AddTodoListTextBox = new TextBox();
            AddTodoListBtn = new Button();
            addTodoListErrorLabel = new Label();
            SuspendLayout();
            // 
            // addTodoListLabel
            // 
            addTodoListLabel.AutoSize = true;
            addTodoListLabel.Location = new Point(47, 9);
            addTodoListLabel.Name = "addTodoListLabel";
            addTodoListLabel.Size = new Size(185, 19);
            addTodoListLabel.TabIndex = 0;
            addTodoListLabel.Text = "Enter a name for the new list";
            // 
            // AddTodoListTextBox
            // 
            AddTodoListTextBox.Location = new Point(12, 43);
            AddTodoListTextBox.Name = "AddTodoListTextBox";
            AddTodoListTextBox.Size = new Size(256, 26);
            AddTodoListTextBox.TabIndex = 1;
            // 
            // AddTodoListBtn
            // 
            AddTodoListBtn.Location = new Point(91, 75);
            AddTodoListBtn.Name = "AddTodoListBtn";
            AddTodoListBtn.Size = new Size(75, 23);
            AddTodoListBtn.TabIndex = 2;
            AddTodoListBtn.Text = "Add";
            AddTodoListBtn.UseVisualStyleBackColor = true;
            AddTodoListBtn.Click += AddTodoListBtn_Click;
            // 
            // addTodoListErrorLabel
            // 
            addTodoListErrorLabel.AutoSize = true;
            addTodoListErrorLabel.ForeColor = Color.Red;
            addTodoListErrorLabel.Location = new Point(47, 112);
            addTodoListErrorLabel.Name = "addTodoListErrorLabel";
            addTodoListErrorLabel.Size = new Size(173, 19);
            addTodoListErrorLabel.TabIndex = 3;
            addTodoListErrorLabel.Text = "That name is alreasy in use";
            // 
            // CreateNewListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(280, 150);
            Controls.Add(addTodoListErrorLabel);
            Controls.Add(AddTodoListBtn);
            Controls.Add(AddTodoListTextBox);
            Controls.Add(addTodoListLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateNewListForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create New List";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label addTodoListLabel;
        private TextBox AddTodoListTextBox;
        private Button AddTodoListBtn;
        private Label addTodoListErrorLabel;
    }
}