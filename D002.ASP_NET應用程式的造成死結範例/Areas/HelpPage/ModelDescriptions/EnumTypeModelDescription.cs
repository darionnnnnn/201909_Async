using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D002.ASP_NET應用程式的造成死結範例.Areas.HelpPage.ModelDescriptions
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