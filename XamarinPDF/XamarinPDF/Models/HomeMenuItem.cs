//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

namespace XamarinPDF.Models {
	public enum MenuItemType {
		Playground,
		OpenAndExportFile,
		PasswordDialog,
		CreateAnnotation,
		Print,
		Search,
		ToolbarItems,
		Events,
		FullTextSearch,
		CustomCss,
		MultiplePdf,
		About,
	}

	public class HomeMenuItem {
		public MenuItemType Id { get; set; }
		public string Title { get; set; }
	}
}