//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using PSPDFKit;
using PSPDFKit.UI;
using XamarinPDF.Helpers;

namespace XamarinPDF.UWP.ViewModels {
    public class PrintPageViewModel : Observable {
		PdfView PDFView { get; set; }

		public void Initialize(PdfView pdfView) => PDFView = pdfView;

		internal async void OnPrint (Windows.UI.Xaml.Controls.Page owningPage)
		{
			try {
				// For printing a `Canvas` UIElement is required on the current page in the visual tree
				var printHelper = new PrintHelper (PDFView.Document, owningPage, "PrintCanvas", "My Lovely Document.pdf");

				printHelper.PrintingCompleteHandler += PrintHelper_PrintingCompleteHandler;

				await printHelper.ShowPrintUIAsync ();
			} catch (Exception e) {
				var messageDialog = new MessageDialog (e.ToString ());
				await messageDialog.ShowAsync ();
			}
		}

		static async void PrintHelper_PrintingCompleteHandler (PrintHelper sender, Windows.Graphics.Printing.PrintTaskCompletedEventArgs args)
		{
			await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (CoreDispatcherPriority.Normal, async () => {
				var messageDialog = new MessageDialog ("Print Status: " + args.Completion);
				await messageDialog.ShowAsync ();
			});
		}
    }
}
