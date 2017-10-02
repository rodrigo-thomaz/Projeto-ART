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
            Event(() => SlipCompleted, x => x.CorrelateById(context => context.Message.TrackingNumber));
            Event(() => SlipFaulted, x => x.CorrelateById(context => context.Message.TrackingNumber));
            Event(() => SlipCompensationFailed, x => x.CorrelateById(context => context.Message.TrackingNumber));

            // Events can arrive out of order, so we want to make sure that all observed events can created
            // the state machine instance
            Initially(
                When(SetResolution)
                    .Then(HandleRoutingSetResolution)
                    .TransitionTo(Executing),
                When(SlipCompleted)
                    .Then(HandleRoutingSlipCompleted)
                    .TransitionTo(Completed),
                When(SlipFaulted)
                    .Then(HandleRoutingSlipFaulted)
                    .TransitionTo(Faulted),
                When(SlipCompensationFailed)
                    .TransitionTo(CompensationFailed));

            // during any state, we can handle any of the events, to transition or capture previously
            // missed data.
            DuringAny(
                When(SetResolution)
                    .Then(context => context.Instance.CreateTime = context.Data.Timestamp),
                When(SlipCompleted)
                    .Then(HandleRoutingSlipCompleted)
                    .TransitionTo(Completed),
                When(SlipFaulted)
                    .Then(HandleRoutingSlipFaulted)
                    .TransitionTo(Faulted),
                When(SlipCompensationFailed)
                    .TransitionTo(CompensationFailed));
        }


        public State Executing { get; private set; }
        public State Completed { get; private set; }
        public State Faulted { get; private set; }
        public State CompensationFailed { get; private set; }

        public Event<DSFamilyTempSensorSetResolutionContract> SetResolution { get; private set; }
        public Event<RoutingSlipCompleted> SlipCompleted { get; private set; }
        public Event<RoutingSlipFaulted> SlipFaulted { get; private set; }
        public Event<RoutingSlipCompensationFailed> SlipCompensationFailed { get; private set; }

        static void HandleRoutingSetResolution(BehaviorContext<DSFamilyTempSensorState, DSFamilyTempSensorSetResolutionContract> context)
        {
            context.Instance.CreateTime = context.Data.Timestamp;
        }        

        static void HandleRoutingSlipCompleted(BehaviorContext<DSFamilyTempSensorState, RoutingSlipCompleted> context)
        {
            context.Instance.EndTime = context.Data.Timestamp;
            context.Instance.Duration = context.Data.Duration;
        }

        static void HandleRoutingSlipFaulted(BehaviorContext<DSFamilyTempSensorState, RoutingSlipFaulted> context)
        {
            context.Instance.EndTime = context.Data.Timestamp;
            context.Instance.Duration = context.Data.Duration;

            string faultSummary = string.Join(", ",
                context.Data.ActivityExceptions.Select(x => string.Format("{0}: {1}", x.Name, x.ExceptionInfo.Message)));

            context.Instance.FaultSummary = faultSummary;
        }
    }
}
