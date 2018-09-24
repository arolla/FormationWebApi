using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Bookings.Hosting.Logging
{
    class FileExceptionLogger : IExceptionLogger
    {
        private readonly string fileName;

        public FileExceptionLogger(string fileName)
        {
            this.fileName = fileName;
        }

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
