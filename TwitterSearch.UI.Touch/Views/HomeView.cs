using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using TwitterSearch.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;
using Cirrious.MvvmCross.Views;

namespace TwitterSearch.UI.Touch.Views
{
	public partial class HomeView : MvxBindingTouchViewController<HomeViewModel>
	{
		public HomeView (MvxShowViewModelRequest request) : base (request, "HomeView", null)
		{
			Title = "Twitter Search Home";
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.AddBindings(new Dictionary<object, string>()
			{
				{ Go, "{'TouchDown':{'Path':'SearchCommand'}}"}, 
				{ Random, "{'TouchDown':{'Path':'PickRandomCommand'}}"}, 
				{ Edit, "{'Text':{'Path':'SearchText'}}"}, 
			});
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

