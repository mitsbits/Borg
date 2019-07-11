using Borg.Infrastructure.Core;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Borg
{
    public static partial class IHtmlHelperExtensions
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            htmlHelper = Preconditions.NotNull(htmlHelper, nameof(htmlHelper));
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string ActionName(this HtmlHelper htmlHelper)
        {
            htmlHelper = Preconditions.NotNull(htmlHelper, nameof(htmlHelper));
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        //
        // Summary:
        //     Returns HTML markup for the expression, using an editor template. The template
        //     is found using the expression's Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.
        //
        // Parameters:
        //   htmlHelper:
        //     The Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper`1 instance this method extends.
        //
        //   expression:
        //     An expression to be evaluated against the current model.
        //
        // Type parameters:
        //   TModel:
        //     The type of the model.
        //
        //   TResult:
        //     The type of the expression result.
        //
        // Returns:
        //     A new Microsoft.AspNetCore.Html.IHtmlContent containing the <input> element(s).
        //
        // Remarks:
        //     For example the default System.Object editor template includes <label> and <input>
        //     elements for each property in the expression result.
        //     Custom templates are found under a EditorTemplates folder. The folder name is
        //     case-sensitive on case-sensitive file systems.
        public static IHtmlContent Editorial(this IHtmlHelper<object> htmlHelper, PropertyInfo property, object model)
        {
            var options = ScriptOptions.Default.AddReferences(model.GetType().Assembly);
            var keyExpr = $"x => x.{property.Name}";
            var keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<object, object>>>(keyExpr, options));
            return htmlHelper.EditorFor(keyExpression);
        }

        //
        // Summary:
        //     Returns HTML markup for the expression, using an editor template, specified HTML
        //     field name, and additional view data. The template is found using the templateName
        //     or the expression's Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.
        //
        // Parameters:
        //   expression:
        //     An expression to be evaluated against the current model.
        //
        //   templateName:
        //     The name of the template that is used to create the HTML markup.
        //
        //   htmlFieldName:
        //     A System.String used to disambiguate the names of HTML elements that are created
        //     for properties that have the same name.
        //
        //   additionalViewData:
        //     An anonymous System.Object or System.Collections.Generic.IDictionary`2 that can
        //     contain additional view data that will be merged into the Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary`1
        //     instance created for the template.
        //
        // Type parameters:
        //   TResult:
        //     The type of the expression result.
        //
        // Returns:
        //     A new Microsoft.AspNetCore.Html.IHtmlContent containing the <input> element(s).
        //
        // Remarks:
        //     For example the default System.Object editor template includes <label> and <input>
        //     elements for each property in the expression result.
        //     Custom templates are found under a EditorTemplates folder. The folder name is
        //     case-sensitive on case-sensitive file systems.
        //public static IHtmlContent EditorFor<TResult>(this IHtmlHelper helper, PropertyInfo properyInfo, object model,  string templateName, string htmlFieldName, object additionalViewData)
        //{
        //    helper.ed
        //    var modelType = model.GetType();
        //    var options = ScriptOptions.Default.AddReferences(modelType.Assembly);
        //    var keyExpr = $"x => x.{properyInfo.Name}";
        //    var keyExpression = AsyncHelpers.RunSync(() => CSharpScript.EvaluateAsync<Expression<Func<object, TResult>>>(keyExpr, options));
        //    return helper.EditorFor(keyExpression, templateName, htmlFieldName, additionalViewData);
        //}
    }
}