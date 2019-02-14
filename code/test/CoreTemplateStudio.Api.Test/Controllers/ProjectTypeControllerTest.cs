﻿using FluentAssertions;
using Microsoft.Templates.Api.Test.ResponseModels;
using Microsoft.Templates.Api.Test.Utilities;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Gen;
using Microsoft.Templates.Core.Locations;
using Microsoft.Templates.Core.Test.TestFakes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Templates.Api.Test.Controllers
{
    [Trait("ExecutionSet", "Minimum")]
    public class ProjectTypeControllerTest
    {
        [Fact]
        public void TestProjectType_ShouldHandleInvalidInput()
        {


            using (HttpClient client = new TestClientProvider().Client)
            {
                // Handle no sync
                ApiResponse response = GetResponseFromUrl(client, $"/api/projectType").Result;
                response.StatusCode.Should().Be(400, "The sync endpoint hasn't been called so GenContext.ToolBox will not be intialized yet");

                response.Value["message"].Should().Be("You must first sync templates before calling this endpoint",
                                                      "Sync endpoint hasn't been called yet so message should reflect that.");
            }
        }

        [Fact]
        public void TestProjectType_ShouldBeSuccessResponse()
        {
            using (HttpClient client = new TestClientProvider().Client)
            {
                GenContext.Bootstrap(new LocalTemplatesSource("."),
                                     new TestShell(Platforms.Web, ProgrammingLanguages.Any),
                                     Platforms.Web,
                                     ProgrammingLanguages.Any);
                ApiResponse response = GetResponseFromUrl(client, $"/api/projectType").Result;
                response.StatusCode.Should().Be(200, "The sync endpoint has been called so the ");
            }
        }

        private async Task<ApiResponse> GetResponseFromUrl(HttpClient client, string url)
        {
            HttpResponseMessage httpResponse = await client.GetAsync(url);
            string content = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(content);
        }
    }
}
