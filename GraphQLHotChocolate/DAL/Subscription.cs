using GraphQLHotChocolate.DAL.Entities;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.DAL
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<School>> OnSchoolCreate([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, School>("SchoolCreated", cancellationToken);
        }


        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Student>> OnStudentGet([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Student>("ReturnedStudent", cancellationToken);
        }
    }
}
