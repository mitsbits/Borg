using System;

namespace Borg.Framework.Reflection.Exceptions
{
    public class NotSubclassOfException : BorgException
    {
        public NotSubclassOfException(Type typeToCheck, Type baseType) : base(ExceptionMessage(typeToCheck, baseType))
        {
        }

        private static string ExceptionMessage(Type typeToCheck, Type baseType)
        {
            return $"{typeToCheck.FullName} is not sub class of {baseType.FullName}";
        }
    }
}