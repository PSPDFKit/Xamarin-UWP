//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using PSPDFKit.Search;
using PSPDFKit.UI;
using PSPDFKitFoundation.Search;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using XamarinPDF.Helpers;
using XamarinPDF.UWP.Helpers;

namespace XamarinPDF.UWP.ViewModels {
	/// <summary>
	/// This example shows how to trigger a search from code and display it in a listbox.
	/// While this isn't a very useful Controller pattern as the PdfView already offers a better inline search,
	/// it still shows of some of the concepts of how to be able to search programmatically.
	/// </summary>
	public class SearchPageViewModel : Observable {
		ICommand _DoSearchCommand;

		/// <summary>
		///     A `ObservableCollection` that our list view can bind on.
		/// </summary>
		public ObservableCollection<SearchResultFormatter> SearchResults { get; } =
			new ObservableCollection<SearchResultFormatter> ();

		/// <summary>
		///     Asks the user for a search term and executes the search.
		/// </summary>
		public ICommand DoSearchCommand => _DoSearchCommand ?? (_DoSearchCommand = new RelayCommand (DoSearch));

		/// <summary>
		///     The Pdf view we get in `Initialize`.
		/// </summary>
		PdfView PdfView { get; set; }

		TextSearcher TextSearcher { get; set; }

		/// <summary>
		///     Initializes the view model with the Pdf view. This is necessary as we have to trigger the search
		///     from the view.
		/// </summary>
		/// <param name="pdfView">The Pdf view of the page</param>
		public void Initialize (PdfView pdfView)
		{
			PdfView = pdfView;
			TextSearcher = new TextSearcher ();

			PdfView.InitializationCompletedHandler += delegate {
				// We register the search result handler as soon as the Pdf view is initialized.
				TextSearcher.SearchResultHandler += APIOnSearchResultHandler;
			};
		}

		async void APIOnSearchResultHandler (TextSearcher sender, PageResults pageResults)
		{
			// Dispatch on the Controller thread, otherwise updating the `SearchResults` list will throw an exception.
			await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (CoreDispatcherPriority.Normal, () => {
				foreach (var result in pageResults.Results)
					SearchResults.Add (new SearchResultFormatter (result));
			});
		}

		async void DoSearch ()
		{
			// Create a TextBox for the user to enter a search term
			var inputBox = new TextBox ();

			// Create a ContentDialog asking the user for a search term.
			var searchQueryDialog = new ContentDialog {
				Title = "Search Document",
				Content = inputBox,
				CloseButtonText = "Cancel",
				PrimaryButtonText = "Search"
			};

			// If the user didn't press `Search`, we return and don't do anything.
			if (await searchQueryDialog.ShowAsync () != ContentDialogResult.Primary)
				return;

			// Clear the search results and execute the search.
			SearchResults.Clear ();
			TextSearcher.SearchDocument (PdfView.Document, new Query (inputBox.Text));
		}

		/// <summary>
		///     The SearchResultFormatter formats the search result to make it look nicer when displaying in the Controller.
		/// </summary>
		public class SearchResultFormatter {
			public SearchResultFormatter (Result result)
			{
				Result = result;

				// We split up the preview text into before the search query, the search query and after the search query.
				var previewText = result.PreviewText;
				var range = result.RangeInPreviewText;
				BeforeQuery = previewText.Substring (0, range.Position);
				SearchQuery = previewText.Substring (range.Position, range.Length);
				AfterQuery = previewText.Substring (range.Position + range.Length);
			}

			/// <summary>
			///     The search result.
			/// </summary>
			public Result Result { get; }

			/// <summary>
			///     The part of the text before the query.
			/// </summary>
			public string BeforeQuery { get; }

			/// <summary>
			///     The search query in the preview text.
			/// </summary>
			public string SearchQuery { get; }

			/// <summary>
			///     The text after the search query, if any.
			/// </summary>
			public string AfterQuery { get; }
		}
	}
}
