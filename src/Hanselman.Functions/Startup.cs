using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using HttpClientFactory.Azure.WebJobs.Extensions;
using Microsoft.Azure.WebJobs.Description;
using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host.Config;
using Hanselman.Functions;

[assembly: WebJobsStartup(typeof(HttpClientFactoryWebJobsStartup))]

namespace HttpClientFactory.Azure.WebJobs.Extensions
{
    /// <summary>
    /// Class defining a startup configuration action for HttpClientFactory binding extensions, which will be performed as part of host startup.
    /// </summary>
    public class HttpClientFactoryWebJobsStartup : IWebJobsStartup
    {
        /// <summary>
        /// Performs the startup configuration action for HttpClientFactory binding extensions.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> that can be used to configure the host.</param>
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<HttpClientFactoryExtensionConfigProvider>();

            builder.Services.AddHttpClient();
            builder.Services.Configure<HttpClientFactoryOptions>(options => options.SuppressHandlerScope = true);
        }
    }
}

namespace Hanselman.Functions
{

    [Binding]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public sealed class HttpClientFactoryAttribute : Attribute
    { }

    [Extension("HttpClientFactory")]
    class HttpClientFactoryExtensionConfigProvider : IExtensionConfigProvider
    {
        readonly IHttpClientFactory httpClientFactory;

        public HttpClientFactoryExtensionConfigProvider(IHttpClientFactory httpClientFactory) => 
            this.httpClientFactory = httpClientFactory;

        public void Initialize(ExtensionConfigContext context)
        {
            var bindingAttributeBindingRule = context.AddBindingRule<HttpClientFactoryAttribute>();
            bindingAttributeBindingRule.BindToInput<HttpClient>((httpClientFactoryAttribute) =>
            {
                return httpClientFactory.CreateClient();
            });
        }
    }
}
