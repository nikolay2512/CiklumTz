using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using InternetShopParser.Model.CustomExceptions;

namespace InternetShopParser.Model
{
    public class AOResult<T>
    {
        private bool _isResultSet;
        private readonly DateTime _creationUtcTime;

        public AOResult([CallerMemberName]string callerName = null, [CallerFilePath]string callerFile = null, [CallerLineNumber]int callerLineNumber = 0)
        {
            _creationUtcTime = DateTime.UtcNow;
            CallerName = callerName;
            CallerFile = callerFile;
            CallerLineNumber = callerLineNumber;
        }

        #region  Public properties 

        /// <summary>
        /// Gets or sets the operation time.
        /// </summary>
        /// <value>The operation time.</value>
        public TimeSpan OperationTime { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:HW_DishOrder.Model.AOResult`1"/> is success.
        /// </summary>
        /// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public Code Code { get; private set; }

        /// <summary>
        /// Gets the name of the caller.
        /// </summary>
        /// <value>The name of the caller.</value>
        public string CallerName { get; private set; }

        /// <summary>
        /// Gets the caller file.
        /// </summary>
        /// <value>The caller file.</value>
        public string CallerFile { get; private set; }

        /// <summary>
        /// Gets the caller line number.
        /// </summary>
        /// <value>The caller line number.</value>
        public int CallerLineNumber { get; private set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<Error> Errors { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; private set; }

        #endregion

        #region  Public methods 

        /// <summary>
        /// Sets the succes.
        /// </summary>
        public void SetSuccess()
        {
            CheckResult();

            SetResult(true, Code.Ok, default(T), null, null, null);
        }

        /// <summary>
        /// Sets the succes.
        /// </summary>
        /// <param name="result">Result.</param>
        public void SetSuccess(T result)
        {
            CheckResult();

            SetResult(true, Code.Ok, result, null, null, null);
        }

        /// <summary>
        /// Sets the error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="code">Code.</param>
        /// <param name="ex">Ex.</param>
        public void SetError(string message, Code code = Code.Error, Exception ex = null)
        {
            CheckResult();

            SetResult(false, code, default(T), message, null, ex);
        }

        /// <summary>
        /// Sets the error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="erorrs">Erorrs.</param>
        /// <param name="code">Code.</param>
        /// <param name="ex">Ex.</param>
        public void SetError(string message, IEnumerable<Error> erorrs, Code code = Code.Error, Exception ex = null)
        {
            CheckResult();

            SetResult(false, code, default(T), message, erorrs, ex);
        }

        private void SetResult(bool isSuccess, Code code, T result, string message, IEnumerable<Error> erorrs, Exception ex)
        {
            var finishTime = DateTime.UtcNow;
            OperationTime = finishTime - _creationUtcTime;
            IsSuccess = isSuccess;
            Exception = ex;
            Errors = erorrs;
            Result = result;
            Code = code;
            Message = message;
            ResultSet(true);
#if DEBUG
            Debug.WriteLine($@"
****** AO Result ******
Method = {CallerName}
Time = {OperationTime}
");
#endif
        }

        #endregion

        #region  Private methods 

        public void CheckResult()
        {
            if (_isResultSet)
            {
                throw new ResultAlreadySetAOResultException();
            }
            else
            {
                //just exits
            }
        }

        private void ResultSet(bool isResultSet)
        {
            _isResultSet = isResultSet;
        }
        #endregion

    }
}
