﻿using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Borg
{
    [DebuggerStepThrough]
    public static class LoggerExtensions
    {
        public static void Warn(this ILogger logger, string message, params object[] args)
        {
            logger.LogWarning(message, args);
        }

        public static void Trace(this ILogger logger, string message, params object[] args)
        {
            logger.LogTrace(message, args);
        }

        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }

        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.LogDebug(message, args);
        }

        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.LogError(message, args);
        }

        public static void Error(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.LogError(default(EventId), exception, message, args);
        }

        public static void Error(this ILogger logger, Exception exception)
        {
            logger.LogError(default(EventId), exception, "");
        }
    }
}