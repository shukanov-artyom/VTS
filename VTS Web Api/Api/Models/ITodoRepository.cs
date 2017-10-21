﻿using System;
using System.Collections.Generic;

namespace test.Models
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);

        IEnumerable<TodoItem> GetAll();

        TodoItem Find(string key);

        TodoItem Remove(string key);

        void Update(TodoItem item);
    }
}
