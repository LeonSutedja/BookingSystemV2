using System;
using System.Collections.Generic;

namespace LeonSutedja.Pressius.TestEngine
{
    public class InputGenerator
    {
        private readonly List<object> _stringTestsInput = new List<object>
            { "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "~!@#$%&*()_+=-`\\][{}|;:,./?><'\"", "defaultEmail@default.com" };
        private readonly List<object> _integerTestsInput = new List<object>
            { 10, 100, 1000, 10000, 100000, 1000000 };
        private readonly List<object> _dateTimeTestsInput = new List<object>
            { DateTime.Now, DateTime.MinValue, DateTime.MaxValue };

        public InputGenerator() { }

        public List<object> CreateInputs(string assemblyQualifiedClassName)
        {
            var objectList = new List<object>();
            var typeGetType = Type.GetType(assemblyQualifiedClassName);
            var inputObject = Activator.CreateInstance(typeGetType);
            var listOfProperties = inputObject.GetType().GetProperties();

            var propertyPermutationLists = new List<List<object>>();
            foreach (var prop in listOfProperties)
            {
                switch (prop.PropertyType.Name)
                {
                    case "String":
                        propertyPermutationLists.Add(_stringTestsInput);
                        break;
                    case "DateTime":
                        propertyPermutationLists.Add(_dateTimeTestsInput);
                        break;
                    default:
                        break;
                }
            }

            var attributePermutations = new List<List<object>>();
            GeneratePermutations(propertyPermutationLists, attributePermutations);

            var results = new List<object>();
            foreach (var permutationSet in attributePermutations)
            {
                var newInput = Activator.CreateInstance(typeGetType);
                for (var i = 0; i < listOfProperties.Length; i++)
                {
                    var prop = listOfProperties[i];
                    var propValue = permutationSet[i];
                    prop.SetValue(newInput, propValue);
                }
                results.Add(newInput);
            }

            return results;
        }

        public void GeneratePermutations(
            List<List<object>> propertyPermutationLists,
            List<List<object>> results,
            List<object> currentPropertyValues = null,
            int propertyIndex = 0)
        {
            if (propertyIndex == propertyPermutationLists.Count)
            {
                results.Add(currentPropertyValues);
                return;
            }

            var propertyLists = propertyPermutationLists[propertyIndex];

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
                GeneratePermutations(propertyPermutationLists, results, currentPropertyValues, propertyIndex + 1);

                currentPropertyValues = newPropertyValues;
            }
        }
    }

    public class PropertyValue
    {
        public string Name { get; private set; }
        public string PropertyType { get; private set; }
        public object Value { get; private set; }

        public PropertyValue(string name, string propertyType, object value)
        {
            Name = name;
            PropertyType = propertyType;
            Value = value;
        }
    }
}