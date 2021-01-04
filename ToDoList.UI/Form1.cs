using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDoList.Domain.Models;
using ToDoList.Domain.Services;
using ToDoList.Domain.Services.Abstract;

namespace ToDoList.UI
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSourceTasks = new BindingSource();
        private readonly BindingSource _bindingSourceCurrentTask = new BindingSource();
        private readonly ITaskService _taskService = new TaskService();
        
        public Form1()
        {
            InitializeComponent();

            SetBindings();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MouseClick += (s, a) => SetCurrentTask();
            LoadTasks();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            _bindingSourceTasks.MoveLast();
            _bindingSourceTasks.Add(new TaskModel());
            _bindingSourceTasks.MoveNext();
            SetCurrentTask();
            textBox1.Focus();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            TaskModel currentTask = _bindingSourceCurrentTask.Current as TaskModel;
            TaskModel currentTasks = _bindingSourceTasks.Current as TaskModel;

            if (dataGridView1.CurrentCell.Value == null)
            {
                _taskService.AddTask(currentTask);
            }
            else
            {
                //_taskService.UpdateTask(currentTasks);
                _taskService.DeleteTaskById(currentTasks.Id);
                _taskService.AddTask(currentTask);
                // TODO: Edited exception when we use method UpdateTask
            }

            LoadTasks();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            TaskModel currentTask = _bindingSourceTasks.Current as TaskModel;

            if (currentTask != null)
            {
                if (currentTask.Id != null)
                {
                    _taskService.DeleteTaskById(currentTask.Id);
                    _bindingSourceTasks.Remove(currentTask);
                }
            }
        }

        private void LoadTasks()
        {
            IEnumerable<TaskModel> tasks = _taskService.GetAllTasks();
            _bindingSourceTasks.DataSource = tasks;
            SetCurrentTask();
        }

        private void SetCurrentTask()
        {
            if (_bindingSourceTasks.Count > 0)
            {
                _bindingSourceCurrentTask.List[0] = TaskModel.GetCloneTask((TaskModel)_bindingSourceTasks.Current);
            }
            else
            {
                _bindingSourceCurrentTask.List[0] = new TaskModel();
            }
            _bindingSourceCurrentTask.ResetItem(0);
        }

        private void SetBindings()
        {
            _bindingSourceTasks.DataSource = typeof(IEnumerable<TaskModel>);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _bindingSourceTasks;

            Column1.DataPropertyName = nameof(TaskModel.Name);
            Column2.DataPropertyName = nameof(TaskModel.Description);
            Column3.DataPropertyName = nameof(TaskModel.DateTime);

            _bindingSourceCurrentTask.DataSource = new List<TaskModel> { new TaskModel() };

            textBox1.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.Name));
            textBox2.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.DateTime));
            textBox3.DataBindings.Add("Text", _bindingSourceCurrentTask, nameof(TaskModel.Description));
        }
    }
}
