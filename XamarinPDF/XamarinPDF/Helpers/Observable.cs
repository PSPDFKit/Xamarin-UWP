//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinPDF.Helpers {
	public class Observable : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Set<T> (ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals (storage, value))
				return;

			storage = value;
			OnPropertyChanged (propertyName);
		}

		protected void OnPropertyChanged (string propertyName) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
	}
}
