namespace ART.MQ.Common.Contracts
{
    public interface IDSFamilyTempSensorSetResolutionContract
    {
        string DeviceAddress { get; }
        int Value { get; }
    }
}
