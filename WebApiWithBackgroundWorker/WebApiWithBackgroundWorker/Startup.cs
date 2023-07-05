using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using WebApiWithBackgroundWorker.Domain.Interface;
using WebApiWithBackgroundWorker.Domain.Services;
using WebApiWithBackgroundWorker.Repository.InMemoryData;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;
using WebApiWithBackgroundWorker.Repository.RabbitMQAccess;

namespace WebApiWithBackgroundWorker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton<IInMemoryMessagesRepository, InMemoryMessagesRepository>();
            
            services.AddSingleton<IConnectionFactory>(ctx =>
            {
                //var connStr = this.Configuration["rabbit"];
                return new ConnectionFactory()
                {
                    //Uri = new Uri(connStr),
                    HostName = "localhost",
                    DispatchConsumersAsync = true // this is mandatory to have Async Subscribers
                };
            });
            
            services.AddSingleton<IRabbitPersistentConnection, RabbitPersistentConnection>();
            services.AddSingleton<IRabbitSubscriber, RabbitSubscriber>();

            var channel = System.Threading.Channels.Channel.CreateUnbounded<Message>();
            services.AddSingleton(channel);

          
            services.AddSingleton<IProducer>(ctx => {
                var producerChannel = ctx.GetRequiredService<System.Threading.Channels.Channel<Message>>();
                var producerLogger = ctx.GetRequiredService<ILogger<Producer>>();
                return new Producer(producerChannel.Writer, producerLogger);
            });

            services.AddSingleton<IEnumerable<IConsumer>>(ctx => {
                var consumeChannel = ctx.GetRequiredService<System.Threading.Channels.Channel<Message>>();
                var consumeLogger = ctx.GetRequiredService<ILogger<Consumer>>();
                var repo = ctx.GetRequiredService<IInMemoryMessagesRepository>();

                var consumers = Enumerable.Range(1, 5)
                                          .Select(i => new Consumer(consumeChannel.Reader, consumeLogger, i, repo))
                                          .ToArray();
                return consumers;
            });


            services.AddHostedService<BackgroundSubscriberWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
