﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Borg.Infrastructure.Core
{
    [DebuggerStepThrough]
    internal static class Preconditions
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration] T value, [InvokerParameterName, NotNull] string parameterName)
            where T : class
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName, NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException("String value cannot be null.", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static DateTime NotEmpty(DateTime value, [InvokerParameterName, NotNull] string parameterName)
        {
            if (value.Equals(default(DateTime)))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static TEnum IsDefined<TEnum>(TEnum value, [InvokerParameterName, NotNull] string parameterName) where TEnum : struct
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentOutOfRangeException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static int PositiveOrZero([NoEnumeration] int value, [InvokerParameterName, NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            if (value < 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException("Int value cannot be less than zero.", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> value, [InvokerParameterName, NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Count() < 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException("Collection must have one ore more elements.", parameterName);
            }

            return value;
        }
    }
}