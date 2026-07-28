using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application_01.Common
{
    public class Error(string code,string description, ErrorType type = ErrorType.Failure)
    {
        public string Code { get; } = code;
        public string Description { get; } = description;
        public ErrorType Type { get; } = type;
        public static Error Failure( string code = "General.Failure",string description = " A General Failure Has Occurred")
            => new Error(code, description, ErrorType.Failure);

        public static Error Validation(string code = "General.Validation",string description = " A Validation Error Has Occurred")
            => new Error(code, description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound",string description = " The requested resource has not found")
            => new Error(code, description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict",string description = " A conflict occured with the current state")
            => new Error(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code = "General.Unauthorized",string description = " Access is denied due to lack of authorization")
            => new Error(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code = "General.Forbidden",string description = " The operation is forbidden")
            => new Error(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentials(string code = "General.InvalidCredentails",string description = " The provided credintals are not valid")
            => new Error(code, description, ErrorType.InvalidCredtials);
    }
}
