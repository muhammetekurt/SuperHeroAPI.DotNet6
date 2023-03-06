using System.Net;

namespace SuperHeroAPI.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
