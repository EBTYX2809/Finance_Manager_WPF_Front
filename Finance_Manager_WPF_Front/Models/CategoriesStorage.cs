namespace Finance_Manager_WPF_Front.Models;

public static class CategoriesStorage
{
    public static List<CategoryModel> AllCategories { get; set; } = new();

    public static CategoryModel? GetCategoryById(int id)
    {        
        return AllCategories.FirstOrDefault(c => c.Id == id);
    }
}
