﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace Borg.System.Backoffice.Core.GenericEntity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class BackOfficeEntityControllerNameAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.GetGenericTypeDefinition().Equals(typeof(BackOfficeEntityController<,>)))
            {
                var entityType = controller.ControllerType.GenericTypeArguments[0];
                controller.ControllerName = entityType.Name;
            }
        }
    }
}