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
	public class CreateAnnotationViewModel : Observable {
		public ICommand CreateInkAnnotationCommand { get; set; }
		public ICommand CreateAndUpdateTextAnnotationCommand { get; set; }

		public CreateAnnotationViewModel ()
		{
			CreateInkAnnotationCommand = new Command<PdfViewer> (CreateInkAnnotation);
			CreateAndUpdateTextAnnotationCommand = new Command<PdfViewer> (CreateAndUpdateTextAnnotation);
		}

		async void CreateInkAnnotation (PdfViewer pdfView) => await DependencyService.Get<ICreateAnnotation> ().CreateInkAnnotation (pdfView);

		async void CreateAndUpdateTextAnnotation (PdfViewer pdfView) => await DependencyService.Get<ICreateAnnotation> ().CreateAndUpdateTextAnnotation (pdfView);
	}
}
