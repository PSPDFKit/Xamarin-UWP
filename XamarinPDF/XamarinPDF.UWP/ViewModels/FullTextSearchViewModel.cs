//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Microsoft.Toolkit.Uwp.Helpers;
using PSPDFKit.Document;
using PSPDFKit.Search;
using PSPDFKit.UI;
using PSPDFKitFoundation.Search;
using XamarinPDF.UWP.Helpers;

namespace XamarinPDF.UWP.ViewModels {
	/// <summary>
	/// Represents a message for an event.
	/// The Tooltip, source Document and page index are optional.
	/// If the Document is set then clicking on the event in the event list will cause it to be openend in the PdfView
	/// </summary>
	public class EventEntry {
		public string Message { get; set; }
		public string Tooltip { get; set; }
		public DocumentSource Document { get; set; }
		public int PageIndex { get; set; }

		public EventEntry (string message) => Message = message;

		public EventEntry (string message, string tooltip)
		{
			Message = message;
			Tooltip = tooltip;
		}
	}

	public class FullTextSearchViewModel {
		/// <summary>
		///     A `ObservableCollection` that our list view can bind on.
		/// </summary>

		public ObservableCollection<EventEntry> Events { get; } = new ObservableCollection<EventEntry> ();

		public PdfView PDFView { get; private set; }

		ICommand _ClearEventListCommand;
		public ICommand ClearEventListCommand => _ClearEventListCommand ?? (_ClearEventListCommand = new RelayCommand (ClearEventList));

		ICommand _AddFolderCommand;
		public ICommand AddFolderCommand => _AddFolderCommand ?? (_AddFolderCommand = new RelayCommand (AddFolderToLibrary));

		ICommand _ClearIndexCommand;
		public ICommand ClearIndexCommand => _ClearIndexCommand ?? (_ClearIndexCommand = new RelayCommand (ClearLibraryIndex));

		ICommand _SearchCommand;
		public ICommand SearchCommand => _SearchCommand ?? (_SearchCommand = new RelayCommand<string> (Search));

		Library _Library;

		/// <summary>
		/// If you don't use the 'using' statement to automatically dispose of the Library don't forget to call it when
		/// you are finished with the Library. For example, when navigating away from a page.
		/// </summary>
		public void DisposeOfLibrary () => _Library?.Dispose ();

		internal async void Initialize (PdfView pdfView)
		{
			PDFView = pdfView;

			PDFView.OnDocumentOpened += (sender, document) => {
				var message = "Document Opened From " + (document.DocumentSource.GetFile () != null ? "StorageFile" : "IBuffer");
				Events.Add (new EventEntry (message));
			};

			_Library = await Library.OpenLibraryAsync ("catalog");
			_Library.OnStartIndexingDocument += Library_OnStartIndexingDocument;
			_Library.OnFinishedIndexingDocument += Library_OnFinishedIndexingDocument;

			_Library.OnSearchComplete += Library_OnSearchComplete;
			_Library.OnSearchPreviewComplete += Library_OnResultPreviewGenerationComplete;

			Events.Add (new EventEntry ("Library opened."));
		}

		async void Library_OnStartIndexingDocument (Library sender, string uid)
		{
			var document = await DocumentSource.CreateFromUidAsync (uid);
			// This handler is called on a non-UI thread so we need to dispatch on a
			// UI thread any interaction with the UI
			var message = new EventEntry ($"Started indexing '{document.GetFile ().Name}'", uid);
#pragma warning disable 4014
			DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (message));
#pragma warning restore 4014
		}

		async void Library_OnFinishedIndexingDocument (Library sender, LibraryIndexingSuccess args)
		{
			var document = await DocumentSource.CreateFromUidAsync (args.Uid);
			// This handler is called on a non-UI thread so we need to dispatch on a
			// UI thread any interaction with the UI
#pragma warning disable 4014
			DispatcherHelper.ExecuteOnUIThreadAsync (() =>
#pragma warning restore 4014
			{
				var success = args.Success ? "successfully" : "unsuccessfully";
				Events.Add (new EventEntry ($"Finished indexing '{document.GetFile ().Name}' {success}", args.Uid));
			});
		}

		public async void AddFolderToLibrary ()
		{
			var folderPicker = new FolderPicker {
				SuggestedStartLocation = PickerLocationId.Desktop
			};
			folderPicker.FileTypeFilter.Add ("*");
			var folder = await folderPicker.PickSingleFolderAsync ();
			if (folder == null)
				return;

			await _Library.EnqueueDocumentsInFolderAsync (folder);
			Events.Add (new EventEntry ($"Added '{folder.Name}' to library."));
		}

		public async void ClearLibraryIndex ()
		{
			Events.Add (new EventEntry ("Clearing indexes."));

			// Cancel any currently running tasks
			await _Library.CancelAllTasksAsync ();
			// Clear the indexes
			await _Library.ClearAllIndexesAsync ();

			Events.Add (new EventEntry ("Indexes cleared."));
		}

		public void Search (string term)
		{
			var query = new LibraryQuery (term) {
				GenerateTextPreviews = true,
				PreviewRange = { Length = 30 }
			};

			// We could block and await the results here
			// but in this case we let the handler get the results
			// in Library_OnSearchComplete
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			_Library.SearchAsync (query);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}

		/// <summary>
		/// This handler recieves the results of the search query.
		/// Specifically any documents that contained a match and the page numbers where those matches are located.
		/// </summary>
		/// <param name="library">The libary that was searched.</param>
		/// <param name="args">The results of the search, if any.</param>
		async void Library_OnSearchComplete (Library library, IDictionary<string, LibraryQueryResult> args)
		{
			await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (new EventEntry ("Search complete")));

			var count = 0;
			foreach (var libraryResult in args) {
				// Get the document from its UID
				DocumentSource document;
				try {
					document = await DocumentSource.CreateFromUidAsync (libraryResult.Key);
				} catch (System.IO.FileNotFoundException) {
					// Document must be gone from the file system. Let's remove it from the library.
					await _Library.RemoveDocumentAsync (libraryResult.Key);
					continue;
				};

				// Compiling the results into some text for the event list
				var resultText = $"Search complete in file: '{document.GetFile ().Name}'\n";
				var pageList = libraryResult.Value.PageResults.Aggregate ("", (current, pageIndex) => current + $"{pageIndex} ");
				resultText += $"With results found on pages {pageList}\n";

				var eventEntry = new EventEntry (resultText, resultText) { Document = document };
				await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (eventEntry));
				count++;
			}

			await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (new EventEntry ($"Found {count} results")));
		}

		/// <summary>
		/// This handler recieves the result previews if they were requested in the <see cref="Query"/>
		/// </summary>
		/// <param name="library">The libary that was searched.</param>
		/// <param name="args">The result previews of the search, if any.</param>
		async void Library_OnResultPreviewGenerationComplete (Library library, IList<LibraryPreviewResult> args)
		{
			await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (new EventEntry ("Preview generation complete")));

			var count = 0;
			foreach (var preview in args) {
				// Get the document from its UID
				DocumentSource document;
				try {
					document = await DocumentSource.CreateFromUidAsync (preview.Uid);
				} catch (System.IO.FileNotFoundException) {
					// Document must be gone from the file system. Let's remove it from the library.
					await _Library.RemoveDocumentAsync (preview.Uid);
					continue;
				};

				var eventEntry = new EventEntry ($"Preview Result: '{preview.PreviewText}'", $"In document {preview.Uid}") {
					Document = document,
					PageIndex = preview.PageIndex
				};
				await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (eventEntry));
				count++;
			}

			await DispatcherHelper.ExecuteOnUIThreadAsync (() => Events.Add (new EventEntry ($"Found {count} preview results")));
		}

		internal async void ShowDocument (DocumentSource document, int pageIndex)
		{
			await PDFView.OpenStorageFileAsync (document.GetFile ());
			await PDFView.Controller.SetCurrentPageIndexAsync (pageIndex);
		}

		public void ClearEventList () => Events.Clear ();
	}
}