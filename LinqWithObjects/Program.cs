using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

List<Exception> exceptions = new() {
    new ArgumentException(),
    new SystemException(),
    new IndexOutOfRangeException(),
    new InvalidOperationException(),
    new NullReferenceException(),
    new InvalidCastException(),
    new OverflowException(),
    new DivideByZeroException(),
    new ApplicationException()
};


string[] names = new[] {"Michael", "Pam", "Jim", "Dwight","Angela", "Kevin", "Toby", "Creed"};

/* Filtering with Where
 names end with an M?
 1-(written using a LINQ extension method)
 2-(written using LINQ query comprehension syntax)
 names with length > 4 */

var query1 = names.Where(n => n.EndsWith("m"));
var query2 = from name in names where name.EndsWith("m") select name;

var query3 = names.Where(nameLongerThanFour);

static bool nameLongerThanFour(string name) {
    return name.Length > 4;
}

/* Sorting itmes by one property : length
 by 2 property : ThenBy : length & name */

var query4 = names
    .Where(n => n.Length > 4)
    .OrderBy(n => n.Length)
    .ThenBy(n => n);

/* Filtering by type
 */
var arithmeticExceptionsQuery = exceptions.OfType<ArithmeticException>();

foreach (var ex in arithmeticExceptionsQuery) {
    WriteLine(ex);
}

