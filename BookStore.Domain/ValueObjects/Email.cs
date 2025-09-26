using System;
using System.Text.RegularExpressions;


namespace BookStore.Domain.ValueObjects;


public readonly struct Email
{
    private static readonly Regex EmailRegex = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);


    public string Value { get; }


    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email is required", nameof(value));
        if (!EmailRegex.IsMatch(value)) throw new ArgumentException("Invalid email format", nameof(value));
        Value = value;
    }


    public override string ToString() => Value;
}