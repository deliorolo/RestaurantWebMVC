// Class that provides the possibility to choose how we want to access to the database

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
