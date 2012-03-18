using System;
using Cirrious.MvvmCross.Dialog.Touch.Dialog.Utilities;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Touch.Interfaces;

namespace TwitterSearch.UI.Touch 
{
	[Register ("AppDelegate")]
	public partial class AppDelegate 
	: MvxApplicationDelegate 
        , IMvxServiceConsumer<IMvxStartNavigation>
	{
		public static readonly NSString NotificationWillChangeStatusBarOrientation = new NSString("UIApplicationWillChangeStatusBarOrientationNotification");
		public static readonly NSString NotificationDidChangeStatusBarOrientation = new NSString("UIApplicationDidChangeStatusBarOrientationNotification");		
		public static readonly NSString NotificationOrientationDidChange = new NSString("UIDeviceOrientationDidChangeNotification");
		public static readonly NSString NotificationFavoriteUpdated = new NSString("NotificationFavoriteUpdated");
		
		// class-level declarations
		UIWindow _window;

		public static bool IsPhone {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
			}
		}

		public static bool IsPad {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
			}
		}

		public static bool HasRetina {
			get {
				if (MonoTouch.UIKit.UIScreen.MainScreen.RespondsToSelector(new Selector("scale")))
					return (MonoTouch.UIKit.UIScreen.MainScreen.Scale == 2.0);
				else
					return false;
			}
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
		
            // initialize app for single screen iPhone display
            var presenter = 
				IsPad 
					? (IMvxTouchViewPresenter)new TwitterTabletSearchPresenter(this, _window) 
					: (IMvxTouchViewPresenter)new TwitterSearchPresenter(this, _window);
			var setup = new Setup(this, presenter);
            setup.Initialize();

            // start the app
            var start = this.GetService<IMvxStartNavigation>();
            start.Start();			
			
            _window.MakeKeyAndVisible();

			return true;
		}
	}
}