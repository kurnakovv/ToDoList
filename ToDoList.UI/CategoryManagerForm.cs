using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;
using ToDoList.Mapping.Automapper;

namespace ToDoList.UI
{
    public partial class CategoryManagerForm : Form
    {
        private readonly BindingSource _bindingSourceCategories = new BindingSource();
        private readonly BindingSource _bindingSourceCurrentCategory = new BindingSource();
        private readonly ITaskCategoryService _taskCategoryService = new TaskCategoryService(AutomapperConfig.MapConfig());
        private readonly ToDoList _form = new ToDoList();

        public CategoryManagerForm(ToDoList form)
        {
            _form = form;
            InitializeComponent();

            SetBindings();
            LoadCategories();
        }

        private void CategoryManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.MouseClick += (s, a) => SetCurrentCategory();
            dataGridView1.MouseClick += (s, a) => EnableEditPanel();

            if (dataGridView1.CurrentCell != null)
                panel1.Visible = true;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Hide();
            Show();
            Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Enabled = false;
            panel1.Visible = true;
            _bindingSourceCategories.MoveLast();
            _bindingSourceCategories.Add(new TaskCategoryModel());
            _bindingSourceCategories.MoveNext();
            SetCurrentCategory();
            textBox1.Focus();
            dataGridView1.Visible = false;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            TaskCategoryModel newCategory = _bindingSourceCurrentCategory.Current as TaskCategoryModel;
            var oldCategory = _bindingSourceCategories.Current as TaskCategoryModel;
            newCategory.Id = oldCategory.Id;


            if (dataGridView1.CurrentCell.Value == null)
            {
                try
                {
                    _taskCategoryService.AddCategory(newCategory);
                    panel1.Visible = false;
                    MessageBox.Show($"The category \"{newCategory.Title}\" added.");
                }
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataGridView1.Visible = true;
            }
            else
            {
                try
                {
                    _taskCategoryService.UpdateCategory(newCategory);
                    panel1.Visible = false;
                    MessageBox.Show($"The category \"{newCategory.Title}\" updated.");
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

            LoadCategories();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            TaskCategoryModel currentCategory = _bindingSourceCategories.Current as TaskCategoryModel;

            try
            {
                _taskCategoryService.DeleteCategoryById(currentCategory.Id);
                _bindingSourceCategories.Remove(currentCategory);
                MessageBox.Show($"The category \"{currentCategory.Title}\" deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                IEnumerable<TaskCategoryModel> categories = _taskCategoryService.GetCategories();
                _bindingSourceCategories.DataSource = categories;

                // Protection against click on empty DataGridView
                if (_bindingSourceCategories.Count != 0)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetBindings()
        {
            _bindingSourceCategories.DataSource = typeof(IEnumerable<TaskCategoryModel>);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bindingSourceCategories;

            Title.DataPropertyName = nameof(TaskCategoryModel.Title);

            _bindingSourceCurrentCategory.DataSource = new List<TaskCategoryModel> { new TaskCategoryModel() };

            textBox1.DataBindings.Add("Text", _bindingSourceCurrentCategory, nameof(TaskCategoryModel.Title));
            //comboBox1.DataBindings.Add("Text", _bindingSourceCurrentCategory, nameof(TaskCategoryModel.Tasks));
        }

        private void SetCurrentCategory()
        {
            if (_bindingSourceCategories.Count > 0)
            {
                var taskCategory = TaskCategoryModel.GetCloneCategory((TaskCategoryModel)_bindingSourceCategories.Current);
                _bindingSourceCurrentCategory.List[0] = taskCategory;

                comboBox1.Items.Clear();
                foreach(TaskModel item in taskCategory.Tasks)
                {
                    comboBox1.Items.Add(item.Name);
                }
            }
            else
            {
                _bindingSourceCurrentCategory.List[0] = new TaskCategoryModel();
            }
            _bindingSourceCurrentCategory.ResetItem(0);
        }

        private void EnableEditPanel()
        {
            panel1.Visible = true;
        }
    }
}
