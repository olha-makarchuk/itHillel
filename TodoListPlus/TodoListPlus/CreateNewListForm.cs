namespace TodoListPlus
{
    public partial class CreateNewListForm : Form
    {
        public string name;
        List<string> TodoLists = new List<string>();

        public CreateNewListForm(List<string> TodoList, int mode = 0, string? currentName = null)
        {
            InitializeComponent();
            addTodoListErrorLabel.Hide();

            if (mode == 1)
            {
                addTodoListLabel.Text = "Enter a new name for the list";
                AddTodoListTextBox.Text = currentName;
                Text = "Rename list";
            }

            this.TodoLists = TodoList;
        }

        private void AddTodoListBtn_Click(object sender, EventArgs e)
        {
            name = AddTodoListTextBox.Text;

            if(!string.IsNullOrWhiteSpace(name) && !TodoLists.Contains(name)) 
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                addTodoListErrorLabel.Text = "List name cannot be empty!";
                addTodoListErrorLabel.Show();
            }
            else
            {
                addTodoListErrorLabel.Text = "This name is already in use!";
                addTodoListErrorLabel.Show();
            }
        }
    }
}
