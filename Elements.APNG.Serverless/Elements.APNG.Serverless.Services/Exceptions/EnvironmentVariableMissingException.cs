using System;

namespace Elements.APNG.Serverless.Services.Exceptions
{
    public class EnvironmentVariableMissingException: Exception
    {
        public EnvironmentVariableMissingException(string message):base(message)
        {

        }
    }
}
