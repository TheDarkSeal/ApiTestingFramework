using ApiTestingFramework.Builders;
using ApiTestingFramework.Clients;
using ApiTestingFramework.Tests;
using ApiTestingFramework.Tests.Assertions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTestingFramework.Test.Authentication
{
    public class AuthTest : TestBase
    {
        [Test]
        public async Task Create_Token_Should_Return_Valid_Token()
        {
            var request = AuthRequestBuilder
                .Create()
                .Build();

            var response = await AuthClient.AuthenticateAsync(request);
            response.ShouldContainToken();
        }
    }
}
