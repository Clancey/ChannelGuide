// 
//  Copyright 2012  Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace ChannelGuide
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		DialogViewController dvc;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			dvc = new DialogViewController(UITableViewStyle.Plain,CreateRoot());
			window.RootViewController = dvc;
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
		
		private RootElement CreateRoot ()
		{
			List<Channel> Channels = new List<Channel> (){
				new Channel{
					Name = "MYST",
					Shows = new List<Show>{
						new Show{Title = "Law & Order",X= 0,Width = .25f},
						new Show{Title = "Haven",X= .25f,Width = .25f},
						new Show{Title = "Desperate Escape",X= .5f,Width = .5f},
					}
				},
				new Channel{
					Name = "MTV2CAN",
					Shows = new List<Show>{
						new Show{Title = "Instant Star",X= 0,Width = .125f},
						new Show{Title = "Instant Star",X= .125f,Width = .125f},
						new Show{Title = "Whistler",X= .25f,Width = .25f},
						new Show{Title = "Peak Season",X= .5f,Width = .125f},
						new Show{Title = "Ridiculousn...",X= .625f,Width = .125f},
						new Show{Title = "Awkward.",X= .75f,Width = .125f},
						new Show{Title = "The Hard Times of RJ Berger",X= .875f,Width = .125f},
					}
				}
			};
			
			var section = new Section ();
			foreach (var channel in Channels) {
				section.Add (new ChannelElement (channel));
			}
			RootElement root = new RootElement ("Guide"){
				section
			};
			return root;
		}
	}
}

