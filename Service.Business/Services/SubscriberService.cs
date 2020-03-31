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
        public SubscriberService ( ISubscriberRepository subscriberRepository , IValidator<MessageModelRequest> messageValidator )
        {
            _subscriberRepository = subscriberRepository;
            _messageValidator = messageValidator;
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
                    //the new GUID should be the returned service id 

                    Guid serviceId = Guid.NewGuid ();
                   

                    Subscriber subscriber = await GetUserByMobileNumber ( message.MobileNumber );
                    if ( subscriber != null && serviceId != null )
                    {
                        //If OptIn 
                        //{
                        if ( IsAlreadySubscribed ( serviceId, subscriber.Services ) )
                        {
                            //should send to the SMS GetWay
                        }
                        else
                        {
                            if ( subscriber.Services == null )
                            {
                                subscriber.Services = new List<ServiceSubscribtion> ();
                            }

                            subscriber.Services.Add ( new ServiceSubscribtion () { IsActive = true, IsPending = true, ServiceId = serviceId } );
                            await Subscribe ( subscriber );
                            //get the service cost 
                            //Send to the charging system
                            //If the charging is not succeeded
                            //should send to the SMS GetWay failure message
                            //If Charging  

                        }
                        //}
                        //If OptOut
                        //{
                        if ( !IsAlreadySubscribed ( serviceId, subscriber.Services ) )
                        {
                            //should send to the SMS GetWay
                        }
                        else
                        {

                            int serviceIndex = subscriber.Services.IndexOf ( subscriber.Services.Where ( s => s.ServiceId == serviceId ).First());
                            subscriber.Services.ElementAt(serviceIndex).IsActive = false;
                            subscriber.Services.ElementAt(serviceIndex).IsPending = false;
                            subscriber.Services.ElementAt(serviceIndex).UnsubscribtionDate = DateTime.UtcNow;

                            await UnSubscribe ( subscriber );

                        }
                        //}
                    }
                    else
                    {
                        throw new ArgumentNullException ( "Invalid user or service" );
                    }

                    
                  
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
            catch (ArgumentNullException argNullExcepton)
            {
                throw argNullExcepton ;
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
            if (subscribedServices == null)
            {
                return false;
            }
            return subscribedServices.Where ( s => s.ServiceId == serviceId ).Any ();

        }
    }
}
