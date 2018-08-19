using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InternetShopParser.Model.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using static InternetShopParser.Model.Attributes.ValidateObjectAttribute;

namespace InternetShopParser.Model.Database.Services
{
    public abstract class BaseService
    {
        protected const string ModelIsNotValidErrorMessage = "Model Is Not Valid.";
        private const string EntityNotFoundFormat = "'{0}' Not Found.";
        private const string EntityAlreadyExistsFormat = "'{0}' Already Exists.";
        private const string SomeEntitiesNotExistFormat = "Next entities: '{0}' Are Not Exists.";

        protected AOResult<T> BaseInvoke<T>(Action<AOResult<T>> action, object request = null, [CallerMemberName]string callerName = null, [CallerFilePath]string callerFile = null, [CallerLineNumber]int callerLineNumber = 0)
        {
            AOResult<T> aoResult = new AOResult<T>(callerName, callerFile, callerLineNumber);
            try
            {
                List<ValidationResult> checkModelAOResult = CheckRequest(request);
                if (checkModelAOResult.Any())
                {
                    aoResult.SetError(ModelIsNotValidErrorMessage, checkModelAOResult.Select(x => new Error
                    {
                        Key = x.MemberNames.FirstOrDefault(),
                        Message = x.ErrorMessage
                    }));
                }
                else
                {
                    action(aoResult);
                }
            }
            catch (DbUpdateException dbException)
            {
                aoResult.SetError(dbException.Message, ex: dbException);
            }
            catch (Exception ex)
            {
                aoResult.SetError(ex.Message, ex: ex);
            }
            return aoResult;
        }

        protected async Task<AOResult<T>> BaseInvokeAsync<T>(Func<AOResult<T>, Task> func, object request = null, [CallerMemberName]string callerName = null, [CallerFilePath]string callerFile = null, [CallerLineNumber]int callerLineNumber = 0)
        {
            AOResult<T> aoResult = new AOResult<T>(callerName, callerFile, callerLineNumber);
            try
            {
                List<ValidationResult> checkModelAOResult = CheckRequest(request);
                if (checkModelAOResult.Any())
                {
                    aoResult.SetError(ModelIsNotValidErrorMessage, checkModelAOResult.Select(x => new Error
                    {
                        Key = x.MemberNames.FirstOrDefault(),
                        Message = x.ErrorMessage
                    }));
                }
                else
                {
                    await func(aoResult);
                }
            }
            catch (DbUpdateException dbException)
            {
                aoResult.SetError(dbException.Message, ex: dbException);
            }
            catch (Exception ex)
            {
                aoResult.SetError(ex.Message, ex: ex);
            }
            return aoResult;
        }

        protected string EntityAlreadyExists(object entityName)
        {
            if (entityName == null)
            {
                throw new ArgumentNullException(nameof(entityName));
            }
            else
            {
                return string.Format(EntityAlreadyExistsFormat, entityName);
            }
        }

        protected string EntityNotExists(object entityName)
        {
            if (entityName == null)
            {
                throw new ArgumentNullException(nameof(entityName));
            }
            else
            {
                return string.Format(EntityNotFoundFormat, entityName);
            }
        }

        protected string EntitiesNotExist<T>(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            else
            {
                return string.Format(SomeEntitiesNotExistFormat, entities.Select(x => x?.ToString()).Where(x => x != null).JoinStr());
            }
        }

        protected static string RemoveServicePostfix(string serviceName)
        => serviceName.EndsWith("Service", StringComparison.Ordinal) ? serviceName.Replace("Service", string.Empty) : serviceName;


        #region private helpers

        private List<ValidationResult> CheckRequest(object request)
        {
            request = request ?? new { };

            var validationResultList = new List<ValidationResult>();
            Validator.TryValidateObject(request, new ValidationContext(request), validationResultList, true);

            return SelectMany(validationResultList);
        }

        private List<ValidationResult> SelectMany(IEnumerable<ValidationResult> validationResults)
        {
            var validationResultList = new List<ValidationResult>();

            validationResultList.AddRange(validationResults.Select(x =>
            {
                if (x is CompositeValidationResult cvr)
                {
                    validationResultList.AddRange(SelectMany(cvr.Results));
                }
                return x;
            }));
            return validationResultList;
        }

        #endregion
    }
}
