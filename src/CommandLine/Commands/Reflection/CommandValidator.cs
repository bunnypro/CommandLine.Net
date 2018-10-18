using System;
using System.Collections.Generic;
using System.Linq;
using Bunnypro.CommandLine.Commands.InternalExceptions;

namespace Bunnypro.CommandLine.Commands.Reflection
{
    public static class CommandValidator
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
            catch (ExecutionMethodConflictException e)
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

            if (!reflection.HasExecutionMethod)
                throw new MissingExecutionMethodException(command);

            if (reflection.HasExecutionMethodConflict)
                throw new ExecutionMethodConflictException(command);

            foreach (var method in reflection.ExecutionMethods)
            {
                if (method.IsAcceptOptions && !method.Parameters.First().IsOptions)
                    throw new ExecutionMethodParameterOrderException(method);
                    
                if (method.IsAcceptOptions && !command.Options.Any())
                    throw new UnexpectedOptionsParameter(method);
                
                if (method.IsAcceptOptions && method.Parameters.Count(p => p.IsOptions) > 1)
                    throw new MultipleCommandOptionDefinitionException(method);

                foreach (var parameter in method.Parameters)
                {
                    if (!AllowedMethodParameterTypes.Contains(parameter.Type))
                        throw new UnsupportedParameterTypeException(parameter);
                }
            }
        }
    }
}