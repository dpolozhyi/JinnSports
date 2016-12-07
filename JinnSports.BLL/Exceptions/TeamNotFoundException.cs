using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.BLL.Exceptions
{
    [Serializable]
    public class TeamNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public TeamNotFoundException()
        {
        }

        public TeamNotFoundException(string message) : base(message)
        {
        }

        public TeamNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TeamNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
