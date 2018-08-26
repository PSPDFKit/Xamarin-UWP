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
	public class OpenAndExportFileViewModel : Observable {
		object currentPdfFile;

		public ICommand OpenPdfCommand { get; set; }
		public ICommand SavePdfCommand { get; set; }
		public ICommand ExportNewPdfCommand { get; set; }
		public bool FlattenAnnotations { get; set; }

		public object CurrentPdfFile {
			get => currentPdfFile;
			set => Set (ref currentPdfFile, value);
		}

		public OpenAndExportFileViewModel ()
		{
			OpenPdfCommand = new Command (OpenPdfPicker);
			SavePdfCommand = new Command<PdfViewer> (SavePdf);
			ExportNewPdfCommand = new Command<PdfViewer> (SaveNewPdf);
		}

		async void OpenPdfPicker ()
		{
			var file = await DependencyService.Get<IOpenAndExportFile> ().OpenPdfPickerAsync ();
			if (file != null)
				CurrentPdfFile = file;
		}

		/// <summary>
		/// Save to the same file that was opened.
		/// If annotations are flattened they will be flattened in the open document too.
		/// </summary>
		async void SavePdf (PdfViewer pdfView) => await DependencyService.Get<IOpenAndExportFile> ().ExportPdf (pdfView, FlattenAnnotations);

		/// <summary>
		/// Export to a new file picked by the user.
		/// If annotations are flattened they will be not be flattened in the currently open document.
		/// </summary>
		async void SaveNewPdf (PdfViewer pdfView) => await DependencyService.Get<IOpenAndExportFile> ().ExportNewPdf (pdfView, FlattenAnnotations);
	}
}
