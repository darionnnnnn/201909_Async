using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D010.ASP_NET專案對於同步內容的使用.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}