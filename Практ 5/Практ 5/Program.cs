namespace Практ_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                Order order = new Order();
                order.Start();
                order.SaveToFile("D:\\История заказов.txt");

                Console.Clear();
                Console.WriteLine("Спасибо за заказ. Для нового заказа нажмите Esc");
                key = Console.ReadKey(true);
            } while (key.Key == ConsoleKey.Escape);
        }
    }
}