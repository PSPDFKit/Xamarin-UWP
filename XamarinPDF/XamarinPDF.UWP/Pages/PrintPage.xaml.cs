//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PSPDFKit;
using PSPDFKit.Document;
using PSPDFKit.Pdf;
using PSPDFKit.UI;
using XamarinPDF.UWP.ViewModels;

namespace XamarinPDF.UWP.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrintPage : Page
    {
		private PrintPageViewModel ViewModel { get; } = new PrintPageViewModel ();

		public PrintPage()
		{
			InitializeComponent ();
			ViewModel.Initialize (PDFView);

			PDFView.InitializationCompletedHandler += PDFView_InitializationCompletedHandler;
		}

		// Once the Document is initialized we tell it we want to know when the user clicks the print button
		private void PDFView_InitializationCompletedHandler (PdfView sender, Document document) => sender.Controller.OnPrint += API_OnPrint;

		private void API_OnPrint (Controller sender, object args) => ViewModel.OnPrint (this);

		private void PrintButton_Click (object sender, RoutedEventArgs e) => ViewModel.OnPrint (this);

		private async void PrintWithoutPdfViewButton_Click(object sender, RoutedEventArgs e)
		{
			var file = await StorageFile.GetFileFromApplicationUriAsync (new Uri ("ms-appx:///Assets/pdfs/PSPDFKit.pdf"));
			var documentSource = DocumentSource.CreateFromStorageFile (file);

			var printHelper = await PrintHelper.CreatePrintHelperFromSourceAsync (documentSource, this, "PrintCanvas", "PrintWithoutUI");
			await printHelper.ShowPrintUIAsync ();
		}
	}
}
