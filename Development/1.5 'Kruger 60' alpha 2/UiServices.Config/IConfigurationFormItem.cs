using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public interface IConfigurationFormItem
    {
        UserControl UserInterfaceItem
        {
            get;
        } // UserInterfaceItem

        string ItemName
        {
            get;
        } // ItemName

        Image ItemImage
        {
            get;
        } // ItemImage
        
        void CommitChanges();
        void DiscardChanges();
    } // interface IConfigurationFormItem
} // namespace
