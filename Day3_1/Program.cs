using System.Text.RegularExpressions;

var test = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

var matches = Regex.Matches(test, @"mul\((\d{1,3}),(\d{1,3})\)");

foreach (Match match in matches)
{
    match.Dump();
    Console.WriteLine(match.Value);
}
