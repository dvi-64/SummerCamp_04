using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //enum, классы не используем. не проходили еще =)

            int attackValue = 25; // значение атаки
            int defenceValue = 50; // процент защиты жертвы, 0-99
            int attackFraction = 0; // фракция атакующего: 0- Добрые, 1- нейтральные, 2- злые
            int victimFraction = 1; // фракция жертвы: 0- Добрые, 1- нейтральные, 2- злые
            bool isBerserkAttack = false; // состояние берсерка атакующего
            bool isBerserkVictim = false; // состояние берсерка жертвы
            string input = string.Empty; // переменная для чтения ввода

            // читаем значение атаки
            do
            {
                Console.WriteLine("Введите значение атаки атакующего");
                input = Console.ReadLine();
            } while (int.TryParse(input, out attackValue) == false
                    || attackValue <= 0);
            Console.WriteLine($"Введено значение атаки {attackValue}\r\n");

            // читаем процент защиты
            do
            {
                Console.WriteLine("Введите процент защиты атакуемого");
                input = Console.ReadLine();
            } while (int.TryParse(input, out defenceValue) == false
                    || defenceValue < 0
                    || defenceValue > 99);// по условию задачи не ясно как расчитывается урон при 100% защиты
            Console.WriteLine($"Введено значение процента защиты {defenceValue}\r\n");

            // читаем значение фракции атакующего 
            do
            {
                Console.WriteLine("Введите значение фракции атакующего (0- Добрые, 1- Нейтральные, 2- Злые)");
                input = Console.ReadLine();
            } while (int.TryParse(input, out attackFraction) == false
                    || attackFraction < 0
                    || attackFraction > 2);
            switch (attackFraction)
            {
                case 0:
                    Console.WriteLine($"Атакует фракция Добрые\r\n");
                    break;
                case 1:
                    Console.WriteLine($"Атакует фракция Нейтральные\r\n");
                    break;
                case 2:
                    Console.WriteLine($"Атакует фракция Злые\r\n");
                    break;
                default:// на всякий случай
                    attackFraction = 1;
                    Console.WriteLine($"Значение не корректное. Полагаем, что атакует фракция Нейтральные\r\n");
                    break;
            }

            // читаем значение фракции жертвы 
            do
            {
                Console.WriteLine("Введите значение фракции жертвы (0- Добрые, 1- Нейтральные, 2- Злые)");
                input = Console.ReadLine();
            } while (int.TryParse(input, out victimFraction) == false
                    || victimFraction < 0
                    || victimFraction > 2);
            switch (victimFraction)
            {
                case 0:
                    Console.WriteLine($"Жертва из фракция Добрые\r\n");
                    break;
                case 1:
                    Console.WriteLine($"Жертва из фракция Нейтральные\r\n");
                    break;
                case 2:
                    Console.WriteLine($"Жертва из фракция Злые\r\n");
                    break;
                default:// на всякий случай
                    victimFraction = 1;
                    Console.WriteLine($"Значение не корректное. Полагаем, что жертва из фракция Нейтральные\r\n");
                    break;
            }

            // читаем используется ли берсерк у атакующего
            do
            {
                Console.WriteLine("Введите используется ли состояние берсерка у атакующего (false- не используется, true- используется)");
                input = Console.ReadLine();
            } while (bool.TryParse(input, out isBerserkAttack) == false);
            if (isBerserkAttack == true)
            {// да тут скобки можно опустить, но не рассказывали об этом
                Console.WriteLine($"Персонаж атакует в состоянии берсерка\r\n");
            }
            else
            {
                Console.WriteLine($"Персонаж атакует без состояния берсерка\r\n");
            }

            // читаем используется ли берсерк у жертвы
            do
            {
                Console.WriteLine("Введите используется ли состояние берсерка у жертвы (false- не используется, true- используется)");
                input = Console.ReadLine();
            } while (bool.TryParse(input, out isBerserkVictim) == false);
            if (isBerserkVictim == true)
            {// да тут скобки можно опустить, но не рассказывали об этом
                Console.WriteLine($"Персонаж жертвы в состоянии берсерка\r\n");
            }
            else
            {
                Console.WriteLine($"Персонаж жертвы без состояния берсерка\r\n");
            }

            // получив все вводные данные, расчитываем
            // влияние фракции
            float fractionDamage = 1;
            if ((victimFraction == 0 && attackFraction == 0)
                || (victimFraction == 2 && attackFraction == 2))
            {
                fractionDamage = 0.5f;
            }
            else if ((victimFraction == 0 && attackFraction == 2)
                || (victimFraction == 2 && attackFraction == 1))
            {
                fractionDamage = 1.5f;
            }

            // влияние состояния берсерка
            int berserkAttackMult = 1;
            float berserkDamageVictim = 1;
            if (isBerserkAttack == true)
            {
                berserkAttackMult = 2;
            }
            if (isBerserkVictim == true)
            {
                berserkDamageVictim = 0.2f;
            }
            //юниту с защитой 10 нанесено 25 урона, он в итоге получит только 25 * 0,9 = 22,5 урона)
            // считаем урон
            float resultAttackValue = attackValue * fractionDamage * berserkAttackMult;

            //считаем защиту
            float resultDefenceValue = (100 - defenceValue) * berserkDamageVictim / 100;

            // финальный урон
            float result = resultDefenceValue * resultAttackValue;
            Console.WriteLine($"В результате атаки будет получено {result} очков урона");

            while (true)
            {
                Console.WriteLine("Завершить бой? (y - да)");
                if (Console.ReadLine() == "y")
                {
                    break;
                }
            }
        }
    }
}
