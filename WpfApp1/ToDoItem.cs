using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp1
{
    public partial class ToDoItem : ObservableObject
    {
        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private bool? isCompleted;

        public ToDoItem(string? _name, bool? _isCompleted = false) 
        {
            Name = _name;
            IsCompleted = _isCompleted; 
        }
    }
}
