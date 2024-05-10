namespace TodoListPlus
{
    public partial class AddTodoItemForm : Form
    {
        public string name;
        List<string> TodoLists = new List<string>();


        public AddTodoItemForm(List<string> TodoList, int mode = 0, string? currentName = null)
        {
            InitializeComponent();
            addNewTodoItemErrorLabel.Hide();

            if (mode == 1)
            {
                label1.Text = "Enter a new name for the list";
                addNewTodoItemTextBox.Text = currentName;
                Text = "Rename list";
            }

            this.TodoLists = TodoList;
        }

        private void addNewTodoItemBtn_Click(object sender, EventArgs e)
        {
            name = addNewTodoItemTextBox.Text;

            if (!string.IsNullOrWhiteSpace(name) && !TodoLists.Contains(name))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                addNewTodoItemErrorLabel.Text = "List name cannot be empty!";
                addNewTodoItemErrorLabel.Show();
            }
            else
            {
                addNewTodoItemErrorLabel.Text = "This name is already in use!";
                addNewTodoItemErrorLabel.Show();
            }
        }
    }
}
