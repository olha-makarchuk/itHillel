namespace TodoListPlus
{
    partial class ConfirmForm
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
            confirmDeleteListLabel = new Label();
            confirmDeleteTodoItemsNumberLabel = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(259, 19);
            label1.TabIndex = 0;
            label1.Text = "Are you sure you want to delete this list?";
            // 
            // confirmDeleteListLabel
            // 
            confirmDeleteListLabel.AutoSize = true;
            confirmDeleteListLabel.Location = new Point(51, 47);
            confirmDeleteListLabel.Name = "confirmDeleteListLabel";
            confirmDeleteListLabel.Size = new Size(130, 19);
            confirmDeleteListLabel.TabIndex = 1;
            confirmDeleteListLabel.Text = "List: <Name of list>";
            confirmDeleteListLabel.Click += confirmDeleteListLabel_Click;
            // 
            // confirmDeleteTodoItemsNumberLabel
            // 
            confirmDeleteTodoItemsNumberLabel.AutoSize = true;
            confirmDeleteTodoItemsNumberLabel.Location = new Point(51, 86);
            confirmDeleteTodoItemsNumberLabel.Name = "confirmDeleteTodoItemsNumberLabel";
            confirmDeleteTodoItemsNumberLabel.Size = new Size(88, 19);
            confirmDeleteTodoItemsNumberLabel.TabIndex = 2;
            confirmDeleteTodoItemsNumberLabel.Text = "Items: <No>";
            // 
            // ConfirmForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(280, 144);
            Controls.Add(confirmDeleteTodoItemsNumberLabel);
            Controls.Add(confirmDeleteListLabel);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfirmForm";
            ShowIcon = false;
            Text = "Confirm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label confirmDeleteListLabel;
        private Label confirmDeleteTodoItemsNumberLabel;
    }
}