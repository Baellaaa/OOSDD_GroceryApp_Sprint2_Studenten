using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class GroceryListItemsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;
        private readonly IProductService _productService;
        public ObservableCollection<GroceryListItem> MyGroceryListItems { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        GroceryList groceryList = new(0, "None", DateOnly.MinValue, "", 0);

        public GroceryListItemsViewModel(IGroceryListItemsService groceryListItemsService, IProductService productService)
        {
            _groceryListItemsService = groceryListItemsService;
            _productService = productService;
            Load(groceryList.Id);
        }

        private void Load(int id)
        {
            MyGroceryListItems.Clear();
            foreach (var item in _groceryListItemsService.GetAllOnGroceryListId(id)) MyGroceryListItems.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts()
        {
            //Clear the current list of products, to prevent conflict
            AvailableProducts.Clear();
            List<Product> producten = _productService.GetAll();
            List<GroceryListItem> boodschappen = _groceryListItemsService.GetAll();
            foreach (var product in producten)
            {
                //checks if the product is already in grocery list or not
                //if stock > 0 -> add to available products 
                if (!MyGroceryListItems.Any(boodschappenProduct => boodschappenProduct.ProductId == product.Id)
                    && product.Stock > 0)
                {
                    AvailableProducts.Add(product);
                }
            }
        }

        partial void OnGroceryListChanged(GroceryList value)
        {
            Load(value.Id);
        }

        [RelayCommand]
        public async Task ChangeColor()
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), GroceryList } };
            await Shell.Current.GoToAsync($"{nameof(ChangeColorView)}?Name={GroceryList.Name}", true, paramater);
        }
        [RelayCommand]
        public void AddProduct(Product product)
        {
            //if stock <= 0 -> don't add to grocerylist
            if (product == null || product.Id <= 0) return;
            List<Product> producten = _productService.GetAll(); //Calling list to prevent conflict with older list


            GroceryListItem newItem = new GroceryListItem(0, groceryList.Id, product.Id, 1);
            _groceryListItemsService.Add(newItem); //Add item to grocery list
            product.Stock--;
            int tmp_index = producten.IndexOf(_productService.Get(product.Id));
            producten.RemoveAt(tmp_index); //Delete old product object 
            producten.Insert(tmp_index, product); //Renew product with updated stock count
            GetAvailableProducts();
            OnGroceryListChanged(GroceryList);
        }
    }
}