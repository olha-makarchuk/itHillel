namespace TodoListPlus
{
    partial class AddTodoItemForm
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
            label1 = new Label();
            addNewTodoItemTextBox = new TextBox();
            addNewTodoItemBtn = new Button();
            addNewTodoItemErrorLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(85, 34);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(240, 30);
            label1.TabIndex = 0;
            label1.Text = "Enter name of new item:";
            // 
            // addNewTodoItemTextBox
            // 
            addNewTodoItemTextBox.Location = new Point(16, 85);
            addNewTodoItemTextBox.Margin = new Padding(4, 4, 4, 4);
            addNewTodoItemTextBox.Name = "addNewTodoItemTextBox";
            addNewTodoItemTextBox.Size = new Size(381, 34);
            addNewTodoItemTextBox.TabIndex = 1;
            // 
            // addNewTodoItemBtn
            // 
            addNewTodoItemBtn.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            addNewTodoItemBtn.Location = new Point(148, 150);
            addNewTodoItemBtn.Margin = new Padding(4, 4, 4, 4);
            addNewTodoItemBtn.Name = "addNewTodoItemBtn";
            addNewTodoItemBtn.Size = new Size(103, 57);
            addNewTodoItemBtn.TabIndex = 2;
            addNewTodoItemBtn.Text = "Add";
            addNewTodoItemBtn.UseVisualStyleBackColor = true;
            addNewTodoItemBtn.Click += addNewTodoItemBtn_Click;
            // 
            // addNewTodoItemErrorLabel
            // 
            addNewTodoItemErrorLabel.AutoSize = true;
            addNewTodoItemErrorLabel.ForeColor = Color.Red;
            addNewTodoItemErrorLabel.Location = new Point(85, 253);
            addNewTodoItemErrorLabel.Margin = new Padding(4, 0, 4, 0);
            addNewTodoItemErrorLabel.Name = "addNewTodoItemErrorLabel";
            addNewTodoItemErrorLabel.Size = new Size(276, 30);
            addNewTodoItemErrorLabel.TabIndex = 3;
            addNewTodoItemErrorLabel.Text = "Item name cannot be empty";
            // 
            // AddTodoItemForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 311);
            Controls.Add(addNewTodoItemErrorLabel);
            Controls.Add(addNewTodoItemBtn);
            Controls.Add(addNewTodoItemTextBox);
            Controls.Add(label1);
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddTodoItemForm";
            ShowIcon = false;
            Text = "Add new todo item";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox addNewTodoItemTextBox;
        private Button addNewTodoItemBtn;
        private Label addNewTodoItemErrorLabel;
    }
}