//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using PSPDFKit.Geometry;
using PSPDFKit.Pdf.Annotation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinPDF.DependencyServices;
using XamarinPDF.UWP.DependencyServicesImpl;
using XamarinPDF.UWP.PageRenderers;
using XamarinPDF.Views;

[assembly: Dependency (typeof (CreateAnnotationImplementation))]
namespace XamarinPDF.UWP.DependencyServicesImpl {
	public class CreateAnnotationImplementation : ICreateAnnotation {

		public async Task CreateInkAnnotation (PdfViewer pdfView)
		{
			var view = (pdfView.GetOrCreateRenderer () as PdfViewerRenderer)?.PdfDocView;
			if (view == null)
				return;

			// First, we define the bounding box of the annotation on the page.
			var boundingBox = new Rect (100, 100, 50, 75);

			// Now we specify which lines we want to draw. 
			var lines = new List<IList<DrawingPoint>> {
				new List<DrawingPoint> {
					new DrawingPoint(100, 150),
					new DrawingPoint(125, 175),
					new DrawingPoint(150, 100)
				}
			};

			// Create the annotation template
			var annotation = new Ink {
				BoundingBox = boundingBox,
				Lines = lines,
				LineWidth = 2,
				StrokeColor = Colors.Red,
				PageIndex = 0
			};

			// Add the annotation to the document in the view.
			await view.Document.CreateAnnotationAsync (annotation);
		}

		public async Task CreateAndUpdateTextAnnotation (PdfViewer pdfView)
		{
			var view = (pdfView.GetOrCreateRenderer () as PdfViewerRenderer)?.PdfDocView;
			if (view == null)
				return;

			// Create the annotation template.
			var annotation = new Text {
				BoundingBox = new Rect (100, 100, 200, 30),
				FontColor = Colors.Blue,
				Contents = "Text annotation",
				FontSize = 12
			};

			// Create the annotation in the document. Note: you will receive an updated annotation back
			// that has more properties set.
			var createdAnnotation = await view.Document.CreateAnnotationAsync (annotation) as Text;

			// Change the text of it on the result.
			createdAnnotation.Contents = "Changed Text Annotation";

			// Actually update the annotation.
			await view.Document.UpdateAnnotationAsync (createdAnnotation);
		}
	}
}
