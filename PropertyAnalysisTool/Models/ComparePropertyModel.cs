using System.Collections.Generic;
using System.Linq;

namespace PropertyAnalysisTool.Models
{
    public class ComparePropertyModel
    {
        public List<PropertyModel> Properties { get; set; }

        public ComparePropertyModel()
        {
            Properties = new List<PropertyModel>();
        }

        //this property will be used to set the size of divs used to hold each property being compared
        //
        public int ContainerSize
        {
            get
            {
                if (!Properties.Any())
                {
                    return 12;
                }
                return 12 / Properties.Count;
            }
        }
    }
}