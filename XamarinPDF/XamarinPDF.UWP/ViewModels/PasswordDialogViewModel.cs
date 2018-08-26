//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using PSPDFKit.Document;
using PSPDFKit.UI;
using PSPDFKitFoundation;
using XamarinPDF.Helpers;
using XamarinPDF.UWP.ContentDialogs;
using XamarinPDF.UWP.Helpers;

namespace XamarinPDF.UWP.ViewModels {
	/// <summary>
	/// This example demonstrates how to use a provide a native password input dialog.
	/// </summary>
	public class PasswordDialogViewModel : Observable {
		StorageFile currentPdfFile;
		ICommand openPdfCommand;
		ICommand openPdfApiCommand;

		public ICommand OpenPdfCommand => openPdfCommand ?? (openPdfCommand = new RelayCommand (OpenPdfPicker));
		public ICommand OpenPdfViaApiCommand => openPdfApiCommand ?? (openPdfApiCommand = new RelayCommand (OpenPdfViaApi));

		public PdfView PDFView { get; private set; }

		public void Initialize(PdfView pdfView)
		{
			PDFView = pdfView;
			PDFView.ShowMessage ("Password is 'pspdfkit'.");
		}

		public async void OpenPdfPicker()
		{
			try {
				PDFView.Controller.OnRequestPassword += Controller_OnRequestPassword;

				var file = await StorageFile.GetFileFromApplicationUriAsync (
					new Uri ("ms-appx:///Assets/pdfs/PSPDFKit_password_pspdfkit.pdf"));
				await PDFView.Controller.ShowDocumentAsync (DocumentSource.CreateFromStorageFile (file));
			} catch (Exception e) {
				var messageDialog = new MessageDialog (e.Message);
				await messageDialog.ShowAsync ();
			} finally {
				PDFView.Controller.OnRequestPassword -= Controller_OnRequestPassword;
			}
		}

		async void Controller_OnRequestPassword(Controller sender, PasswordEventArgs args)
		{
			// It is essential the we call `Complete()` on the Deferral at the end.
			var deferral = args.Deferral;

			try {
				var passwordEntryDialog = new PasswordInputDialog ();
				var result = await passwordEntryDialog.ShowAsync ();
				switch (result) {
				case ContentDialogResult.Primary:
					args.Response = new PasswordRequestResponse (true, passwordEntryDialog.Password, true);
					break;
				default:
					args.Response = new PasswordRequestResponse (false, null, false);
					break;
				}
			} finally {
				deferral.Complete ();
			}
		}

		/// <summary>
		/// Open a PDF with a password without any UI interaction to provide the password.
		/// </summary>
		public async void OpenPdfViaApi()
		{
			PDFView.Controller.OnRequestPassword += Controller_OnRequestPassword;

			var file = await StorageFile.GetFileFromApplicationUriAsync (
				new Uri ("ms-appx:///Assets/pdfs/PSPDFKit_password_pspdfkit.pdf"));
			var documentSource = DocumentSource.CreateFromStorageFile (file);

			// Set the password in the document source.
			documentSource.Password = "pspdfkit";

			await PDFView.Controller.ShowDocumentAsync (documentSource);
		}
	}
}