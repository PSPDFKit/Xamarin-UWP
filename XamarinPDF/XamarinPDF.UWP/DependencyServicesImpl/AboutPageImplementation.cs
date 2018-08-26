//
//  Copyright © 2018 PSPDFKit GmbH. All rights reserved.
//
//  The PSPDFKit Sample applications are licensed with a modified BSD license.
//  Please see License for details. This notice may not be removed from this file.
//

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Xamarin.Forms;
using XamarinPDF.DependencyServices;
using XamarinPDF.UWP.DependencyServicesImpl;

[assembly: Dependency (typeof (AboutPageImplementation))]
namespace XamarinPDF.UWP.DependencyServicesImpl {
	public class AboutPageImplementation : IAboutPage {
		public async Task<string> LoadLicense ()
		{
			var appx = Package.Current.InstalledLocation;
			var licenseFile = await appx.GetFileAsync ("License.txt");
			return await FileIO.ReadTextAsync (licenseFile);
		}
	}
}
