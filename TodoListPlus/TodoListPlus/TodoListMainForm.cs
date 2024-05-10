namespace TodoListPlus
{
    public partial class TodoListMainForm : Form
    {
        public Dictionary<string, List<string>> TodoLists = new Dictionary<string, List<string>>();
        public Dictionary<string, List<bool>> IsComplete = new Dictionary<string, List<bool>>();

        public TodoListMainForm()
        {
            InitializeComponent();
            TodoListsListBox.DataSource = new BindingSource(TodoLists, null);
            TodoListsListBox.DisplayMember = "Key";
            TodoListsListBox.SelectedIndex = -1;
            NoTodoListSelected();
        }

        private void NoTodoListSelected()
        {
            CurrentListLabel.Text = "Choose or create new Todo list";
            DeleteListBtn.Enabled = false;
            RenameBtn.Enabled = false;
            RenameListBtn.Enabled = false;
            RemoveBtn.Enabled = false;
            AddBtn.Enabled = false;
        }

        private void AddTodoList_Click(object sender, EventArgs e)
        {
            CreateNewListForm createNewListForm = new CreateNewListForm(TodoLists.Keys.ToList());
            createNewListForm.ShowDialog();

            if (createNewListForm.DialogResult == DialogResult.OK)
            {
                string newListName = createNewListForm.name;
                TodoLists.Add(newListName, new List<string>());
                IsComplete.Add(newListName, new List<bool>());

                TodoListsListBox.DataSource = null;
                TodoListsListBox.DataSource = new BindingSource(TodoLists, null);
                TodoListsListBox.DisplayMember = "Key";
                TodoListsListBox.SelectedIndex = TodoListsListBox.FindStringExact(newListName);
            }
        }

        private void TodoListsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex == -1)
            {
                NoTodoListSelected();
                return;
            }

            string selectedListName = TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem);
            CurrentListLabel.Text = selectedListName;

            DeleteListBtn.Enabled = true;
            RenameBtn.Enabled = true;
            RenameListBtn.Enabled = true;
            RemoveBtn.Enabled = true;
            AddBtn.Enabled = true;

            // show todo items on UI
            RefreshTodoItems(selectedListName);
        }

        private void RefreshTodoItems(string listName)
        {
            checkedListBoxTodoItems.Items.Clear();
            if (TodoLists.ContainsKey(listName))
            {
                List<string> items = TodoLists[listName];
                List<bool> completionStatus = IsComplete[listName];
                for (int i = 0; i < items.Count; i++)
                {
                    checkedListBoxTodoItems.Items.Add(items[i], completionStatus[i]);
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex != -1)
            {
                AddTodoItemForm addTodoItemForm = new AddTodoItemForm(TodoLists[TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem)]);
                addTodoItemForm.ShowDialog();

                if (addTodoItemForm.DialogResult == DialogResult.OK)
                {
                    TodoLists[TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem)].Add(addTodoItemForm.name);
                    IsComplete[TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem)].Add(false);

                    RefreshTodoItems(TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem));
                }
            }
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex != -1 && checkedListBoxTodoItems.SelectedIndex != -1)
            {
                string selectedListName = TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem);
                int selectedIndex = checkedListBoxTodoItems.SelectedIndex;
                TodoLists[selectedListName].RemoveAt(selectedIndex);
                IsComplete[selectedListName].RemoveAt(selectedIndex);

                RefreshTodoItems(selectedListName);
            }
        }

        private void RenameListBtn_Click(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex != -1)
            {
                string selectedListName = TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem);
                AddTodoItemForm addTodoItemForm = new AddTodoItemForm(TodoLists.Keys.ToList(), 1, selectedListName);
                addTodoItemForm.ShowDialog();

                if (addTodoItemForm.DialogResult == DialogResult.OK)
                {
                    string newName = addTodoItemForm.name;
                    TodoLists[newName] = TodoLists[selectedListName];
                    TodoLists.Remove(selectedListName);
                    IsComplete[newName] = IsComplete[selectedListName];
                    IsComplete.Remove(selectedListName);

                    TodoListsListBox.DataSource = null;
                    TodoListsListBox.DataSource = new BindingSource(TodoLists, null);
                    TodoListsListBox.DisplayMember = "Key";
                    TodoListsListBox.SelectedIndex = TodoListsListBox.FindStringExact(newName);
                }
            }
        }

        private void RenameBtn_Click(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex != -1 && checkedListBoxTodoItems.SelectedIndex != -1)
            {
                string selectedListName = TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem);
                AddTodoItemForm addTodoItemForm = new AddTodoItemForm(TodoLists[selectedListName], 1, TodoLists[selectedListName][checkedListBoxTodoItems.SelectedIndex]);
                addTodoItemForm.ShowDialog();

                if (addTodoItemForm.DialogResult == DialogResult.OK)
                {
                    TodoLists[selectedListName][checkedListBoxTodoItems.SelectedIndex] = addTodoItemForm.name;

                    RefreshTodoItems(selectedListName);
                }
            }
        }

        private void DeleteListBtn_Click(object sender, EventArgs e)
        {
            if (TodoListsListBox.SelectedIndex != -1)
            {
                string selectedListName = TodoListsListBox.GetItemText(TodoListsListBox.SelectedItem);
                TodoLists.Remove(selectedListName);
                IsComplete.Remove(selectedListName);

                RefreshTodoItems(selectedListName);
                NoTodoListSelected();
            }
        }

        private void CurrentListLabel_Click(object sender, EventArgs e)
        {

        }
        private void checkedListBoxTodoItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
