﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace LeonSutedja.Pressius
{
    public abstract class DefaultParameterDefinition : IParameterDefinition
    {
        public abstract List<object> InputCatalogues { get; }

        public abstract ParameterTypeDefinition TypeName { get; }

        public virtual bool IsMatch(PropertyInfo propertyInfo)
            => String.Compare(propertyInfo.PropertyType.Name, TypeName.Name) == 0;

        public virtual bool IsMatch(ParameterInfo parameterInfo)
            => String.Compare(parameterInfo.ParameterType.Name, TypeName.Name) == 0;
    }

    public class StringParameter : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "~!@#$%&*()_+=-`\\][{}|;:,./?><'\"",
                "defaultEmail@default.com" };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("String");
    }

    public class EmailStringParameter : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> { "A@a.com" };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("Email");

        public override bool IsMatch(PropertyInfo propertyInfo)
        {
            var isString = propertyInfo.PropertyType.Name.Contains("String");
            var isEmail = propertyInfo.Name.Contains("Email");
            return isString && isEmail;
        }
    }

    public class IntegerParameter : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> { 10, 100, 1000, 10000, 100000, 1000000 };
        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("Integer");
    }

    public class DateTimeParameter : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues => new List<object>
            { DateTime.Now, DateTime.MinValue, DateTime.MaxValue };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("DateTime");
    }

    public class DoubleParameter : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues => new List<object>
            { 0.1, Double.MaxValue, Double.MinValue };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("Double");
    }
}