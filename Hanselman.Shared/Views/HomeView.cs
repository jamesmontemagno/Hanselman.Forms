﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Hanselman.Shared
{
	public class HomeView : MasterDetailPage
	{
		private HomeViewModel ViewModel 
		{
			get { return BindingContext as HomeViewModel; }
		}
		HomeMasterView master;
		private Dictionary<MenuType, NavigationPage> pages;
		public HomeView ()
		{
			pages = new Dictionary<MenuType, NavigationPage> ();
			BindingContext = new HomeViewModel ();

			Master = master = new HomeMasterView (ViewModel);

			var homeNav = new NavigationPage(master.PageSelection) {
				Tint = Helpers.Color.DarkBlue.ToFormsColor()
			};
			Detail = homeNav;

			pages.Add (MenuType.About, homeNav);

			master.PageSelectionChanged = (menuType) => {
			
				NavigationPage newPage;
				if(pages.ContainsKey(menuType)){
					newPage = pages[menuType];
				}
				else{
					newPage = new NavigationPage(master.PageSelection){
						Tint = Helpers.Color.DarkBlue.ToFormsColor()
					};
					pages.Add (menuType, newPage);
				}
				Detail = newPage;
				Detail.Title = master.PageSelection.Title;
				IsPresented = false;
			};

			this.Icon = "slideout.png";
		}
	}


	public class HomeMasterView : BaseView
	{
		public Action<MenuType> PageSelectionChanged;
		private Page pageSelection;
		private MenuType menuType = MenuType.About;
		public Page PageSelection {
				get{ return pageSelection; }
			set { pageSelection = value; 
				if (PageSelectionChanged != null)
					PageSelectionChanged (menuType);
			}
		}
		private AboutView about;
		private BlogView blog;
		private TwitterView twitter;
	    private PodcastView podcast;
		public HomeMasterView(HomeViewModel viewModel)
		{
			this.Icon = "slideout.png";
			BindingContext = viewModel;

      
			var layout = new StackLayout { Spacing = 0 };

			var label = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				BackgroundColor = Color.Transparent,
				Content = new Label {
					Text = "MENU",
					Font = Font.SystemFontOfSize (NamedSize.Medium)
				}
			};

			layout.Children.Add(label);
		
			var listView = new ListView ();

			var cell = new DataTemplate(typeof(ListImageCell));

			cell.SetBinding (TextCell.TextProperty, HomeViewModel.TitlePropertyName);
			cell.SetBinding (ImageCell.ImageSourceProperty, "Icon");

			listView.ItemTemplate = cell;

			listView.ItemsSource = viewModel.MenuItems;
			if (about == null)
				about = new AboutView ();

			PageSelection = about;
			//Change to the correct page
			listView.ItemSelected += (sender, args) =>
			{
				var menuItem = listView.SelectedItem as HomeMenuItem;
				menuType = menuItem.MenuType;
				switch(menuItem.MenuType){
				case MenuType.About:
					if(about == null)
						about = new AboutView();

					PageSelection = about;
					break;
				case MenuType.Blog:
					if(blog == null)
						blog = new BlogView();
						
					PageSelection = blog;
					break;
				case MenuType.Twitter:
					if(twitter == null)
						twitter = new TwitterView();

					PageSelection = twitter;
					break;
                    case MenuType.Podcast:
				        if (podcast == null)
				            podcast = new PodcastView();
                    
                    PageSelection = podcast;
                    break;
				}
			};

			listView.SelectedItem = viewModel.MenuItems[0];
			layout.Children.Add(listView);

			Content = layout;
		}
	}

}

