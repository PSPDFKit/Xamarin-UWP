Xamarin.Forms UWP Sample
========================

Xamarin.iOS Bindings for PSPDFKit for iOS: [PSPDFKit/Xamarin-iOS](https://github.com/PSPDFKit/Xamarin-iOS)

Xamarin.Android Bindings for PSPDFKit for Android: [PSPDFKit/Xamarin-Android](https://github.com/PSPDFKit/Xamarin-Android)

#### PSPDFKit

The [PSPDFKit SDK](https://pspdfkit.com/) is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

[PSPDFKit Instant](https://pspdfkit.com/instant) adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

## Running Xamarin.Forms UWP Sample

### Requirements

In order to build this project you need:

* Windows 10
* Visual Studio 2017
* PSPDFKit for Windows.vsix Visual Studio Extension ([get your trial here](https://pspdfkit.com/try/))
* [Visual Studio Tools for Xamarin](https://visualstudio.microsoft.com/xamarin/)
* Getting familiar with [Xamarin.Forms UWP project setup](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/windows/installation/).

### Running the sample project

Open `XamarinPDF.sln` using Visual Studio 2017 and add your license key as a String value in both `App.xaml` files listed below:

* [XamarinPDF/XamarinPDF/App.xaml](XamarinPDF/XamarinPDF/App.xaml)
* [XamarinPDF/XamarinPDF.UWP/App.xaml](XamarinPDF/XamarinPDF.UWP/App.xaml)

Replace `LICENSE_KEY_GOES_HERE` with your provided license from the PSPDFKit customer portal or trial key.

```xaml
<ResourceDictionary>
    <x:String x:Key="PSPDFKitLicense"> LICENSE_KEY_GOES_HERE </x:String>
</ResourceDictionary>
```

Once you set the license key in both files you will be able to build and run the sample project.

**Note**:
> You do not need `PSPDFKitLicense` to be present in both `App.xaml` in your actual application, in this provided project we are just demonstrating that the license initialization can be done either from Xamarin.Forms or the actual UWP project; you can choose just one of the `App.xaml` files to host the `PSPDFKitLicense `.


## Using PSPDFKit for Windows in your Xamarin.Forms UWP project

First you need to follow the [PSPDFKit for Windows integration guide](https://pspdfkit.com/guides/windows/current/getting-started/integrating-pspdfkit/) then you can create a Xamarin.Forms control that inherits from [Xamarin.Forms.View](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.view?view=xamarin-forms) which can be used to host the actual [PdfView](https://pspdfkit.com/api/windows/PSPDFKit/PSPDFKit.UI.PdfView.html) using a Xamarin.Forms custom renderer technique. We encourage you to read Xamarin.Forms [Implementing a View](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view) documentation, we use this pathern in the example project.

### Xamarin.Forms Custom Control

* [PdfViewer.cs](XamarinPDF/XamarinPDF/Views/PdfViewer.cs): This is the Xamarin.Forms custom control that is used to represent a [PdfView](https://pspdfkit.com/api/windows/PSPDFKit/PSPDFKit.UI.PdfView.html) in the Xamarin.Forms context. In this particular example it mirrors properties that are bindable from a XAML context.
* [PdfViewerRenderer.cs](XamarinPDF/XamarinPDF.UWP/PageRenderers/PdfViewerRenderer.cs): This is the class that will actually contain the [PdfView](https://pspdfkit.com/api/windows/PSPDFKit/PSPDFKit.UI.PdfView.html) object initialization and implementation, `PdfViewerRenderer.cs` is hosted in the UWP project context, so it has access to all UWP platform features. [Creating the Custom Renderer on UWP](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view#creating-the-custom-renderer-on-uwp) document goes into greater detail on this subject.

Now that you have both pieces in place, you can use [PdfViewer.cs](XamarinPDF/XamarinPDF/Views/PdfViewer.cs) from the Xamarin.Forms XAML context like this:

```xaml
<ContentPage.Content>
	<PdfViewer
		License="{StaticResource PSPDFKitLicense}"
		PdfUriSource="ms-appx:///Assets/pdfs/default.pdf"/>
</ContentPage.Content>
```

**Note:**
> [PdfViewer.cs](XamarinPDF/XamarinPDF/Views/PdfViewer.cs) and [PdfViewerRenderer.cs](XamarinPDF/XamarinPDF.UWP/PageRenderers/PdfViewerRenderer.cs) are just provided as a way to achieve rendering a PDF within a Xamarin.Forms UWP application, you can definitely achieve the same result by creating a UWP page to host the [PdfView](https://pspdfkit.com/api/windows/PSPDFKit/PSPDFKit.UI.PdfView.html) and your own UWP logic and just use a `Xamarin.Forms.ContentPage` as a container of said UWP page. This is also demonstrated in this sample on [PrintPageContainer.cs](XamarinPDF/XamarinPDF/Views/PrintPageContainer.cs), [PrintPageRenderer.cs](XamarinPDF/XamarinPDF.UWP/PageRenderers/PrintPageRenderer.cs), [PrintPage.xaml.cs](XamarinPDF/XamarinPDF.UWP/Pages/PrintPage.xaml.cs) and [PrintPage.xaml](XamarinPDF/XamarinPDF.UWP/Pages/PrintPage.xaml).