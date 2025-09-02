using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace FullStackCase
{
    internal class MaxIncreasingSubArrayAsJson
    {
        public static string MaxIncreasingSubArrayAsJsonMethod(List<int> numbers)
        {

            if (numbers == null || numbers.Count == 0) //boş veya null kontrolü
                return JsonSerializer.Serialize(new List<int>());

            List<int> maxSub = new List<int>(); //en büyük toplamlı alt diziyi tutar
            List<int> curSub = new List<int> { numbers[0] }; //mevcut alt diziyi tutar

            foreach (var num in numbers.Skip(1)) //listenin ilk elemanını atlıyor (curSub zaten ilk elemanı içerdiği için)
            {
                if (num > curSub.Last())
                {
                    curSub.Add(num);
                }
                else
                {
                    if (curSub.Sum() > maxSub.Sum())
                        maxSub = new List<int>(curSub);

                    curSub = new List<int> { num };
                }
            }

            if (curSub.Sum() > maxSub.Sum())
                maxSub = curSub;

            return JsonSerializer.Serialize(maxSub);
        }
    }
}
