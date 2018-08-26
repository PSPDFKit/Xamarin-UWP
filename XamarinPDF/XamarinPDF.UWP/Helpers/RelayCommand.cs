//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Windows.Input;

namespace XamarinPDF.UWP.Helpers {
	public class RelayCommand : ICommand {
		readonly Func<bool> _canExecute;
		readonly Action _execute;

		public RelayCommand (Action execute) : this (execute, null)
		{
		}

		public RelayCommand (Action execute, Func<bool> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException (nameof (execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute (object parameter) => _canExecute == null || _canExecute ();

		public void Execute (object parameter) => _execute ();

		public void OnCanExecuteChanged () => CanExecuteChanged?.Invoke (this, EventArgs.Empty);
	}

	public class RelayCommand<T> : ICommand {
		private readonly Func<T, bool> _canExecute;
		private readonly Action<T> _execute;

		public RelayCommand(Action<T> execute) : this (execute, null)
		{
		}

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException (nameof (execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => _canExecute == null || _canExecute ((T) parameter);

		public void Execute(object parameter) => _execute ((T) parameter);

		public void OnCanExecuteChanged () => CanExecuteChanged?.Invoke (this, EventArgs.Empty);
	}
}