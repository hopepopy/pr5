using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практ_5
{
    internal class Order
    {
        private List<Menuitem> menu; 
        
        public Order()
        {
            menu = new List<Menuitem>();

            Menuitem form = new Menuitem("Форма");
            Menuitem size = new Menuitem("Размер");
            Menuitem flavor = new Menuitem("Вкус");
            Menuitem count = new Menuitem("Количество коржей");
            Menuitem icing = new Menuitem("Глазурь");
            Menuitem decor = new Menuitem("Декор");

            Subitem form1 = new Subitem("Круг", 500);
            Subitem form2 = new Subitem("Квадрат", 500);
            Subitem form3 = new Subitem("Сердце", 700);
            form.all.Add(form1);
            form.all.Add(form2);
            form.all.Add(form3);
            menu.Add(form);

            Subitem size1 = new Subitem("Маленький", 1000);
            Subitem size2 = new Subitem("Средний", 1200);
            Subitem size3 = new Subitem("Большой", 2000);
            size.all.Add(size1);
            size.all.Add(size2);
            size.all.Add(size3);
            menu.Add(size);

            Subitem flavor1 = new Subitem("Ваниль", 100);
            Subitem flavor2 = new Subitem("Шоколад", 100);
            Subitem flavor3 = new Subitem("Кокос", 250);
            flavor.all.Add(flavor1);
            flavor.all.Add(flavor2);
            flavor.all.Add(flavor3);
            menu.Add(flavor);

            Subitem count1 = new Subitem("1", 200);
            Subitem count2 = new Subitem("2", 400);
            Subitem count3 = new Subitem("3", 600);
            count.all.Add(count1);
            count.all.Add(count2);
            count.all.Add(count3);
            menu.Add(count);

            Subitem icing1 = new Subitem("Шоколадная", 150);
            Subitem icing2 = new Subitem("Ягодная", 150);
            Subitem icing3 = new Subitem("Кремовая", 150);
            icing.all.Add(icing1);
            icing.all.Add(icing2);
            icing.all.Add(icing3);
            menu.Add(icing);

            Subitem decor1 = new Subitem("Посыпка", 50);
            Subitem decor2 = new Subitem("Свечи", 100);
            Subitem decor3 = new Subitem("Фрукты", 150);
            decor.all.Add(decor1);
            decor.all.Add(decor2);
            decor.all.Add(decor3);
            menu.Add(decor);
        }

        public void Start()
        {
            SelectItems();
        }

        private int GetTotalPrice()
        {
            int price = 0;
            foreach (Menuitem item in menu)
            {
                if (item.selected != null)
                {
                    price += item.selected.price;
                }
            }
            return price;
        }

        private string GetDetails()
        {
            string details = "";
            foreach (Menuitem item in menu)
            {
                if (item.selected != null)
                {
                    details += $"{item.selected.name} - {item.selected.price};";
                }
            }
            return details;
        }

        private void ShowMenu()
        {
            Console.Clear();

            int price = GetTotalPrice();
            string details = GetDetails();

            Console.WriteLine("Выбрите составляющие торта:");
            foreach (Menuitem item in menu)
            {
                Console.WriteLine($"  {item.name}");
            }
            Console.WriteLine("  Конец заказа");
            Console.WriteLine();
            Console.WriteLine($"Итоговая цена: {price}");
            Console.WriteLine($"Ваш торт: {details}");
        }

        private void SelectItems()
        {
            ShowMenu();
            Arrow arrow = new Arrow(1, menu.Count + 1);
            bool end = false;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (!end)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.Next();
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.Back();
                        break;
                    case ConsoleKey.Enter:
                        int index = arrow.GetIndex();
                        if (index == menu.Count)
                        {
                            end = true;
                        }
                        else
                        {
                            Menuitem menuitem = menu[index];
                            SelectSubItem(menuitem);
                            ShowMenu();
                            arrow.Print();
                        }

                        break;
                }
                if (!end)
                {
                    key = Console.ReadKey(true);
                }
            }
        }
        private void SelectSubItem(Menuitem menuitem)
        {
            Console.Clear();
            Console.WriteLine("Для возврата нажмите Esc.");
            List<Subitem> subitems = menuitem.all;
            foreach (Subitem subitem in subitems)
            {
                Console.WriteLine($"  {subitem.name} - {subitem.price}");
            }
            Arrow arrow = new Arrow(1, subitems.Count);

            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Escape)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        arrow.Next();
                        break;
                    case ConsoleKey.UpArrow:
                        arrow.Back();
                        break;
                    case ConsoleKey.Enter:
                        int index = arrow.GetIndex();
                        menuitem.selected = subitems[index];
                        return;
                }

                key = Console.ReadKey(true);
            }
            menuitem.selected = null;
        }

        public void SaveToFile(string path)
        {
            int price = GetTotalPrice();
            string details = GetDetails();
            DateTime date = DateTime.Now;
            File.AppendAllText(path, $"Заказ от {date}:\n\tЗаказ:{details}\n\tЦена:{price}\n");
        }
    }
}
