namespace Finance_Manager_WPF_Front.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsIncome { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string ColorForBackground { get; set; } = string.Empty;
    public List<CategoryModel>? InnerCategories { get; set; } 
    public CategoryModel? ParentCategory { get; set; }
}