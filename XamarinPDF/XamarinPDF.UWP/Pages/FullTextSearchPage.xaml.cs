//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XamarinPDF.UWP.ViewModels;

namespace XamarinPDF.UWP.Pages {
	public sealed partial class FullTextSearchPage : Page {
		public FullTextSearchPage()
		{
			InitializeComponent ();
			ViewModel.Initialize (PDFView);

			EventList.SelectionChanged += EventList_SelectionChanged;
		}

		private void EventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItems = ((ListBox) sender).SelectedItems;
			if (selectedItems.Count <= 0)
				return;

			var item = (EventEntry) selectedItems [0];
			if (item?.Document != null)
				ViewModel.ShowDocument (item.Document, item.PageIndex);
		}

		public FullTextSearchViewModel ViewModel { get; } = new FullTextSearchViewModel ();

		protected override void OnNavigatedFrom (NavigationEventArgs e) => ViewModel.DisposeOfLibrary ();
	}
}
