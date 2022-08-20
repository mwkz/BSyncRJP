using BS.Common.Core.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Infrastructure.Services
{
    public abstract class BusinessService<Request, Response> : IBusinessService<Request, Response>
    {
        public BusinessService(IEnumerable<IValidator<Request>> validators, ILogger logger)
        {
            Validators = validators;
            Logger = logger;
        }

        protected IEnumerable<IValidator<Request>> Validators { get; }
        protected ILogger Logger { get; }

        public async Task<BusinessServiceResponse<Response>> Execute(BusinessServiceRequest<Request> request, CancellationToken token = default)
        {
            var succeeded = true;
            List<ValidationData> validation = new List<ValidationData>();

            foreach(var validator in Validators)
            {

                var validationResult  = await validator.ValidateAsync(request.Request, token);
                succeeded = succeeded && validationResult.IsValid;

                foreach (var entry in validationResult.Errors)
                {
                    validation.Add(new ValidationData(entry.ErrorMessage, entry.PropertyName, BusinessService<Request, Response>.ToValidationSeverity(entry.Severity)));
                    LogValidationFailure(entry);
                }
            }

            var response =  await ExecuteRequest(request, token).ConfigureAwait(false);
            return new BusinessServiceResponse<Response>(response, succeeded, validation);
        }

        /// <summary>
        /// Override to execute login associated with business service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<Response> ExecuteRequest(BusinessServiceRequest<Request> request, CancellationToken token = default);    
        

        private void LogValidationFailure(ValidationFailure failure)
        {
            switch (failure.Severity)
            {
                case Severity.Warning:
                    Logger.LogWarning($"Validation issued a warning {failure.ErrorMessage} for type {typeof(Request).Name} and property {failure.PropertyName}. Error code: {failure.ErrorCode}");
                    break;
                case Severity.Info:
                    Logger.LogInformation($"Validation information {failure.ErrorMessage} for type {typeof(Request).Name} and property {failure.PropertyName}. Error code: {failure.ErrorCode}");
                    break;
                case Severity.Error:
                    Logger.LogError($"Validation issued an error {failure.ErrorMessage} for type {typeof(Request).Name} and property {failure.PropertyName}. Error code: {failure.ErrorCode}");
                    break;

            }
        }


        private static ValidationSeverity ToValidationSeverity(Severity severity) =>
            severity switch
            {
                Severity.Error => ValidationSeverity.Error,
                Severity.Warning => ValidationSeverity.Warning,
                _ => ValidationSeverity.Information,
            };

    }
}
