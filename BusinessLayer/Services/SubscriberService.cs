using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using BusinessLayer.Interfaces.Services;
using BusinessLayer.Validation;

using Models;

using Project1.CustomExceptions;
using Project1.Models;
using Project1.MongoDB.Repository.Interfaces;

namespace BusinessLayer.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IValidator<MessageModelRequest> _messageValidator;
        public SubscriberService ( ISubscriberRepository subscriberRepository )
        {
            _subscriberRepository = subscriberRepository;
        }

        public async Task ProccessMessage ( MessageModelRequest message )
        {
            try
            {
                bool isValidMessage = _messageValidator.IsValid ( message );
                if ( isValidMessage )
                {
                    //CheckIfOptIn or Out 
                    //Return service ID


                    //If OptIn 
                    //{

                    //the new GUID should be the returned service id 
                    Subscriber subscriber = await GetUserByMobileNumber ( message.MobileNumber );
                    if ( IsAlreadySubscribed ( Guid.NewGuid (), subscriber.Services ) )
                    {
                        //should send to the SMS GetWay
                    }
                    else
                    {
                        subscriber.Services.Add ( new ServiceSubscribtion () { IsActive = true, IsPending = true, ServiceId = Guid.NewGuid () } );
                        await Subscribe ( subscriber );
                        //Send to the charging system
                        //If the charging is not succeeded
                        //should send to the SMS GetWay failure message

                    }
                    //}
                    //If OptOut
                    //{

                    //}
                }
                else
                {
                    throw new ValidationException ();
                }
            }
            catch ( DatabaseException dbException )
            {
                string exceptionMessage = "Mongo database exception ";
                DatabaseException modifiedException = new DatabaseException ( exceptionMessage, dbException.InnerException );
                throw modifiedException;
            }
            catch ( ValidationException validationException )
            {
                throw validationException;
            }
            catch ( Exception exception )
            {
                throw exception;
            }
        }
        private async Task Subscribe ( Subscriber newSubscribtion )
        {
            await _subscriberRepository.UpdateAsync ( newSubscribtion );
        }

        private async Task UnSubscribe ( Subscriber subscribtion )
        {
            await _subscriberRepository.UpdateAsync ( subscribtion );
        }

        public void UpdatePendingStatus ( Subscriber pendingSubscribtion )
        {
            throw new NotImplementedException ();
        }


        private async Task<Subscriber> GetUserByMobileNumber ( string mobileNumber )
        {
            return await _subscriberRepository.GetDataByExpressionAsync ( s => s.MobileNumber == mobileNumber );
        }

        private bool IsAlreadySubscribed ( Guid serviceId, List<ServiceSubscribtion> subscribedServices )
        {
            return subscribedServices.Where ( s => s.ServiceId == serviceId ).Any ();

        }
    }
}
