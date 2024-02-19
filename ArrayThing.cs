 var array = new[] { -1, 23, -3, 11, 5, 0, -9, 12, 1 };

//Zero is a positive number in terms of how it's stored in the memory (there're no sign bit)
var positiveArray = array.Where(number => number >= 0).ToArray();
for (int i = positiveArray.Length - 1; i >= 0; i--)
{
    Console.WriteLine(positiveArray[i]);
}
