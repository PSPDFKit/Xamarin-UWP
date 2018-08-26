//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.Threading.Tasks;
using XamarinPDF.Views;

namespace XamarinPDF.DependencyServices {
	public interface ICreateAnnotation {
		Task CreateInkAnnotation (PdfViewer pdfView);
		Task CreateAndUpdateTextAnnotation (PdfViewer pdfView);
	}
}