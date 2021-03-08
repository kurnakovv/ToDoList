using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;
using ToDoList.Mapping.Automapper;

namespace ToDoList.UI
{
    public partial class ToDoList : Form
    {
        private readonly BindingSource _bindingSourceTasks = new BindingSource();
        private readonly BindingSource _bindingSourceCurrentTask = new BindingSource();
        private readonly ITaskService _taskService = new TaskService(AutomapperConfig.MapConfig());
        private readonly ITaskCategoryService _taskCategoryService = new TaskCategoryService(AutomapperConfig.MapConfig());

        public ToDoList()
        {
            InitializeComponent();

            SetBindings();
            LoadTasks();
            LoadCategories();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MouseClick += (s, a) => SetCurrentTask();
            dataGridView1.MouseClick += (s, a) => EnableEditPanel();
            taskCategoryToolStripMenuItem.DropDownItemClicked += (s, a) => SortByCategory(a.ClickedItem.Text);
            taskCategoryToolStripMenuItem.Click += (s, a) => LoadTasks();
        }

        private void LoadCategories()
        {
            var categories = _taskCategoryService.GetCategories();
            comboBox1.DataSource = categories;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "Id";

            int id = 0;

            foreach (TaskCategoryModel item in categories)
            {
                ToolStripMenuItem itemToolStrip = new ToolStripMenuItem(item.Title);
                itemToolStrip.Tag = id;
                id++;

                taskCategoryToolStripMenuItem.DropDownItems.Add(itemToolStrip);
            }
        }

        private void SortByCategory(string currentItemInMenu)
        {
            try
            {
                _bindingSourceTasks.DataSource = _taskService.SortTasksByCategory(currentItemInMenu);
            }
            catch(System.Data.Entity.Core.ObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Enabled = false;
            panel1.Visible = true;
            _bindingSourceTasks.MoveLast();
            _bindingSourceTasks.Add(new TaskModel());
            _bindingSourceTasks.MoveNext();
            SetCurrentTask();
            textBox1.Focus();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            TaskModel newTask = _bindingSourceCurrentTask.Current as TaskModel;
            var oldTask = _bindingSourceTasks.Current as TaskModel;

            // For a new Task, a new Id is created, this should not be the case, 
            // for new task the previous Id from oldTask.
            newTask.Id = oldTask.Id;

            if (comboBox1.SelectedItem != null)
            {
                // Give id the current item in combobox.
                newTask.CategoryId = comboBox1.SelectedValue.ToString();
            }

            if (dataGridView1.CurrentCell.Value == null)
            {
                try
                {
                    _taskService.AddTask(newTask);
                    panel1.Visible = false;
                    MessageBox.Show($"The task \"{newTask.Name}\" added.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    _taskService.UpdateTask(newTask);
                    panel1.Visible = false;
                    MessageBox.Show($"The task \"{newTask.Name}\" updated.");
                }
                catch (System.Data.Entity.Core.ObjectNotFoundException ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            LoadTasks();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            TaskModel currentTask = _bindingSourceTasks.Current as TaskModel;

            try
            {
                _taskService.DeleteTaskById(currentTask.Id);
                _bindingSourceTasks.Remove(currentTask);
                MessageBox.Show($"The task \"{currentTask.Name}\" deleted.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadTasks();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    var tasks = _taskService.GetTasksByName(textBox4.Text);
                    _bindingSourceTasks.DataSource = tasks;
                }
                else
                {
                    LoadTasks();
                }
            }
            catch(System.Data.Entity.Core.ObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox4.Text = string.Empty;
        }

        private void LoadCompletenessBtn_Click(object sender, EventArgs e)
        {
            LoadColorsInTasks();
        }

        private void LoadTasks()
        {
            try
            {
                IEnumerable<TaskModel> tasks = _taskService.GetAllTasks();
                _bindingSourceTasks.DataSource = tasks;
                
                // Protection against click on empty DataGridView
                if (_bindingSourceTasks.Count != 0)
                {
                    dataGridView1.Enabled = true;
                }
                else
                {
                    dataGridView1.Enabled = false;
                    panel1.Visible = false;
                }

                AddBtn.Enabled = true;
            }
            catch(Exception)
            {
                MessageBox.Show("Database not connected");
            }
        }

        private void LoadColorsInTasks()
        {
            var row = 0;
            foreach (TaskModel t in _taskService.GetAllTasks())
            {
                var currentRow = dataGridView1.Rows[row];

                if (t.Completeness == true)
                {
                    currentRow.DefaultCellStyle.ForeColor = Color.Green;
                }
                else
                {
                    currentRow.DefaultCellStyle.ForeColor = Color.Black;
                }
                row++;
            }
            row = 0;
        }

        

        private void SetCurrentTask()
        {
            if (_bindingSourceTasks.Count > 0)
            {
                TaskModel currentSelectedTask = TaskModel.GetCloneTask((TaskModel)_bindingSourceTasks.Current);
                _bindingSourceCurrentTask.List[0] = currentSelectedTask;

                if (currentSelectedTask.Category != null)
                {
                    comboBox1.Text = currentSelectedTask.Category.Title;
                }
            }
            else
            {
                _bindingSourceCurrentTask.List[0] = new TaskModel();
            }
            _bindingSourceCurrentTask.ResetItem(0);
            panel1.Visible = true;
        }

        private void EnableEditPanel()
        {
            panel1.Visible = true;
        }

        private void SetBindings()
        {
            _bindingSourceTasks.DataSource = typeof(IEnumerable<TaskModel>);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bindingSourceTasks;

            Column1.DataPropertyName = nameof(TaskModel.Name);
            Column2.DataPropertyName = nameof(TaskModel.Description);
            Column3.DataPropertyName = nameof(TaskModel.DateTime);
            Column4.DataPropertyName = nameof(TaskModel.Completeness);
            Column5.DataPropertyName = nameof(TaskModel.CategoryId);
            Column5.Visible = false;

            _bindingSourceCurrentTask.DataSource = new List<TaskModel> { new TaskModel() };

            textBox1.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.Name));
            textBox2.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.DateTime));
            textBox3.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.Description));
            comboBox1.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.CategoryId));
        }

        private void CategoryManagerBtn_Click(object sender, EventArgs e)
        {
            var form = new CategoryManagerForm(this);
            form.ShowDialog();
        }

        private void ReloadCategories_Btn_Click(object sender, EventArgs e)
        {
            taskCategoryToolStripMenuItem.DropDownItems.Clear();
            LoadCategories();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ignore clicks that are not on button cells.
                if (e.RowIndex < 0 || e.ColumnIndex !=
                    dataGridView1.Columns[Column4.Name].Index) return;

                var task = _bindingSourceTasks.Current as TaskModel;

                // Old completeness.
                bool completeness = (bool)dataGridView1[4, e.RowIndex].Value;


                var currentRow = dataGridView1.Rows[e.RowIndex];

                // Change the currently selected completeness for new value.
                if (completeness == false)
                {
                    completeness = true;
                    currentRow.DefaultCellStyle.ForeColor = Color.Green;
                    MessageBox.Show("Congratulations!");
                }
                else
                {
                    completeness = false;
                    currentRow.DefaultCellStyle.ForeColor = Color.Black;
                }

                _taskService.UpdateCompleteness(task, completeness);
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
