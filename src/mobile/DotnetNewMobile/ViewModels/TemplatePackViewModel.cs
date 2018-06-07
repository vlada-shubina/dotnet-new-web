﻿using System;
using System.Windows.Input;
using Plugin.Share;
using Plugin.Share.Abstractions;
using TemplatesShared;
using Xamarin.Forms;

namespace DotnetNewMobile.ViewModels
{
    public class TemplatePackViewModel
    {
        public TemplatePackViewModel(TemplatePack pack){
            Pack = pack;
            BrowseToNuGet = new Command(ExecuteBrowseToNuget);
            BrowseProjectSite = new Command(ExecuteBrowseProjectSite);
            BrowseLicense = new Command(ExecuteBrowseToLicense);
            ShareCommand = new Command(ExecuteShare);
        }

        public TemplatePack Pack
        {
            get; private set;
        }

        public ICommand BrowseToNuGet
        {
            get; private set;
        }

        public ICommand BrowseProjectSite{
            get; private set;
        }

        public ICommand BrowseLicense{
            get; private set;
        }

        public ICommand ShareCommand{
            get;private set;
        }

        public string DownloadCount
        {
            get{
                return Pack != null ? Pack.DownloadCount.ToString() : string.Empty;
            }
        }
   
        public string PackageString{
            get{
                return Pack != null? Pack.Package: "";
            }
        }

        public string NumTemplatesString{
            get{
                return Pack != null ? $"{Pack.Templates.Length} templates" : string.Empty;
            }
        }

        public string OwnerString{
            get{
                return Pack != null ? $"By {Pack.Owners}": string.Empty;
            }
        }

        public string IconPngUrl{
            get{
                return Pack != null ? Pack.IconPngUrl : string.Empty;
            }
        }

		public string Description
		{
			get
			{
				return Pack != null ? Pack.Description : string.Empty;
			}
		}
		public bool HasLicense{
			get{
				return !string.IsNullOrWhiteSpace(LicenseUrl);
			}
		}
        public string LicenseUrl
        {
            get
            {
                return Pack != null ? Pack.LicenseUrl : string.Empty;
            }
        }
		public bool HasProjectUrl{
			get{
				return !string.IsNullOrWhiteSpace(ProjectUrl);
			}
		}
        public string ProjectUrl
        {
            get
            {
                return Pack != null ? Pack.ProjectUrl : string.Empty;
            }
        }
        public string NuGetUrl
        {
            get
            {
                return Pack != null ? $"https://www.nuget.org/packages/{Pack.Package}" : string.Empty;
            }
        }

        public void ExecuteBrowseToNuget(object s){
            Device.OpenUri(new System.Uri(NuGetUrl));
        }

        public void ExecuteBrowseProjectSite(){
            Device.OpenUri(new System.Uri(ProjectUrl));
        }

        public void ExecuteBrowseToLicense(object s)
        {
            Device.OpenUri(new System.Uri(LicenseUrl));
        }

        public async void ExecuteShare(){
            ShareMessage msg = new ShareMessage();
            msg.Title = "Share";
            msg.Text = "Check this out";
            msg.Url = "https://google.com";
            await CrossShare.Current.Share(msg);
        }
    }
}

