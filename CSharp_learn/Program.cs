// See https://aka.ms/new-console-template for more information
namespace CSharp_learn;

public class Program
{
    const string ValidSigns = "+-/*";
    static Fraction GetDrob(string? hint = null)
    {
        if (string.IsNullOrEmpty(hint))
        {
            hint = "Введите дробь (a/b): ";
        }
        int counter = 3;
        while(counter > 0)
        {
            Console.Write(hint);
            var result = Fraction.Parse(Console.ReadLine());
            
            if (result.Fraction is null) // in one line declare and init new var and check result
            {
                Console.WriteLine(result.ErrorMassage);
            }
            else
            {
                return result.Fraction;
            } 
            --counter;
        }
        throw new Exceptions.IncorrectInputEx("Ебать ты лох, с трех раз не смог нормально дробь ввести, животное!");
    }
    static char? GetSign(string? hint = null)
    {
        if (string.IsNullOrEmpty(hint))
        {
            hint = "Введите знак: ";
        }
        int counter = 3;
        while (counter > 0)
        {
            Console.Write(hint);
            if (char.TryParse(Console.ReadLine(), out char result) && (ValidSigns.Contains(result)))
            {
                return result;
            }
            counter--;
            Console.WriteLine("Ошибка! Поддерживаемые операции: " + string.Join(' ', ValidSigns.ToCharArray()));
        }
        throw new Exceptions.IncorrectInputEx("Ты че, пёс, знак ввести не можешь? Пошёл нахуй!");
    }
    static Fraction Calculate(Fraction a, Fraction b, char s) => s switch
    {
        '+' => a + b,
        '-' => a - b,
        '*' => a * b,
        '/' => a / b,
        _ => throw new NotSupportedException("Хуйня какая-то произошла")
    };
    static void Calc()
    {
        Fraction a, b, r;
        char? s;
        try
        {
            a = GetDrob();
            b = GetDrob();
            s = GetSign();
            r = Calculate(a, b, s!.Value);
        } catch (Exceptions.IncorrectInputEx ex)
        {
            Console.WriteLine(ex.Message);
            return;
        } catch (NotSupportedException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        
        Console.WriteLine($"Ответ: {r}");
    }

    public static void Main() => Calc();
}