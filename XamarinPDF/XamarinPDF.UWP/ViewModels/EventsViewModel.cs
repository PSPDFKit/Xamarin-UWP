//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PSPDFKit.Pdf;
using PSPDFKit.Pdf.Annotation;
using PSPDFKit.UI;
using XamarinPDF.Helpers;

namespace XamarinPDF.UWP.ViewModels {
	public class EventsViewModel : Observable {
		/// <summary>
		///     A `ObservableCollection` that our list view can bind on.
		/// </summary>
		public ObservableCollection<string> Events { get; } = new ObservableCollection<string> ();

		public PdfView PDFView { get; private set; }

		internal void Initialize (PdfView pdfView)
		{
			PDFView = pdfView;

			// Register event handlers
			PDFView.InitializationCompletedHandler += PDFView_InitializationCompletedHandler;

			PDFView.OnDocumentOpened += (sender, document) => {
				Events.Add ("Document Opened From " + (document.DocumentSource.GetFile () != null ? "StorageFile" : "IBuffer"));

				document.AnnotationsCreated += (view, annotations) => { AnnotationEvent ("created", annotations); };
				document.AnnotationsDeleted += (view, annotations) => { AnnotationEvent ("deleted", annotations); };
				document.AnnotationsUpdated += (view, annotations) => { AnnotationEvent ("updated", annotations); };
			};
		}

		void AnnotationEvent (string eventSource, ICollection<IAnnotation> annotations)
		{
			var eventMessage = $"{annotations.Count} annotations were {eventSource}";
			eventMessage = annotations.Aggregate (eventMessage, (current, annotation) =>
				 current + $"\r\n{annotation.AnnotationType.ToString ()} on page {annotation.PageIndex}");
			Events.Add (eventMessage);
		}

		async void PDFView_InitializationCompletedHandler (PdfView pdfView, Document document)
		{
			pdfView.Controller.OnPrint += (sender, args) => { Events.Add ("Print Button Clicked"); };
			pdfView.Controller.OnCurrentPageChanged += (sender, pageIndex) => {
				Events.Add ($"Current Page Changed to {pageIndex}");
			};

			pdfView.Controller.OnAnnotationSelectionChanged += (sender, annotation) => {
				if (annotation == null)
					Events.Add ("No annotation selected");
				else
					Events.Add ($"Selected {annotation.AnnotationType.ToString ()} annotation with {annotation.Id}");
			};

			var pageCount = await document.GetTotalPageCountAsync ();
			Events.Add ($"PdfView Initialization Completed: Opened document with {pageCount} pages");
		}
	}
}