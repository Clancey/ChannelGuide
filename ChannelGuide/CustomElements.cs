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
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

namespace ChannelGuide
{
	public class ChannelElement : Element
	{
		public const string Key = "ChannelElement";
		Channel Channel;
		public ChannelElement(Channel channel) : base ("")
		{
			Channel = channel;
		}
		public override UITableViewCell GetCell (UITableView tv)
		{
			ChannelCell cell = tv.DequeueReusableCell (Key) as ChannelCell;
			if (cell == null) {
				cell = new ChannelCell(Channel);
			}
			else
			{
				cell.UpdateCell(Channel);
			}
			return cell;
		}
		
		public class ChannelCell : UITableViewCell
		{
			// Create the UIViews that we will use here, layout happens in LayoutSubviews
			public Channel Channel;
			ChannelView ChannelView;
			public ChannelCell (Channel channel) : base (UITableViewCellStyle.Default, ChannelElement.Key)
			{

				UpdateCell (channel);
			}
			
			public void UpdateCell (Channel channel)
			{
				if(channel == Channel)
					return;
				Channel = channel;
				if(ChannelView == null)
					ChannelView = new ChannelView();
				ChannelView.Update(Channel);
				this.ContentView.AddSubview(ChannelView);
				SetNeedsDisplay ();
			}
			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				ChannelView.Frame = this.Bounds;
			}
		}
		public class ChannelView : UIView
		{
			List<UIButton> buttons;
			Channel Channel;
			public ChannelView()
			{
				buttons = new List<UIButton>();
			}
			public void Update (Channel channel)
			{
				Channel = channel;
				SetButton();
			}
			private void SetButton()
			{
				//Clear out old buttons
				if(buttons.Count > Channel.Shows.Count)
				{
					var showCount = Channel.Shows.Count;
					var excess = buttons.Count - showCount;
					var oldButtons = buttons.GetRange(showCount - 1,excess);
					foreach(var button in oldButtons)
					{
						button.RemoveFromSuperview();
					}
					
				
					
				}
				// Set buttons
				for(int i = 0; i < Channel.Shows.Count;i++)
				{
					UIButton btn;
					if(buttons.Count >= i +1)
						btn = buttons[i];
					else
					{
						btn = UIButton.FromType(UIButtonType.RoundedRect);
						btn.SetTitleColor(UIColor.Black,UIControlState.Normal);
						buttons.Add(btn);
					}
					this.AddSubview(btn);
				}
				
			}
			
			public override void LayoutSubviews ()
			{
				for(int i = 0; i< Channel.Shows.Count ; i++)
				{
					var show = Channel.Shows[i];
					var btn = buttons[i];
					btn.SetTitle(show.Title,UIControlState.Normal);
					//TODO: change this to be dynamic and figure it out based on times
					var frame = new RectangleF(show.X * this.Bounds.Width,0,show.Width * this.Bounds.Width,this.Bounds.Height);
					btn.Frame = frame;
				}
			}
		}
	}
}

