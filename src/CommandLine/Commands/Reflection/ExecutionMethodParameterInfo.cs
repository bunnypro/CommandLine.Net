using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    internal sealed class ExecutionMethodParameterInfo
    {
        private readonly ParameterInfo _parameter;

        public ExecutionMethodParameterInfo(ExecutionMethodInfo method, ParameterInfo parameter)
        {
            Method = method;
            _parameter = parameter;
        }
        
        public ExecutionMethodInfo Method { get; }

        public object Name => _parameter.Name;
        public Type Type => _parameter.ParameterType;
        public string TypeName => IsAny ? "any" : Type.Name.ToLower();
        
        public bool IsOptions => Type == typeof(IDictionary<Option, object>);
        public bool IsArgument => !IsOptions;
        
        public bool IsAny => Type.IsInstanceOfType(typeof(object));
    }
}