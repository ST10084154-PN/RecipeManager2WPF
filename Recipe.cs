namespace RecipeManager2WPF
{
    internal class Recipe
    {
        public string Name { get; set; }
        public object TotalCalories { get; internal set; }
        public object Ingredients { get; internal set; }
    }
}