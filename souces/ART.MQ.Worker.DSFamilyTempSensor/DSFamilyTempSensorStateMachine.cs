using System.Linq;
using Automatonymous;
using MassTransit.Courier.Contracts;
using ART.MQ.Common.Contracts;

namespace ART.MQ.Worker.DSFamilyTempSensor
{
    public class DSFamilyTempSensorStateMachine : MassTransitStateMachine<DSFamilyTempSensorState>
    {
        public DSFamilyTempSensorStateMachine()
        {
            InstanceState(x => x.State, Executing, Completed, Faulted, CompensationFailed);

            Event(() => SetResolution, x => x.CorrelateById(context => context.Message.TrackingNumber));
            Event(() => SetResolutionCompleted, x => x.CorrelateById(context => context.Message.TrackingNumber));
            Event(() => SetResolutionFaulted, x => x.CorrelateById(context => context.Message.TrackingNumber));
            Event(() => SetResolutionCompensationFailed, x => x.CorrelateById(context => context.Message.TrackingNumber));

            // Events can arrive out of order, so we want to make sure that all observed events can created
            // the state machine instance
            Initially(
                When(SetResolution)
                    .Then(HandleRoutingSetResolution)
                    .TransitionTo(Executing),                
                When(SetResolutionCompleted)
                    .Then(HandleRoutingSetResolutionCompleted)
                    .TransitionTo(Completed),
                When(SetResolutionFaulted)
                    .Then(HandleRoutingSetResolutionFaulted)
                    .TransitionTo(Faulted),
                When(SetResolutionCompensationFailed)
                    .TransitionTo(CompensationFailed));

            // during any state, we can handle any of the events, to transition or capture previously
            // missed data.
            DuringAny(
                When(SetResolution)
                    .Then(context =>
                    {
                        context.Instance.CreateTime = context.Data.Timestamp;
                    }),
                When(SetResolutionCompleted)
                    .Then(HandleRoutingSetResolutionCompleted)
                    .TransitionTo(Completed),
                When(SetResolutionFaulted)
                    .Then(HandleRoutingSetResolutionFaulted)
                    .TransitionTo(Faulted),
                When(SetResolutionCompensationFailed)
                    .TransitionTo(CompensationFailed));
        }


        public State Executing { get; private set; }
        public State Completed { get; private set; }
        public State Faulted { get; private set; }
        public State CompensationFailed { get; private set; }

        public Event<DSFamilyTempSensorSetResolutionContract> SetResolution { get; private set; }
        public Event<RoutingSlipCompleted> SetResolutionCompleted { get; private set; }
        public Event<RoutingSlipFaulted> SetResolutionFaulted { get; private set; }
        public Event<RoutingSlipCompensationFailed> SetResolutionCompensationFailed { get; private set; }

        static void HandleRoutingSetResolution(BehaviorContext<DSFamilyTempSensorState, DSFamilyTempSensorSetResolutionContract> context)
        {
            context.Instance.CreateTime = context.Data.Timestamp;
        }        

        static void HandleRoutingSetResolutionCompleted(BehaviorContext<DSFamilyTempSensorState, RoutingSlipCompleted> context)
        {
            context.Instance.EndTime = context.Data.Timestamp;
            context.Instance.Duration = context.Data.Duration;
        }

        static void HandleRoutingSetResolutionFaulted(BehaviorContext<DSFamilyTempSensorState, RoutingSlipFaulted> context)
        {
            context.Instance.EndTime = context.Data.Timestamp;
            context.Instance.Duration = context.Data.Duration;

            string faultSummary = string.Join(", ",
                context.Data.ActivityExceptions.Select(x => string.Format("{0}: {1}", x.Name, x.ExceptionInfo.Message)));

            context.Instance.FaultSummary = faultSummary;
        }
    }
}
