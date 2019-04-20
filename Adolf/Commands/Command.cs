using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Adolf.Attributes;

namespace Adolf.Commands
{
    /// <example>
    /// adolf work -id 12345 (with named arguments)
    /// adolf work 658 (with default argument)
    /// adolf #12345 (with shorthand argument)
    /// </example>
    public abstract class Command
    {
        public abstract Task Execute();
        
        public static Command Load(string[] args)
        {
            var firstArg = args.FirstOrDefault();

            // Check for shorthand code, i.e. #123 (work item 123)
            if (firstArg != null)
            {
                if (firstArg.StartsWith("#")) return new WorkItemCommand(Convert.ToInt32(firstArg.TrimStart('#')));
            }

            args = args.Skip(1).ToArray(); // Lose the command name
            var type = typeof(Command).Assembly.GetTypes()
                .Where(t => typeof(Command).IsAssignableFrom(t))
                .ToDictionary(t => t, t => t.GetCustomAttribute<AliasAttribute>()?.Value)
                .Where(x => x.Value != null)
                .SingleOrDefault(t => t.Value.Equals(firstArg, StringComparison.InvariantCultureIgnoreCase))
                .Key;
            
            if (type == null) throw new ArgumentException($"Unknown command: {firstArg}", firstArg);

            var command = (Command)Activator.CreateInstance(type);
            command.SetDefaultArguments(args);
            command.SetNamedArguments(args);
            return command;
        }

        private void SetDefaultArguments(IReadOnlyList<string> args)
        {
            var defaultProps = GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<DefaultArgumentAttribute>() != null)
                .ToList();

            for (var index = 0; index < args.Count && index < defaultProps.Count; index++)
            {
                var arg = args[index];
                var property = defaultProps[index];
                if (arg.StartsWith("-")) return;

                var typedValue = Convert.ChangeType(arg, property.PropertyType);
                defaultProps[index].SetValue(this, typedValue);
            }
        }

        private void SetNamedArguments(IReadOnlyCollection<string> args)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            
            // Check named arguments
            for (var index = 0; index < args.Count; index++)
            {
                var argName = args.ElementAtOrDefault(index);

                if (string.IsNullOrWhiteSpace(argName)) continue;
                if (argName.First() != '-') continue; // Only process actual argument names
                argName = argName.Substring(1, argName.Length - 1);

                var value = (args.ElementAtOrDefault(index + 1));
                var property = GetType().GetProperty(argName, bindingFlags)
                    ?? GetType().GetProperties().SingleOrDefault(p => p.GetCustomAttribute<AliasAttribute>()?.Value == argName);

                if (value?.First() == '-') value = null; // It must be an argument name, ignore it
                if (property == null) throw new ArgumentException($"Invalid argument: {argName}", argName);
                if (!property.CanWrite) throw new ArgumentException($"Can't set property: {property.Name}", argName); // Check yourself before you reflect yourself.

                var typedValue = Convert.ChangeType(value, property.PropertyType);
                if (property.PropertyType == typeof(bool) && typedValue == null) property.SetValue(this, true); 
                else property.SetValue(this, typedValue);
            }
        }
    }
}