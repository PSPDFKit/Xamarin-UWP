//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Windows.UI.Xaml.Controls;
using XamarinPDF.UWP.ViewModels;

namespace XamarinPDF.UWP.Pages {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SearchPage : Page {
		public SearchPageViewModel ViewModel { get; } = new SearchPageViewModel ();

		public SearchPage ()
		{
			InitializeComponent ();
			ViewModel.Initialize (PDFView);
		}
	}
}
