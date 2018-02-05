namespace ART.Domotica.Constant
{
    public static class ESPDeviceConstants
    {
        #region Fields

        public static readonly string CheckForUpdatesRPCQueueName = "ESPDevice.CheckForUpdatesRPC";
        public static readonly string GetAllByApplicationIdQueueName = "ESPDevice.GetAllByApplicationId";
        public static readonly string GetAllByApplicationIdViewCompletedQueueName = "ESPDevice.GetAllByApplicationIdViewCompleted";
        public static readonly string GetAllQueueName = "ESPDeviceAdmin.GetAll";
        public static readonly string GetAllViewCompletedQueueName = "ESPDeviceAdmin.GetAllViewCompleted";
        public static readonly string GetByPinQueueName = "ESPDevice.GetByPin";
        public static readonly string GetByPinViewCompletedQueueName = "ESPDevice.GetByPinViewCompleted";
        public static readonly string GetConfigurationsRPCQueueName = "ESPDevice.GetConfigurationsRPC";
        public static readonly string SetLabelIoTQueueName = "ESPDevice.SetLabelIoT";
        public static readonly string SetLabelQueueName = "ESPDevice.SetLabel";
        public static readonly string SetLabelViewCompletedQueueName = "ESPDevice.SetLabelViewCompleted";
        public static readonly string UpdatePinIoTQueueName = "ESPDevice.UpdatePinIoT";

        #endregion Fields
    }
}