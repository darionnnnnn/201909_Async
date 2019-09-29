using System;
using System.Reflection;

namespace D002.ASP_NET應用程式的造成死結範例.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}