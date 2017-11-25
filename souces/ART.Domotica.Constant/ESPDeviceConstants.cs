namespace ART.Domotica.Constant
{
    public static class ESPDeviceConstants
    {
        #region Fields

        public static readonly string DeleteFromApplicationIoTQueueName = "ESPDevice.DeleteFromApplicationIoT";
        public static readonly string DeleteFromApplicationQueueName = "ESPDevice.DeleteFromApplication";
        public static readonly string DeleteFromApplicationViewCompletedQueueName = "ESPDevice.DeleteFromApplicationViewCompleted";
        public static readonly string GetAllQueueName = "ESPDeviceAdmin.GetAll";
        public static readonly string GetAllViewCompletedQueueName = "ESPDeviceAdmin.GetAllViewCompleted";
        public static readonly string GetByPinQueueName = "ESPDevice.GetByPin";
        public static readonly string GetByPinViewCompletedQueueName = "ESPDevice.GetByPinViewCompleted";
        public static readonly string GetConfigurationsRPCQueueName = "ESPDevice.GetConfigurationsRPC";
        public static readonly string GetListInApplicationQueueName = "ESPDevice.GetListInApplication";
        public static readonly string GetListInApplicationViewCompletedQueueName = "ESPDevice.GetListInApplicationViewCompleted";
        public static readonly string InsertInApplicationIoTQueueName = "ESPDevice.InsertInApplicationIoT";
        public static readonly string InsertInApplicationQueueName = "ESPDevice.InsertInApplication";
        public static readonly string InsertInApplicationViewCompletedQueueName = "ESPDevice.InsertInApplicationViewCompleted";
        public static readonly string SetUtcTimeOffsetInSecondIoTQueueName = "ESPDevice.SetUtcTimeOffsetInSecondIoT";
        public static readonly string SetTimeZoneQueueName = "ESPDevice.SetTimeZone";
        public static readonly string SetTimeZoneViewCompletedQueueName = "ESPDevice.SetTimeZoneViewCompleted";
        public static readonly string SetUpdateIntervalInMilliSecondIoTQueueName = "ESPDevice.SetUpdateIntervalInMilliSecondIoT";
        public static readonly string SetUpdateIntervalInMilliSecondQueueName = "ESPDevice.SetUpdateIntervalInMilliSecond";
        public static readonly string SetUpdateIntervalInMilliSecondViewCompletedQueueName = "ESPDevice.SetUpdateIntervalInMilliSecondViewCompleted";
        public static readonly string UpdatePinIoTQueueName = "ESPDevice.UpdatePinIoT";

        #endregion Fields
    }
}