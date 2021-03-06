﻿namespace Microsoft.HockeyApp.DataContracts
{
    using System.Collections.Generic;

    using Channel;
    using Extensibility;
    using TestFramework;
    using Microsoft.Developer.Analytics.DataCollection.Model.v2;
#if WINDOWS_PHONE || WINDOWS_STORE || WINDOWS_UWP
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
    using Assert = Xunit.Assert;
    using DataPlatformModel = Microsoft.Developer.Analytics.DataCollection.Model.v2;

    [TestClass]
    public class SessionStateTelemetryTest
    {
        [TestMethod]
        public void ConstructorInitializesStateWithSpecifiedValue()
        {
            var telemetry = new SessionStateTelemetry(SessionState.End);
            Assert.Equal(SessionState.End, telemetry.State);
        }

        [TestMethod]
        public void ParameterizedConstructorPerformsDefaultInitialization()
        {
            var telemetry = new SessionStateTelemetry(SessionState.Start);
            Assert.NotNull(telemetry.Context);
        }

        [TestMethod]
        public void SessionStateIsStartByDefault()
        {
            var telemetry = new SessionStateTelemetry();
            Assert.Equal(SessionState.Start, telemetry.State);
        }

        [TestMethod]
        public void SessionStateCanBeSetByUser()
        {
            var telemetry = new SessionStateTelemetry();
            telemetry.State = SessionState.End;
            Assert.Equal(SessionState.End, telemetry.State);
        }

        [TestMethod]
        public void SerializeWritesStateAsExpectedByEndpoint()
        {
            var telemetry = new SessionStateTelemetry { State = SessionState.End };
            TelemetryItem<SessionStateData> envelope = TelemetryItemTestHelper.SerializeDeserializeTelemetryItem<SessionStateTelemetry, SessionStateData>(telemetry);
            Assert.Equal(DataPlatformModel.SessionState.End, envelope.Data.BaseData.State);
            Assert.Equal(2, envelope.Data.BaseData.Ver);
        }
    }
}
