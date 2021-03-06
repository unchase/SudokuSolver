﻿using System;
using System.Windows.Input;

namespace Sudoku.ViewModels
{
    internal sealed class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool> canExecute;

        public RelayCommand(Action<object?> execute) : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // when hooked up by wpf add our can execute method to the command 
        // managers event handler instead of this so that the state is 
        // updated when the command manger sees fit
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private static bool DefaultCanExecute(object? _) => true;

        public bool CanExecute(object? param) => canExecute(param);

        public void Execute(object? param) => execute(param);
    }
}
