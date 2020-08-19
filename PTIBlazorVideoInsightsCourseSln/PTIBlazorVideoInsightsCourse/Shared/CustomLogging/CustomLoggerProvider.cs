using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace PTIBlazorVideoInsightsCourse.Shared.CustomLogging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private static Dictionary<string, ILogger> Loggers { get; set; } =
            new Dictionary<string, ILogger>();
        private CustomLoggerConfiguration CustomLoggerConfiguration { get; }

        public CustomLoggerProvider(CustomLoggerConfiguration customLoggerConfiguration)
        {
            this.CustomLoggerConfiguration = customLoggerConfiguration;
        }
        public ILogger CreateLogger(string categoryName)
        {
            if (Loggers.ContainsKey(categoryName))
                return Loggers[categoryName];
            else
            {
                ILogger customerLogger = new CustomLogger(LogLevel.Error,
                    this.CustomLoggerConfiguration);
                Loggers.Add(categoryName, customerLogger);
                return customerLogger;
            }
        }

        public void Dispose()
        {
        }
    }

    public class CustomLoggerConfiguration
    {
        public string AzureStorageKey { get; set; }
        public string AzureStorageAccountName { get;set; }
        public string AzureStorageUrl { get; set; }
    }

    public class CustomLogger : ILogger
    {
        private LogLevel LogLevel { get; set; }
        private CustomLoggerConfiguration CustomLoggerConfiguration { get; }

        public CustomLogger(LogLevel loglevel,
            CustomLoggerConfiguration customLoggerConfiguration)
        {
            this.LogLevel = LogLevel;
            this.CustomLoggerConfiguration = customLoggerConfiguration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.LogLevel == logLevel;
        }

        private class LogData: TableEntity
        {
            public DateTimeOffset CreatedTime { get; set; }
            public string Message { get; set; }
            public Exception ExceptionInfo { get; set; }
            public string ExceptionString { get; set; }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine("Custom Logger Worked!!!");
            Task.Run(() =>
            {
                //TODO: Add here your customized logic to handle logging
                //e.g. Store exceptions into a database
                Microsoft.Azure.Cosmos.Table.CloudTableClient cloudTableClient =
                    new Microsoft.Azure.Cosmos.Table.CloudTableClient
                    (new Uri(this.CustomLoggerConfiguration.AzureStorageUrl),
                    new Microsoft.Azure.Cosmos.Table.StorageCredentials(
                        keyValue: this.CustomLoggerConfiguration.AzureStorageKey,
                        accountName: this.CustomLoggerConfiguration.AzureStorageAccountName
                        ));
                LogData logData = new LogData()
                {
                    CreatedTime = DateTimeOffset.UtcNow,
                    ExceptionInfo = exception,
                    ExceptionString = exception.ToString(),
                    PartitionKey = Guid.NewGuid().ToString(),
                    Message = exception.Message,
                    Timestamp = DateTimeOffset.UtcNow,
                    RowKey = Guid.NewGuid().ToString()
                };
                var tableReference = cloudTableClient.GetTableReference("ExceptionsLog");
                var insertOperation = TableOperation.InsertOrMerge(logData);
                var result = tableReference.Execute(insertOperation);
            });
        }
    }
}
