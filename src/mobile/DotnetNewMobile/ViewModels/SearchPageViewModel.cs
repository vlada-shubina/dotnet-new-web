﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DotnetNewMobile.ViewModels;
using TemplatesShared;
using Xamarin.Forms;

namespace DotnetNewMobile
{
    public class SearchPageViewModel : BaseViewModel
    {
        public SearchPageViewModel(INavigation navigation)
        {
            IsBusy = false;
            SearchCommand = new Command(() => ExecuteSearchCommand());
            FoundItems = new ObservableCollection<SearchTemplateViewModel>();
            Navigation = navigation;
        }

        public INavigation Navigation { get; set; }

        private TemplateSearcher _searcher = new TemplateSearcher();

        private string _searchTerm;
        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }
            set
            {
                if (_searchTerm != value)
                {
                    SetProperty(ref _searchTerm, value, nameof(SearchTerm));
                }
            }
        }

        public ICommand SearchCommand
        {
            get; private set;
        }



        public ObservableCollection<SearchTemplateViewModel> FoundItems { get; set; }
        protected void SetFoundItems(IList<Template> templates){
            if(templates != null){
                FoundItems.Clear();
                foreach(var item in templates){
                    FoundItems.Add(new SearchTemplateViewModel(item, Navigation));
                }
                NumSearchResults = FoundItems.Count;
                NumSearchResultLabelVisible = true;
            }
            else{
                FoundItems = null;
                NumSearchResults = 0;
            }
        }

        private int _numSearchResults;
        public int NumSearchResults{
            get{
                return _numSearchResults;
            }
            set{
                if(_numSearchResults != value){
                    SetProperty(ref _numSearchResults, value, nameof(NumSearchResults));
                }
            }
        }

        private bool _numSearchResultLabelVisible;
        public bool NumSearchResultLabelVisible{
            get{
                return _numSearchResultLabelVisible;
            }
            set{
                if(_numSearchResultLabelVisible != value){
                    SetProperty(ref _numSearchResultLabelVisible, value, nameof(NumSearchResultLabelVisible));
                }
            }
        }

        void ExecuteSearchCommand()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                var helper = new TemplateHelper();
                var allTemplates = helper.GetTemplatePacks();
                var foundTemplates = _searcher.Search(SearchTerm, allTemplates);
                SetFoundItems(foundTemplates);
                
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}