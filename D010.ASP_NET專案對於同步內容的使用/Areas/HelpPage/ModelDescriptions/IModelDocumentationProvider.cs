using System;
using System.Reflection;

namespace D010.ASP_NET專案對於同步內容的使用.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}