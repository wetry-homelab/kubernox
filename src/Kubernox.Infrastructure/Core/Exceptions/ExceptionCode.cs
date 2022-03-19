using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Core.Exceptions
{
    public class ExceptionCode
    {
        public static int AuthenticateFailed = 1001;

        public static int DatabaseReadAllFailed = 8001;
        public static int DatabaseReadOneFailed = 8002;
        public static int DatabaseUpdateFailed = 8003;
        public static int DatabaseInsertFailed = 8004;
    }
}
