using System.Collections.Generic;
using System.Reflection;

namespace LeonSutedja.Pressius
{
    public interface IParameterDefinition
    {
        List<object> InputCatalogues { get; }

        ParameterTypeDefinition TypeName { get; }

        bool IsMatch(PropertyInfo propertyInfo);

        bool IsMatch(ParameterInfo parameterInfo);
    }
}