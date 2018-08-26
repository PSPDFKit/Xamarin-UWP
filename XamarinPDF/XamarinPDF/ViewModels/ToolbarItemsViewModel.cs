//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.Windows.Input;
using Xamarin.Forms;
using XamarinPDF.DependencyServices;
using XamarinPDF.Helpers;
using XamarinPDF.Views;

namespace XamarinPDF.ViewModels {
	public class ToolbarItemsViewModel : Observable {
		public ICommand AddToolbarItemsCommand { get; set; }
		public ICommand ShuffleToolbarItemsCommand { get; set; }

		public ToolbarItemsViewModel ()
		{
			AddToolbarItemsCommand = new Command<PdfViewer> (AddToolbarItems);
			ShuffleToolbarItemsCommand = new Command<PdfViewer> (ShuffleToolbarItems);
		}

		async void AddToolbarItems (PdfViewer pdfView) => await DependencyService.Get<IToolbarItems> ().AddToolbarItems (pdfView);

		async void ShuffleToolbarItems (PdfViewer pdfView) => await DependencyService.Get<IToolbarItems> ().ShuffleToolbarItems (pdfView);
	}
}
