using System;
using System.Reflection;

namespace malone.Core.Sample.AdoNet.SqlServer.Api.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}