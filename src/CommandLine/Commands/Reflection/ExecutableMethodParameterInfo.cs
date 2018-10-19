using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class ExecutableMethodParameterInfo
    {
        private readonly ParameterInfo _parameter;

        public ExecutableMethodParameterInfo(ExecutableMethodInfo method, ParameterInfo parameter)
        {
            Method = method;
            _parameter = parameter;
        }
        
        public ExecutableMethodInfo Method { get; }

        public object Name => _parameter.Name;
        public Type Type => _parameter.ParameterType;
        public string TypeName => IsAny ? "any" : Type.Name.ToLower();
        
        public bool IsOptions => Type == typeof(IDictionary<Option, object>);
        public bool IsArgument => !IsOptions;
        
        public bool IsAny => Type.IsInstanceOfType(typeof(object));
    }
}