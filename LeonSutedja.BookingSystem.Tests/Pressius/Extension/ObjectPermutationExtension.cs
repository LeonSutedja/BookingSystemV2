using System.Collections.Generic;
using System.Linq;

namespace LeonSutedja.Pressius
{
    public static class ObjectPermutationExtension
    {
        public static List<List<object>> GenerateMinimalPermutations(this List<List<object>> propertyPermutationLists)
        {
            var results = new List<List<object>>();
            var firstOfEveryList = propertyPermutationLists.Select(ppl => ppl[0]).ToList();
            for (var i = 0; i < propertyPermutationLists.Count; i++)
            {
                var permutationList = propertyPermutationLists[i];
                for (var j = 0; j < permutationList.Count; j++)
                {
                    if (i > 0 && j == 0) continue;

                    var value = permutationList[j];
                    var newObjectList = new List<object>();
                    for (var x = 0; x < propertyPermutationLists.Count; x++)
                    {
                        var valueToBeAdded = (x == i) ? value : firstOfEveryList[i];
                        newObjectList.Add(valueToBeAdded);
                    }
                    results.Add(newObjectList);
                }
            }

            return results.Distinct().ToList();
        }

        public static void GeneratePermutations(this List<List<object>> permutationLists, List<List<object>> results)
        {
            _generatePermutations(permutationLists, results);
        }

        private static void _generatePermutations(
            List<List<object>> permutationLists,
            List<List<object>> results,
            List<object> currentPropertyValues = null,
            int propertyIndex = 0)
        {
            if (propertyIndex == permutationLists.Count)
            {
                results.Add(currentPropertyValues);
                return;
            }

            var propertyLists = permutationLists[propertyIndex];

            for (var i = 0; i < propertyLists.Count; i++)
            {
                var newPropertyValues = new List<object>();

                if (currentPropertyValues != null)
                {
                    newPropertyValues = new List<object>();
                    foreach (var propValues in currentPropertyValues)
                    {
                        newPropertyValues.Add(propValues);
                    }
                }

                if (currentPropertyValues == null || propertyIndex == 0)
                {
                    currentPropertyValues = new List<object>();
                }

                var propertyToBeAded = propertyLists[i];
                currentPropertyValues.Add(propertyToBeAded);
                _generatePermutations(permutationLists, results, currentPropertyValues, propertyIndex + 1);

                currentPropertyValues = newPropertyValues;
            }
        }
    }
}
