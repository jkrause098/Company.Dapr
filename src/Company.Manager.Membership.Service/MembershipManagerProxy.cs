﻿using Company.iFX.Proxy;
using Company.Manager.Membership.Data;
using Company.Manager.Membership.Interface;
using ProtoBuf.Grpc;

namespace Company.Manager.Membership.Service
{
    public class MembershipManagerProxy
        : IMembershipManager
    {
        private readonly IMembershipManager m_Proxy;

        public MembershipManagerProxy()
        {
            m_Proxy = Proxy.Create<IMembershipManager>();
        }

        public async Task<RegisterResponseBase> RegisterMemberAsync(
            RegisterRequestBase registerRequest,
            CallContext context = default)
        {
            return await m_Proxy.RegisterMemberAsync(registerRequest, context);
        }
    }
}
