namespace Domain.Exceptions;

public class NoConnectionStringProvidedException() : Exception($"Application can't work with invalid ConnectionString!");