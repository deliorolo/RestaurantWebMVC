namespace CodeLibrary.DataAccess
{
    public static class TypeOfAccess
    {
        public static Connection Access { get; set; }
    }

    public enum Connection
    {
        Direct,
        WebApi
    }
}
