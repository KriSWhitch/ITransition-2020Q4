using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Камень_ножницы_бумага
{

    class Program
    {
        static string HMACHASH(string str, string key)
        {
            byte[] bkey = Encoding.Default.GetBytes(key);
            using (var hmac = new HMACSHA256(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(str);
                var bhash = hmac.ComputeHash(bstr);
                return BitConverter.ToString(bhash).Replace("-", string.Empty).ToLower();
            }
        }

        static void Main(string[] args)
        {
            if (args.Length % 2 == 0 || args.Length < 3) 
            {
                Console.WriteLine($"Вы должны передать нечётное количество параметров, больше или равное 3-м!");
                return;
            }
            

            byte[] data = new byte[32];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(data); // should include zero bytes?
            string number = BitConverter.ToString(data, 0).Replace("-", "");

            //Создание объекта для генерации чисел
            Random rnd = new Random();
            //Получить случайное число (в диапазоне от 0 до 10)
            int value = rnd.Next(1, args.Length+1);

            string computerChoose = args[value - 1];

            Console.WriteLine($"HMAC : {HMACHASH(computerChoose, number)}");

            int center = args.Length / 2;

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"{i+1} - {args[i]}");
            }
            Console.WriteLine($"0 - Exit");

            Console.Write($"Вы выбрали: ");
            int choose =  int.Parse(Console.ReadLine());

            if (choose == 0) return;

            Console.WriteLine($"Вы выбрали: {args[choose - 1]}");
            Console.WriteLine($"Компьютер выбрал: {args[value - 1]}");

            string yourChoose = args[choose - 1];


            while (args[center] != yourChoose)
            {
                string argsLast = args[args.Length - 1];
                for (int j = args.Length - 1; j > 0; j--) args[j] = args[j - 1];
                args[0] = argsLast;
            }

            bool flag = false;

            for (int i = 0; i < args.Length / 2 + 1; i++)
            {
                if (computerChoose == args[i]) flag = true;
            }

            
            if (computerChoose == yourChoose) Console.WriteLine($"Ничья!");
            else if (flag == true) Console.WriteLine($"Вы победили!");
            else Console.WriteLine($"Вы проиграли!");

            Console.WriteLine($"HMAC key : {number}");

        }
    }
}