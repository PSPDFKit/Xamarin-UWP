//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Xamarin.Forms;

namespace XamarinPDF.Views {
	public class PdfViewer : View {
		// License
		public static readonly BindableProperty LicenseProperty = BindableProperty.Create (
			propertyName: "License",
			returnType: typeof (string),
			declaringType: typeof (PdfViewer),
			defaultValue: null);

		public string License {
			get => (string) GetValue (LicenseProperty);
			set => SetValue (LicenseProperty, value);
		}

		// PdfUriSource
		public static readonly BindableProperty PdfUriSourceProperty = BindableProperty.Create (
			propertyName: "PdfUriSource",
			returnType: typeof (string),
			declaringType: typeof (PdfViewer),
			defaultValue: null);

		public string PdfUriSource {
			get => (string) GetValue (PdfUriSourceProperty);
			set => SetValue (PdfUriSourceProperty, value);
		}

		// Css
		public static readonly BindableProperty CssProperty = BindableProperty.Create (
			propertyName: "Css",
			returnType: typeof (string),
			declaringType: typeof (PdfViewer),
			defaultValue: null);

		public string Css {
			get => (string) GetValue (CssProperty);
			set => SetValue (CssProperty, value);
		}

		// PdfFileSource
		public static readonly BindableProperty PdfFileSourceProperty = BindableProperty.Create (
			propertyName: "PdfFileSource",
			returnType: typeof (object),
			declaringType: typeof (PdfViewer),
			defaultValue: null);

		public object PdfFileSource {
			get => GetValue (PdfFileSourceProperty);
			set => SetValue (PdfFileSourceProperty, value);
		}

		// ShowMessage
		public static readonly BindableProperty ShowMessageProperty = BindableProperty.Create (
			propertyName: "ShowMessage",
			returnType: typeof (string),
			declaringType: typeof (PdfViewer),
			defaultValue: null);

		public string ShowMessage {
			get => (string) GetValue (ShowMessageProperty);
			set => SetValue (ShowMessageProperty, value);
		}
	}
}