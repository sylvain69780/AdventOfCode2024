var test2 = @"Register A: 22817223
Register B: 0
Register C: 0

Program: 2,4,1,2,7,5,4,5,0,3,1,7,5,5,3,0";

var test = @"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0";

var test3 = @"Register A: 2024
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0";

//4,3,7,1,5,3,0,5,4   /// 9*3 = 27 bits 
var solution = new Solution(test2);

Console.WriteLine(solution.Run());