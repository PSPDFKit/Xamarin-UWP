//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinPDF.DependencyServices;
using XamarinPDF.Helpers;

namespace XamarinPDF.ViewModels {
	public class AboutPageViewModel : Observable {
		string licenseText;

		public string LicenseText {
			get => licenseText;
			set => Set (ref licenseText, value);
		}

		public async Task LoadLicense ()
		{
			var license = await DependencyService.Get<IAboutPage> ().LoadLicense ();
			if (license != null)
				LicenseText = license;
		}
	}
}
