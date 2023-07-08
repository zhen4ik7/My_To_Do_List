using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfApp1
{
    public partial class NewWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool showAll;

        [ObservableProperty]
        private bool showComleted;

        [ObservableProperty]
        private bool showToDoOnly;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveToDoItemCommand))]
        private ToDoItem? selectedItem;

        [ObservableProperty]
        private ObservableCollection<ToDoItem>? toDoListItems;

        [ObservableProperty]
        private ObservableCollection<ToDoItem>? toDoListSelectedItems;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddNewToDoItemCommand))]
        private string? newToDoText;

        [RelayCommand(CanExecute = nameof(CanAddNewToDoItem))]
        private void AddNewToDoItem()
        {
            ToDoListItems?.Add(new ToDoItem(NewToDoText));
        }
        private bool CanAddNewToDoItem()
        {
            return !string.IsNullOrEmpty(NewToDoText);
        }

        public RelayCommand RemoveToDoItemCommand { get; }
        public RelayCommand UpdateSelectedListCommand { get; }

        public NewWindowViewModel()
        {
            ShowAll = true;
            ToDoListItems = new ObservableCollection<ToDoItem>();
            ToDoListItems?.Add(new ToDoItem("Comleted", true));
            RemoveToDoItemCommand = new RelayCommand(RemoveToDoItem, () => SelectedItem is not null);
            UpdateSelectedListCommand = new RelayCommand(UpdateSelectedList);
        }

        private void RemoveToDoItem()
        {
            ToDoListItems?.Remove(SelectedItem);
        }

        private void UpdateSelectedList()
        {
            ToDoListSelectedItems = ToDoListItems;
            if (!ShowAll)
            {
                var filtredList = ToDoListSelectedItems?.Where(x => x.IsCompleted == ShowComleted);
                ToDoListSelectedItems = filtredList == null ? new ObservableCollection<ToDoItem>() : new ObservableCollection<ToDoItem>(filtredList);
            }
        }
        partial void OnToDoListItemsChanged(ObservableCollection<ToDoItem>? value)
        {
            UpdateSelectedList();
        }
    }
}
