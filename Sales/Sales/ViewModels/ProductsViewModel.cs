namespace Sales.ViewModels
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Sales.ViewModels;
    using Sales.Common.Models;
    using Sales.Services;
    using Xamarin.Forms;
    
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class ProductsViewModel : BaseViewModel
    {

        private ApiService apiService;

        private bool isRefreshing;
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.SetValue(ref this.products, value);
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.SetValue(ref this.isRefreshing, value);
            }
        }
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var response = await this.apiService.GetList<Product>(url,"/api","/Products");

            if (!response.IsSuccess)
                {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                this.IsRefreshing = false;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }
}
