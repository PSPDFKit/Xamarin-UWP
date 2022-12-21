//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPDF.Models;

namespace XamarinPDF.Views {
	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class MainPage : FlyoutPage {
		readonly Dictionary<int, NavigationPage> _menuPages = new Dictionary<int, NavigationPage> ();

		public MainPage()
		{
			InitializeComponent ();

            _menuPages.Add ((int) MenuItemType.Playground, (NavigationPage) Detail);
		}

		public void NavigateFromMenu (int id)
		{
			if (!_menuPages.ContainsKey (id)) {
				if (id == (int) MenuItemType.Playground)
                    _menuPages.Add (id, new NavigationPage (new PlaygroundPage ()));
				else if (id == (int) MenuItemType.OpenAndExportFile)
                    _menuPages.Add (id, new NavigationPage (new OpenAndExportFilePage ()));
				else if (id == (int) MenuItemType.PasswordDialog)
                    _menuPages.Add (id, new NavigationPage (new PasswordDialogPageContainer ()));
				else if (id == (int) MenuItemType.CreateAnnotation)
					_menuPages.Add (id, new NavigationPage (new CreateAnnotationPage ()));
				else if (id == (int) MenuItemType.Print)
                    _menuPages.Add (id, new NavigationPage (new PrintPageContainer ()));
				else if (id == (int) MenuItemType.Search)
                    _menuPages.Add (id, new NavigationPage (new SearchPageContainer ()));
				else if (id == (int) MenuItemType.ToolbarItems)
                    _menuPages.Add (id, new NavigationPage (new ToolbarItemsPage ()));
				else if (id == (int) MenuItemType.Events)
                    _menuPages.Add (id, new NavigationPage (new EventsPageContainer ()));
				else if (id == (int) MenuItemType.FullTextSearch)
                    _menuPages.Add (id, new NavigationPage (new FullTextSearchPageContainer ()));
				else if (id == (int) MenuItemType.CustomCss)
                    _menuPages.Add (id, new NavigationPage (new CustomCssPage ()));
				else if (id == (int) MenuItemType.MultiplePdf)
                    _menuPages.Add (id, new NavigationPage (new MultiplePdfPage ()));
				else if (id == (int) MenuItemType.About)
                    _menuPages.Add (id, new NavigationPage (new AboutPage ()));
			}

			var newPage = _menuPages[id];

			if (newPage != null && Detail != newPage) {
				Detail = newPage;
			}
		}
	}
}
