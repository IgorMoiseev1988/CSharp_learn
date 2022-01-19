using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Calculator;
public record Fraction
{
    //LOWTODO support negative value
    //TODO add support irregular fraction

    public int Nominator { get; private set; }
    public int Denominator { get; private set; }
    public Fraction(int nominator = 0, int denominator = 1)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator must be not equal 0");
        }
        Nominator = nominator;
        Denominator = denominator;
        Normolize();
    }
    public static Fraction operator +(Fraction a, Fraction b)
    {
        int common_denominator = NOK(a.Denominator, b.Denominator);

        int a_nominator = common_denominator / a.Denominator * a.Nominator;
        int b_nominator = common_denominator / b.Denominator * b.Nominator;
        return new Fraction(a_nominator + b_nominator, common_denominator);
    }
    public static Fraction operator -(Fraction a, Fraction b)
    {
        b.Nominator = -b.Nominator;
        return a + b;
    }
    public static Fraction operator *(Fraction a, Fraction b)
    {
        int nom_nod = NOD(a.Nominator, b.Denominator);
        int denom_nod = NOD(a.Denominator, b.Nominator);

        int new_nominator = (a.Nominator / nom_nod) * (b.Nominator / denom_nod);
        int new_denomintor = (a.Denominator / denom_nod) * (b.Denominator / nom_nod);
        return new Fraction(new_nominator, new_denomintor);
    }
    public static Fraction operator /(Fraction a, Fraction b)
    {
        return a * b.Reverse();
    }
    private Fraction Reverse() => new(this.Denominator, this.Nominator);

    static private int NOD(int a, int b)
    {
        int t;
        while (b != 0)
        {
            t = b;
            b = a % b;
            a = t;
        }
        return Math.Abs(a);
    }

    static private int NOK(int a, int b) => a / NOD(a, b) * b;
    private void Normolize()
    {
        if (Denominator < 0)
        {
            Nominator = -Nominator;
            Denominator = -Denominator;
        }
        int nod = NOD(Nominator, Denominator);
        Nominator /= nod;
        Denominator /= nod;
        
    }

    public override string ToString()
    {
        if (Denominator == 1)
        {
            return $"{Nominator}";
        }
        return $"{Nominator}/{Denominator}";
    }

    static public (string? ErrorMassage, Fraction? Fraction) Parse(string? fraction_str, char splitter = '/')
    {
        if (fraction_str == null)
        {
            return ("Нахуй null!", null);
        }
        fraction_str = fraction_str.Replace(" ", "");
        if (fraction_str.Contains(splitter))
        {
            var strings = fraction_str.Split(splitter);
            if (strings.Length != 2)
            {
                return ("Нахуй корявые дроби!", null);
            }
            return Fraction.CreateFromTwoStrings(strings[0], strings[1]);
        }
        return Fraction.CreateFromOneString(fraction_str);
    }

    static private (string? ErrorMassage, Fraction? Fraction) CreateFromTwoStrings(string a_str, string b_str)
    {
        if (!int.TryParse(a_str, out int a))
        {
            return ("Нахуй корявый числитель!", null);
        }
        if (!int.TryParse(b_str, out int b))
        {
            return ("Нахуй корявый знаменатель!", null);
        }
        return (null, new Fraction(a, b));
    }
    static private (string? ErrorMassage, Fraction? Fraction) CreateFromOneString(string str)
    {
        if (int.TryParse(str, out int a))
        {
            return (null, new Fraction(a, 1));
        }
        return ("Нахуй корявую дробь!", null);
    }


}
