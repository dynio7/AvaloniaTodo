using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using TodoListup.Models;
using TodoListup.Services;

namespace TodoListup.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        
        public ReactiveCommand<TodoItem, Unit> DeleteItemButton { get; }
        
        public ViewModelBase content;
        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());

            var service = new Database();
            List = new TodoListViewModel(service.GetItems());
            content = List;
            DeleteItemButton = ReactiveCommand.Create<TodoItem>(DeleteItem);
        }

        private void DeleteItem(TodoItem item)
        {
            List.Items.Remove(item);
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });
            Content = vm;
        }
    }
}