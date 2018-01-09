namespace ART.Domotica.IoTContract
{
    public class SetValueRequestIoTContract<T>
    {
        #region Constructors

        public SetValueRequestIoTContract()
        {
        }

        public SetValueRequestIoTContract(T value)
        {
            Value = value;
        }

        #endregion Constructors

        #region Properties

        public T Value
        {
            get; set;
        }

        #endregion Properties
    }
}