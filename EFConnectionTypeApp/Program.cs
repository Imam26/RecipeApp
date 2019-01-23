using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConnectionTypeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RecipeContext db = new RecipeContext())
            {
                while (true)
                {
                    Console.WriteLine("1 - Создание нового рецепта");
                    Console.WriteLine("2 - Редактирование нового рецепта");
                    Console.WriteLine("3 - Список рецептов");

                    int key;

                    if (int.TryParse(Console.ReadLine(), out key))
                    {
                        switch (key)
                        {
                            case 1:
                                CreatNewRecipe(db);
                                break;
                            case 2: break;
                            case 3: break;
                            default:break;
                        }
                    }

                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void CreatNewRecipe(RecipeContext db)
        {
            Recipe recipe = new Recipe();
            Ingredient ingredient = new Ingredient();
            while (true)
            {
                Console.WriteLine("1 - Добавить название рецепта");
                Console.WriteLine("2 - Добавить ингредиент");
                Console.WriteLine("3 - Сохранить");

                int key;

                if (int.TryParse(Console.ReadLine(), out key))
                {
                    switch (key)
                    {
                        case 1:
                            Console.WriteLine("Текущее название рецепта: {0}", recipe.Name == ""?"не установлено":recipe.Name);
                            Console.Write("Введите новое название:");
                            string name = Console.ReadLine();
                            recipe.Name = name;                            
                            break;
                        case 2:
                            AddIngredient(ingredient);
                            break;
                        case 3: SaveRecipe(db, recipe, ingredient);  break;
                        default: break;
                    }
                }
                Console.WriteLine("Сохранено!!!");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void AddIngredient(Ingredient ingredient)
        {
            Console.Write("Введите название ингредиента: ");
            ingredient.Name = Console.ReadLine();
            while (true)
            {
                Console.Write("Введите количество ингредиента: ");
                int count;
                if (int.TryParse(Console.ReadLine(), out count))
                {
                    ingredient.Count = count;
                    break;
                }
                Console.WriteLine("Ошибка!Введите число");
            }
            Console.WriteLine("Введите ед.измерения(Например: кг.,шт. и.т.п)");
            ingredient.Measure = Console.ReadLine();
            Console.WriteLine("Ингредиент добавлен!!!");
        }

        static void SaveRecipe(RecipeContext db, Recipe recipe, Ingredient ingredient)
        {
            db.Recipes.Add(recipe);
            ingredient.Recipe = recipe;
            db.Ingredients.Add(ingredient);

            db.SaveChanges();
        }
    }
}
