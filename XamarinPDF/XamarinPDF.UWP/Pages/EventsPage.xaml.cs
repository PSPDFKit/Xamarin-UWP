//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Windows.UI.Xaml.Controls;
using XamarinPDF.UWP.ViewModels;

namespace XamarinPDF.UWP.Pages {
	public sealed partial class EventsPage : Page {
		public EventsPage ()
		{
			InitializeComponent ();
			ViewModel.Initialize (PDFView);
		}

		public EventsViewModel ViewModel { get; } = new EventsViewModel ();
	}
}
