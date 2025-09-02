using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace FullStackCase
{
    internal class LongestVowelSubsequenceAsJson
    {
        public static string LongestVowelSubsequenceAsJsonMethod(List<string> words)
        {

            if (words == null || words.Count == 0)  //boş veya null kontrolü
                return JsonSerializer.Serialize(new List<object>());

            // sesli harfler
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' }; //daha okunabilir ve optimize olduğu için HashSet kullanıldı

            var result = new List<object>(); //JSON çıkış(output) listesi 

            foreach (var word in words)
            {
                string maxSeq = ""; //en uzun sesli harf dizisini tutar
                string curSeq = ""; //mevcut sesli harf dizisini tutar

                foreach (var v in word.ToLower()) //kelimeyi küçük harfe çevirip her harfi kontrol eder
                {
                    if (vowels.Contains(v))
                    {
                        curSeq += v;
                        if (curSeq.Length > maxSeq.Length)
                            maxSeq = curSeq;
                    }
                    else
                    {
                        curSeq = ""; //sessiz bir harf ise dizi kesilir
                    }
                }

                result.Add(new
                {
                    word = word,
                    sequence = maxSeq,
                    length = maxSeq.Length
                });
            }

            return JsonSerializer.Serialize(result);
        }
    }
}
