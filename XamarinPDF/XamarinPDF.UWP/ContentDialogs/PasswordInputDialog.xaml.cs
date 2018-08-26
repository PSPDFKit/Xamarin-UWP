//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamarinPDF.UWP.ContentDialogs {
	public sealed partial class PasswordInputDialog : ContentDialog {
		public string Password;

		public PasswordInputDialog () => InitializeComponent ();

		private void PasswordBox_OnPasswordChanged (object sender, RoutedEventArgs e) => Password = PasswordBox.Password;
	}
}
