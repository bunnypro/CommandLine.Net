using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Commands.InternalExceptions;
using Bunnypro.CommandLine.Commands.Reflection;

namespace Bunnypro.CommandLine.Commands
{
    internal static class CommandValidator
    {
        public static readonly IEnumerable<Type> AllowedMethodParameterTypes = new[]
        {
            typeof(IDictionary<Option, object>),
            typeof(int),
            typeof(string),
            typeof(char),
            typeof(object)
        };
        
        public static bool TryValidate(Command command)
        {
            try
            {
                Validate(command);
            }
            catch (ExecutableMethodConflictException e)
            {
                Console.WriteLine($"{e.Command.GetType().Name} has method conflict");
                throw;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        public static void Validate(Command command)
        {
            var reflection = new CommandInfo(command);

            if (!reflection.HasExecutableMethod)
                throw new MissingExecutableMethodException(command);

            if (reflection.HasExecutableMethodConflict)
                throw new ExecutableMethodConflictException(command);

            foreach (var method in reflection.ExecutableMethods)
            {
                if (method.IsAcceptOptions && !method.Parameters.First().IsOptions)
                    throw new ExecutableMethodParameterOrderException(method);
                    
                if (method.IsAcceptOptions && !command.Options.Any())
                    throw new UnexpectedOptionsParameter(method);
                
                if (method.IsAcceptOptions && method.Parameters.Count(p => p.IsOptions) > 1)
                    throw new MultipleOptionsDefinitionException(method);

                foreach (var parameter in method.Parameters)
                {
                    if (!AllowedMethodParameterTypes.Contains(parameter.Type))
                        throw new UnsupportedParameterTypeException(parameter);
                }
            }
        }
    }
}