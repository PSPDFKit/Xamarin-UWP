//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinPDF.UWP.PageRenderers;
using XamarinPDF.UWP.Pages;
using XamarinPDF.Views;

[assembly: ExportRenderer (typeof (SearchPageContainer), typeof (SearchPageRenderer))]
namespace XamarinPDF.UWP.PageRenderers {
	public class SearchPageRenderer : PageRenderer {
		SearchPage page;
		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			if (page == null)
				page = new SearchPage ();

			SetNativeControl (page);
		}
	}
}
