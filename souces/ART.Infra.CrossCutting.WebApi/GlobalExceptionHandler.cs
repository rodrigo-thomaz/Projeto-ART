namespace ART.Infra.CrossCutting.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Results;

    public class GlobalExceptionHandler : ExceptionHandler
    {
        #region Constructors

        public GlobalExceptionHandler()
        {
            #if (!DEBUG)
            //_rollbarClient = new RollbarClient();
            #endif
        }

        #endregion Constructors

        #region Methods

        public override void Handle(ExceptionHandlerContext context)
        {
            HttpResponseMessage responseMessage;

            if (context.Exception is UnauthorizedAccessException)
            {
                responseMessage = context.Request.CreateErrorResponse(HttpStatusCode.Forbidden, context.Exception);
            }
            //else if (context.Exception is RecordNotFoundException)
            //{
            //    responseMessage = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, context.Exception);
            //}
            else if (context.Exception.GetBaseException() is ArgumentException)
            {
                responseMessage = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception);
            }
            else
            {
                responseMessage = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, context.Exception);
            }

            context.Result = new ResponseMessageResult(responseMessage);
        }

        #endregion Methods

        #if (!DEBUG)

        // _rollbarClient.SendException(context.Exception);

        #endif
    }
}