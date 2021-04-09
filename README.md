## ToDoList

## Description
> This program is designed to store a list of tasks. Instead of keeping all your tasks on paper, you can use this program.

![MainMenu](ImgForReadme/MainMenu.png)

## Table of contents
* [Technologies](#technologies)
* [Lite UML](#lite-uml)
* [Layers](#layers)
* [How to start](#how-to-start)
* [Versions](#versions)
* [How to participate](#how-to-participate)
* [Features](#features)
* [To Do](#to-do)
* [Special thanks to](#special-thanks-to)

## Technologies
- Windows Forms
- EntityFramework - v6.4.4
- AutoMapper - v10.1.1
- MsTest - v2.1.1
- Moq - v4.15.2

## Lite UML
![LiteUML](ImgForReadme/LiteUML.png)

## Layers
I use three-tiered architecture:
- ToDoList.Data - layer for data storage
- ToDoList.Domain - later for BL
- ToDoList.Test - layer for tests
- ToDoList.Mapping - layer for mapping objects and models with each other
- ToDoList.UI - layer for user intrerface (WF)

## How to start
- Go to ToDoList.Data/App.config
- Change path in connectionStrings:
``` XML
<connectionStrings>
    <add name = "TaskDbContext"
         providerName="System.Data.SqlClient"
         connectionString="Server=(LocalDB)\MSSQLLocalDB;
                           Database=ToDoList;
                           Integrated Security=True;"/> <!-- change here. -->
  </connectionStrings>
```

- If you need change DB name go to ToDoList.Data/Context/TaskDbContext.cs:
``` CS
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("ToDoList") { } // change here.
        public DbSet<TaskEntity> Tasks { get; set; }
    }
```

``` CS
    internal class TaskCategoryDbContext : DbContext
    {
        public TaskCategoryDbContext() : base("ToDoList") { }
        public DbSet<TaskCategoryEntity> TaskCategories { get; set; }
    }
```

## Versions
- v1.0.0 - CRUD functional with user-friendly interface.
- v1.0.1 - Added table of contents.
- v1.0.2 - Edited task update system.
- v2.0.0 - Add category management, task search, marking all completed tasks

## How to participate
throw all your request in "pullRequests" branch

## Features
- CRUD functional
- User-friendly interface
- Category management
- Task menu
- Marking all completed tasks
- Task search

## To Do
- Empty

## Status
Project finished, accept pull requests

## Contributors:
[![KurnakovMaksim](https://avatars.githubusercontent.com/u/59327306?v=3&s=100)](https://github.com/KurnakovMaksim) | <img src="https://avatars.githubusercontent.com/u/66691708" width="100" height="100"/> |
--- | --- |
[KurnakovMaksim](https://github.com/KurnakovMaksim) | [Ulyanov-programmer](https://github.com/Ulyanov-programmer)

[List Contributors](https://github.com/KurnakovMaksim/ToDoList/graphs/contributors)

## Connect with me
If you want to improve something or fix a bug, you can send me an email maxmamamama2003@gmail.com
