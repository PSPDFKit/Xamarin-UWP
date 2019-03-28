//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using PSPDFKit.UI.ToolbarComponents;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinPDF.DependencyServices;
using XamarinPDF.UWP.DependencyServicesImpl;
using XamarinPDF.UWP.PageRenderers;
using XamarinPDF.Views;

[assembly: Dependency (typeof (ToolbarItemsImplementation))]
namespace XamarinPDF.UWP.DependencyServicesImpl {
	public class ToolbarItemsImplementation : IToolbarItems {

		/// <summary>
		/// Adds a single new toolbar item with an event handler registered to when it's clicked on
		/// </summary>
		public async Task AddToolbarItems (PdfViewer pdfView)
		{
			var view = (pdfView.GetOrCreateRenderer () as PdfViewerRenderer)?.PdfDocView;
			if (view == null)
				return;

            var toolbarItem = new ButtonToolbarItem {
                Attributes =
                {
                    Id = "id",
                    Title = "Button"
                },
                Icon = new Uri ("ms-appx-web:///Assets/ToolbarIcons/status_completed.svg")
			};
			toolbarItem.OnItemPressEvent += ToolbarItem_OnPress;

			var toolbarItems = view.GetToolbarItems();
			toolbarItems.Add (toolbarItem);
			await view.SetToolbarItemsAsync (toolbarItems);
		}

		/// <summary>
		/// Randomly shuffles the order of the items in the toolbar
		/// </summary>
		public async Task ShuffleToolbarItems (PdfViewer pdfView)
		{
			var view = (pdfView.GetOrCreateRenderer () as PdfViewerRenderer)?.PdfDocView;
			if (view == null)
				return;

			var toolbarItems = view.GetToolbarItems ();
			var shuffledList = toolbarItems.OrderBy (a => Guid.NewGuid ()).ToList ();

			await view.SetToolbarItemsAsync (shuffledList);
		}

		/// <summary>
		/// An event handler that will be fired by a click on the custom item added in <see cref="OnAddToolbarItems"/>
		/// </summary>
		/// <param name="toolbarItem">The toolbar item that was clicked</param>
		/// <param name="id">The id of the toolbar item</param>
		async void ToolbarItem_OnPress (IToolbarItem toolbarItem, string id)
		{
			var locationPromptDialog = new ContentDialog {
				Title = "Custom Dialog",
				Content = toolbarItem.Attributes.Title + " was pressed.",
				PrimaryButtonText = "OK"
			};

			await locationPromptDialog.ShowAsync ();
		}
	}
}
