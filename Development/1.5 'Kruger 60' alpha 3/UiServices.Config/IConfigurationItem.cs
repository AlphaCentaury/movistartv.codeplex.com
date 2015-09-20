using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration
{
    // interface for marking configuration items
    public interface IConfigurationItem
    {
        bool CanValidate
        {
            get;
        } // CanValidate

        string Validate(string ownerTag);
    } // interface IConfigurationItem
} // namespace
