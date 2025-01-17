﻿using AutoMapper;
using Company.Engine.Registration.Interface;
using Company.iFX.Proxy;
using Company.Manager.Membership.Data.Mobile;
using Company.Manager.Membership.Interface.Mobile;
using ProtoBuf.Grpc;
using Serilog;
using Zametek.Utility.Logging;

namespace Company.Manager.Membership.Impl.Mobile
{
    [DiagnosticLogging(LogActive.On)]
    public class UseCases
        : IUseCases
    {
        private readonly ILogger m_Logger;
        private readonly IMapper m_Mapper;

        public UseCases()
        {
            m_Logger = Proxy.CreateLogger<IUseCases>();
            m_Mapper = iFX.Container.Container.GetService<IMapper>();
        }

        public async Task<RegisterResponse> RegisterMemberAsync(
            RegisterRequest registerRequest,
            CallContext context = default)
        {
            m_Logger.Information($"{nameof(RegisterMemberAsync)} Invoked");
            m_Logger.Information($"{nameof(RegisterMemberAsync)} {registerRequest.Name}");

            Engine.Registration.Data.RegisterRequestBase engineRegisterRequest =
                m_Mapper.Map<Engine.Registration.Data.RegisterRequestBase>(registerRequest);

            IRegistrationEngine registrationEngine = Proxy.Create<IRegistrationEngine>();
            Engine.Registration.Data.RegisterResponseBase engineResponse = await registrationEngine.RegisterAsync(engineRegisterRequest, context.CancellationToken);

            RegisterResponse registerResponse = m_Mapper.Map<RegisterResponse>(engineResponse);

            return registerResponse;
        }
    }
}
