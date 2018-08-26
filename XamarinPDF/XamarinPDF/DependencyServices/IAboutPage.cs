//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.Threading.Tasks;

namespace XamarinPDF.DependencyServices {
	public interface IAboutPage {
		Task<string> LoadLicense ();
	}
}