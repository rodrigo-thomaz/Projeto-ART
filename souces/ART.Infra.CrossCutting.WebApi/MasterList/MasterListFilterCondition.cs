namespace ART.Infra.CrossCutting.WebApi.MasterList
{
    public enum MasterListFilterCondition
    {
        StartsWith = 2,
        EndsWith = 4,
        Exact = 8,
        Contains = 16,
        GreaterThan = 32,
        GreaterThanOrEqual = 64,
        LessThan = 128,
        LessThanOrEqual = 256,
        NotEqual = 512,
    }
}
