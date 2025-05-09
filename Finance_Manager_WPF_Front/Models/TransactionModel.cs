using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance_Manager_WPF_Front.Models;

public class TransactionModel : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private decimal _price;
    private CategoryModel _category = new();
    private CategoryModel? _innerCategory = new();    
    //private string _photo = string.Empty;

    public int Id { get; set; }
    public UserModel User { get; set; } = new();

    public DateTime Date { get; set; } = DateTime.Now;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                _price = value;
                OnPropertyChanged();
            }
        }
    }

    public CategoryModel Category
    {
        get => _category;
        set
        {
            if (_category != value)
            {
                _category = value;
                OnPropertyChanged();
            }
        }
    }

    public CategoryModel? InnerCategory
    {
        get => _innerCategory;
        set
        {
            if (_innerCategory != value)
            {
                _innerCategory = value;
                OnPropertyChanged();
            }
        }
    }

    /*public string Photo
    {
        get => _photo;
        set
        {
            if (_photo != value)
            {
                _photo = value;
                OnPropertyChanged();
            }
        }
    }*/

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}