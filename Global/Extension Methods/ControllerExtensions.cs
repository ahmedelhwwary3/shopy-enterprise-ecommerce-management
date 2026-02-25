using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Global
{

    public enum enTempMessage
    {
        UpdatedSuccessfully = 0,
        DeletedSuccessfully = 1,
        CreateSucceeded = 2
    }
    public static class ControllerExtensions
    {
        public static string GetDomainName(this Controller controller)
        {
            string name = controller.GetType().Name;
            if (name.EndsWith("Controller"))
                name = name.Replace("Controller", "");

            return name;
        }
        public static void SendTempMessage(this Controller controller,enTempMessage type)
        {
            string domain = controller.GetDomainName();
            switch(type)
            {
                //SuccessMessages
                case enTempMessage.UpdatedSuccessfully:
                    {
                        controller.TempData[GlobalMessages.SuccessMessage] 
                            = GlobalMessages.UpdatedSuccessfully(domain);
                        break;
                    }
                case enTempMessage.CreateSucceeded:
                    {
                        controller.TempData[GlobalMessages.SuccessMessage]
                            = GlobalMessages.CreateSucceeded(domain);
                        break;
                    }
                case enTempMessage.DeletedSuccessfully:
                    {
                        controller.TempData[GlobalMessages.SuccessMessage]
                            = GlobalMessages.DeletedSuccessfully(domain);
                        break;
                    }
            }
        }
    }
}
