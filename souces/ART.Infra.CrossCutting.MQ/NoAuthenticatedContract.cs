namespace ART.Infra.CrossCutting.MQ
{
    public class NoAuthenticatedContract<TContract>
    {
        public string SouceMQSession { get; set; }

        public TContract Contract { get; set; }
    }
}
