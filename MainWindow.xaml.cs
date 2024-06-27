using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeManager2WPF
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Recipe currentRecipe;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var ingredientName = IngredientNameTextBox.Text;
            var calories = int.Parse(CaloriesTextBox.Text);
            var foodGroup = (FoodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var ingredient = new Ingredient { Name = ingredientName, Calories = calories, FoodGroup = foodGroup };

            if (currentRecipe == null)
            {
                currentRecipe = new Recipe { Name = RecipeNameTextBox.Text };
                recipes.Add(currentRecipe);
            }

            object value = currentRecipe.Ingredients.Add(ingredient);
            IngredientNameTextBox.Clear();
            CaloriesTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe = new Recipe { Name = RecipeNameTextBox.Text };
            recipes.Add(currentRecipe);
            RecipeNameTextBox.Clear();
            UpdateRecipeList();
        }

        private void UpdateRecipeList()
        {
            RecipesListBox.ItemsSource = recipes.OrderBy(r => r.Name).ToList();
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            string details = $"Recipe: {recipe.Name}\nTotal Calories: {recipe.TotalCalories}\n";
            if (recipe.TotalCalories > 300)
            {
                details += "Warning: This recipe exceeds 300 calories!\n";
            }
            foreach (var ingredient in recipe.Ingredients)
            {
                details += $"- {ingredient.Name}, Calories: {ingredient.Calories}, Food Group: {ingredient.FoodGroup}\n";
            }
            RecipeDetailsTextBlock.Text = details;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var filterText = FilterTextBox.Text;
            var filteredRecipes = recipes
                .Where(r => r.Ingredients.Any(i => i.Name.Contains(filterText)))
                .OrderBy(r => r.Name)
                .ToList();
            RecipesListBox.ItemsSource = filteredRecipes;
        }
    }

    internal class Recipe
    {
        public string Name { get; set; }
        public object Ingredients { get; internal set; }
        public object TotalCalories { get; internal set; }
    }

    internal class Ingredient
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }
}
