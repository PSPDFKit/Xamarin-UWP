//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using PSPDFKit.Document;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinPDF.DependencyServices;
using XamarinPDF.UWP.DependencyServicesImpl;
using XamarinPDF.UWP.PageRenderers;
using XamarinPDF.Views;

[assembly: Dependency (typeof (OpenAndExportFileImplementation))]
namespace XamarinPDF.UWP.DependencyServicesImpl {
	public class OpenAndExportFileImplementation : IOpenAndExportFile {

		public async Task<object> OpenPdfPickerAsync ()
		{
			// Open a Picker so the user can choose a Pdf
			var picker = new FileOpenPicker {
				ViewMode = PickerViewMode.Thumbnail,
				SuggestedStartLocation = PickerLocationId.DocumentsLibrary
			};
			picker.FileTypeFilter.Add (".pdf");

			// Set the `CurrentPdfFile` which will trigger the binding to update the Pdf view.
			var file = await picker.PickSingleFileAsync ();
			return file;
		}

		public async Task ExportPdf (PdfViewer pdfViewer, bool flattenAnnotations = false)
		{
			var renderer = pdfViewer.GetOrCreateRenderer () as PdfViewerRenderer;
			var file = renderer?.PdfDocView?.Controller?.GetPdfDocument ()?.DocumentSource?.GetFile ();
			if (file == null)
				return;

			if (flattenAnnotations)
				await renderer.PdfDocView?.Document.ExportAsync(file, new DocumentExportOptions{Flattened = true});
			else
				await renderer.PdfDocView?.Document.ExportAsync(file);
		}

		public async Task ExportNewPdf (PdfViewer pdfViewer, bool flattenAnnotations = false)
		{
			var savePicker = new FileSavePicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
			savePicker.FileTypeChoices.Add ("Portable Document Format", new List<string> { ".pdf" });
			savePicker.SuggestedFileName = "New Document";

			try {
				var renderer = pdfViewer.GetOrCreateRenderer () as PdfViewerRenderer;
				var file = await savePicker.PickSaveFileAsync ();
				if (file == null || renderer == null)
					return;

				if (flattenAnnotations)
					await renderer.PdfDocView?.Document.ExportAsync(file, new DocumentExportOptions { Flattened = true });
                else
					await renderer.PdfDocView?.Document.ExportAsync(file);
			} catch (Exception e) {
				var messageDialog = new MessageDialog (e.Message);
				await messageDialog.ShowAsync ();
			}
		}
	}
}
