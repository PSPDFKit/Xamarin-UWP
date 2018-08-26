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
	public partial class MainPage : MasterDetailPage {
		readonly Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage> ();

		public MainPage()
		{
			InitializeComponent ();

			MasterBehavior = MasterBehavior.Split;
			MenuPages.Add ((int) MenuItemType.Playground, (NavigationPage) Detail);
		}

		public void NavigateFromMenu (int id)
		{
			if (!MenuPages.ContainsKey (id)) {
				if (id == (int) MenuItemType.Playground)
					MenuPages.Add (id, new NavigationPage (new PlaygroundPage ()));
				else if (id == (int) MenuItemType.OpenAndExportFile)
					MenuPages.Add (id, new NavigationPage (new OpenAndExportFilePage ()));
				else if (id == (int) MenuItemType.PasswordDialog)
					MenuPages.Add (id, new NavigationPage (new PasswordDialogPageContainer ()));
				else if (id == (int) MenuItemType.CreateAnnotation)
					MenuPages.Add (id, new NavigationPage (new CreateAnnotationPage ()));
				else if (id == (int) MenuItemType.Print)
					MenuPages.Add (id, new NavigationPage (new PrintPageContainer ()));
				else if (id == (int) MenuItemType.Search)
					MenuPages.Add (id, new NavigationPage (new SearchPageContainer ()));
				else if (id == (int) MenuItemType.ToolbarItems)
					MenuPages.Add (id, new NavigationPage (new ToolbarItemsPage ()));
				else if (id == (int) MenuItemType.Events)
					MenuPages.Add (id, new NavigationPage (new EventsPageContainer ()));
				else if (id == (int) MenuItemType.FullTextSearch)
					MenuPages.Add (id, new NavigationPage (new FullTextSearchPageContainer ()));
				else if (id == (int) MenuItemType.CustomCss)
					MenuPages.Add (id, new NavigationPage (new CustomCssPage ()));
				else if (id == (int) MenuItemType.MultiplePdf)
					MenuPages.Add (id, new NavigationPage (new MultiplePdfPage ()));
				else if (id == (int) MenuItemType.About)
					MenuPages.Add (id, new NavigationPage (new AboutPage ()));
			}

			var newPage = MenuPages [id];

			if (newPage != null && Detail != newPage) {
				Detail = newPage;
			}
		}
	}
}
