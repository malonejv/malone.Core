using malone.Core.Business.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions.Handler
{
    public interface IExceptionHandler<TErrorCoder>
        where TErrorCoder : Enum
    {
        void HandleException<TException>(TErrorCoder errorCode, params object[] args) where TException : BaseException<TErrorCoder>;

        void HandleException<TException>(Exception ex, TErrorCoder errorCode, params object[] args) where TException : BaseException<TErrorCoder>;

        void HandleException<TValidation>(ValidationResultList validationResult) where TValidation : BusinessValidationException;
    }
}
