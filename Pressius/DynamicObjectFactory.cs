using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pressius
{
    public static class PressiusPermutations
    {
        public static IEnumerable<T> Generate<T>(IObjectDefinition objectDefinition)
        {
            var inputGenerator = new DynamicObjectFactory();
            var inputs = inputGenerator
                .SetObjectDefinition(objectDefinition)
                .GeneratePermutations<T>();
            return inputs;
        }

        private class DynamicObjectFactory
        {
            private readonly List<IParameterDefinition> _inputDefinitions;
            private readonly List<IObjectDefinition> _objectDefinitions;

            private Type[] GetTypesInNamespace(string nameSpace)
            {
                var assembly = Assembly.GetExecutingAssembly();
                return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
            }

            public DynamicObjectFactory()
            {
                _inputDefinitions = new List<IParameterDefinition>()
            {
                new StringParameter(),
                new EmailStringParameter(),
                new IntegerParameter(),
                new DateTimeParameter(),
                new DoubleParameter()
            };
                _objectDefinitions = new List<IObjectDefinition>();
            }

            public DynamicObjectFactory SetObjectDefinition(IObjectDefinition objectDefinition)
            {
                _objectDefinitions.Add(objectDefinition);
                return this;
            }

            public IEnumerable<object> GeneratePermutationsFromAssemblyQualifiedClassName(
                string assemblyQualifiedClassName)
            {
                return _generateObjectPermutation(assemblyQualifiedClassName);
            }

            public IEnumerable<T> GeneratePermutations<T>()
            {
                var assemblyQualifiedClassName = typeof(T).AssemblyQualifiedName;
                var permutations = _generateObjectPermutation(assemblyQualifiedClassName);
                return permutations.Select(objectResult => (T)objectResult);
            }

            private IEnumerable<object> _generateObjectPermutation(string assemblyQualifiedClassName)
            {
                var objectDefinition = _objectDefinitions.FirstOrDefault(
                       od => string.Compare(od.AssemblyQualifiedClassName, assemblyQualifiedClassName) == 0);
                if (objectDefinition != null)
                {
                    return _generatePermutationBasedOnObjectDefinition(objectDefinition, assemblyQualifiedClassName);
                }

                return _generatePermutation(assemblyQualifiedClassName);
            }

            private IEnumerable<object> _generatePermutationBasedOnObjectDefinition(
                IObjectDefinition objectDefinition,
                string assemblyQualifiedClassName)
            {
                var classType = Type.GetType(assemblyQualifiedClassName);

                var objectInitiation = objectDefinition.InitiationMethod;
                if (objectInitiation == ObjectInitiation.Constructor)
                {
                    return _generateConstructorPermutation(classType, objectDefinition);
                }
                else if (objectInitiation == ObjectInitiation.Properties)
                {
                    return _generatePropertiesPermutation(classType, objectDefinition);
                }

                throw new Exception("Cannot find object initiation");
            }

            private IEnumerable<object> _generatePermutation(string assemblyQualifiedClassName)
            {
                var classType = Type.GetType(assemblyQualifiedClassName);

                try
                {
                    return _generatePropertiesPermutation(classType);
                }
                catch
                {
                    // swallow here
                }

                try
                {
                    return _generateConstructorPermutation(classType);
                }
                catch
                {
                    throw new Exception("Cannot automatically generate permutations. Please define object definition first");
                }
            }

            private IEnumerable<object> _generateConstructorPermutation(
               Type classType,
               IObjectDefinition objectDefinition = null)
            {
                var classConstructors = classType.GetConstructors().FirstOrDefault();
                var listOfParameters = classConstructors.GetParameters();

                var propertyPermutationLists = new List<List<object>>();
                foreach (var param in listOfParameters)
                {
                    var inputDefinitionType = string.Empty;
                    if (objectDefinition == null)
                    {
                        inputDefinitionType = param.ParameterType.Name;
                    }
                    else if (!objectDefinition.MatcherDictionary.TryGetValue(param.Name, out inputDefinitionType))
                    {
                        inputDefinitionType = param.ParameterType.Name;
                    }

                    var testInput = _inputDefinitions.FirstOrDefault(
                        id => String.Compare(id.TypeName.Name, inputDefinitionType) == 0)?.InputCatalogues;
                    if (testInput == null)
                        throw new Exception("Cannot Found Matching Input Definition");

                    propertyPermutationLists.Add(testInput);
                }

                var attributePermutations = new List<List<object>>();
                propertyPermutationLists.GeneratePermutations(attributePermutations);

                var attributePermutationsMinimal = propertyPermutationLists.GenerateMinimalPermutations();

                var results = attributePermutationsMinimal
                    .Select(ap => Activator.CreateInstance(classType, ap.ToArray()));

                return results;
            }

            private IEnumerable<object> _generatePropertiesPermutation(
                Type classType,
                IObjectDefinition objectDefinition = null)
            {
                var listOfProperties = classType.GetProperties();
                var propertyPermutationLists = new List<List<object>>();
                foreach (var prop in listOfProperties)
                {
                    var inputDefinitionType = string.Empty;
                    if (objectDefinition == null)
                    {
                        inputDefinitionType = prop.PropertyType.Name;
                    }
                    else if (!objectDefinition.MatcherDictionary.TryGetValue(prop.Name, out inputDefinitionType))
                    {
                        inputDefinitionType = prop.PropertyType.Name;
                    }

                    var testInput = _inputDefinitions.FirstOrDefault(
                        id => String.Compare(id.TypeName.Name, inputDefinitionType) == 0)?.InputCatalogues;
                    if (testInput == null)
                        throw new Exception("Cannot Found Matching Input Definition");

                    propertyPermutationLists.Add(testInput);
                }

                var attributePermutations = new List<List<object>>();
                propertyPermutationLists.GeneratePermutations(attributePermutations);

                ObjectPermutationExtension.GeneratePermutations(propertyPermutationLists, attributePermutations);

                //var attributePermutationsMinimal = propertyPermutationLists.GenerateMinimalPermutations();

                var results = new List<object>();
                foreach (var permutationSet in attributePermutations)
                {
                    var constructor = classType.GetConstructor(Type.EmptyTypes);
                    if (constructor == null) throw new Exception("No parameterless constructor in the class.");
                    var newInput = Activator.CreateInstance(classType);
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
        }
    }
}