using malone.Core.BL.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Exceptions.Handler
{
    public interface IExceptionHandler<TCode>
        where TCode : Enum
    {
        void HandleException<TException>(TCode errorCode, params object[] args) where TException : BaseException<TCode>;

        void HandleException<TException>(Exception ex, TCode errorCode, params object[] args) where TException : BaseException<TCode>;
        
        void HandleException<TValidation>(ValidationResultList validationResult) where TValidation : BusinessValidationException<TCode>;
    }

}
