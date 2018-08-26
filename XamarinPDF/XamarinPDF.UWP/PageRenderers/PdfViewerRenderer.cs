//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using PSPDFKit.UI;
using System;
using System.ComponentModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.UWP;
using XamarinPDF.UWP.PageRenderers;
using XamarinPDF.Views;

[assembly: ExportRenderer (typeof (PdfViewer), typeof (PdfViewerRenderer))]
namespace XamarinPDF.UWP.PageRenderers {
	public class PdfViewerRenderer : ViewRenderer<PdfViewer, PdfView> {
		public PdfView PdfDocView { get; private set; }
		protected override void OnElementChanged (ElementChangedEventArgs<PdfViewer> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			PdfDocView = new PdfView {
				// See if we sent license from X.F side else look in main App.xaml
				License = e.NewElement.License ?? Application.Current.Resources.MergedDictionaries.FirstOrDefault (r => r.ContainsKey ("PSPDFKitLicense"))? ["PSPDFKitLicense"]?.ToString (),
				PdfUriSource = e.NewElement.PdfUriSource is null ? null : new Uri (e.NewElement.PdfUriSource),
				Css = e.NewElement.Css is null ? null : new Uri (e.NewElement.Css),
				PdfFileSource = e.NewElement.PdfFileSource as StorageFile,
			};

			if (e.NewElement.ShowMessage != null)
				PdfDocView.ShowMessage (e.NewElement.ShowMessage);

			SetNativeControl (PdfDocView);
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var v = (PdfViewer) sender;
			if (e.PropertyName == "PdfFileSource" && PdfDocView != null)
				PdfDocView.PdfFileSource = v?.PdfFileSource as StorageFile;
		}
	}
}