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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage {
		MainPage RootPage => Application.Current.MainPage as MainPage;
		List<HomeMenuItem> menuItems;

		public MenuPage ()
		{
			InitializeComponent ();

			menuItems = new List<HomeMenuItem> {
				new HomeMenuItem { Id = MenuItemType.Playground, Title="Playground" },
				new HomeMenuItem { Id = MenuItemType.OpenAndExportFile, Title="Open and Export a PDF" },
				new HomeMenuItem { Id = MenuItemType.PasswordDialog, Title="Password Dialog" },
				new HomeMenuItem { Id = MenuItemType.CreateAnnotation, Title="Create Annotations from Code" },
				new HomeMenuItem { Id = MenuItemType.Print, Title="Print Page" },
				new HomeMenuItem { Id = MenuItemType.Search, Title="Search from Code" },
				new HomeMenuItem { Id = MenuItemType.ToolbarItems, Title="Toolbar Items" },
				new HomeMenuItem { Id = MenuItemType.Events, Title="Event Handling" },
				new HomeMenuItem { Id = MenuItemType.FullTextSearch, Title="Full-Text-Search" },
				new HomeMenuItem { Id = MenuItemType.CustomCss, Title="Custom Css" },
				new HomeMenuItem { Id = MenuItemType.MultiplePdf, Title="Multiple PDFs" },
				new HomeMenuItem { Id = MenuItemType.About, Title="About" }
			};

			ListViewMenu.ItemsSource = menuItems;

			ListViewMenu.SelectedItem = menuItems [0];
			ListViewMenu.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;

				var id = (int) ((HomeMenuItem) e.SelectedItem).Id;
				RootPage.NavigateFromMenu (id);
			};
		}
	}
}