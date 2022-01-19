using System;

namespace CSharp_learn.Exceptions;
public class IncorrectInputEx : Exception
{
    public IncorrectInputEx(string message) : base(message)
    { }
}