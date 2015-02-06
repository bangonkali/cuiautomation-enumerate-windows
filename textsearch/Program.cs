using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIAutomationBlockingCoreLib;

namespace textsearch
{
    class Automation // http://goo.gl/pMXTLv
    {
        CUIAutomation _automation;

        public void Start()
        {
            _automation = new CUIAutomation();
            EnumarateChildren(_automation.GetRootElement());
        }

        private void EnumarateChildren(IUIAutomationElement element)
        {
            Console.WriteLine("{0}", element.CurrentName.Trim());

            
            IUIAutomationCacheRequest cacheRequest = _automation.CreateCacheRequest();
            cacheRequest.AddProperty(System.Windows.Automation.AutomationElement.NameProperty.Id);
            cacheRequest.AddProperty(System.Windows.Automation.AutomationElement.ControlTypeProperty.Id);
            cacheRequest.TreeScope = TreeScope.TreeScope_Element | TreeScope.TreeScope_Children | TreeScope.TreeScope_Subtree;

            IUIAutomationCondition cond;
            cond = _automation.CreatePropertyConditionEx(
                System.Windows.Automation.AutomationElement.ControlTypeProperty.Id,
                System.Windows.Automation.ControlType.Window.Id, 
                PropertyConditionFlags.PropertyConditionFlags_IgnoreCase);

            IUIAutomationElementArray elementList = element.FindAllBuildCache(TreeScope.TreeScope_Children, cond, cacheRequest);

            if (elementList == null) return;
            
            for (int i = 0; i < elementList.Length; i++)
            {
                EnumarateChildren(elementList.GetElement(i));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Automation automation = new Automation();
            automation.Start();
        }
    }
}
