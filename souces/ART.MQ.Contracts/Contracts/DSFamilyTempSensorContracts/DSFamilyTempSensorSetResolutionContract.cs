namespace ART.MQ.Common.Contracts.DSFamilyTempSensorContracts
{
    public interface DSFamilyTempSensorSetResolutionContract
    {
        string DeviceAddress { get; }
        int Value { get; }
    }
}
