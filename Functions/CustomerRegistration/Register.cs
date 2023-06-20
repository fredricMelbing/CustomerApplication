using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Functions.CustomerRegistration    
{
    public class Register
    {
        private readonly ILogger _logger;
        public Register(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Register>();
        }

        [Function("Register")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "CustomerApplicationDB",
            collectionName: "Customer",
            ConnectionStringSetting = "CosmosDbConnString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Customer> input)
        {
            try
            {
                if (input != null && input.Count > 0)
                {
                    using var client = new DocumentClient(
                    new Uri(Environment.GetEnvironmentVariable("EndpointUrl")),
                    Environment.GetEnvironmentVariable("PrimaryKey"));

                    string databaseName = "CustomerApplicationDB";
                    string collectionName = "Sales";

                    Uri collectionUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);

                    Customer customer = input[0];

                    var salesContact = client.CreateDocumentQuery<Sales>(collectionUri)
                        .Where(x => x.SalesId == customer.SalesId)
                        .Take(1).AsEnumerable().FirstOrDefault();



                    var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                    var mailClient = new SendGridClient(apiKey);
                    var from = new EmailAddress("fredric.melbing@gmail.com", "Fredric");
                    var to = new EmailAddress(salesContact.Email, salesContact.FullName);
                    var subject = $"New Customer: {customer.FullName}";
                    var plainTextContent = $"Congratz new Customer,{customer.Title}, {customer.FullName}, {customer.PhoneNumber}";
                    var htmlContent = $"<strong>Congratz new Customer,{customer.Title}, {customer.FullName}, {customer.PhoneNumber}</strong>";
                    var msg = MailHelper.CreateSingleEmail(
                        from,
                        to,
                        subject,
                        plainTextContent,
                        htmlContent
                        );
                    var response = await mailClient.SendEmailAsync(msg);
                    _logger.LogInformation($"Mail send {response.IsSuccessStatusCode} to: {salesContact.Email} ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }            
        }
    }

    public class Customer
    {
        public string CustomerId { get; set; }        
        public string FullName { get; set; }
        public string Title { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }        
        public string SalesId { get; set; }
    }
    public class Sales
    {        
        public string SalesId { get; set; }        
        public string FullName { get; set; }
        [Phone]        
        public string Phonenumber { get; set; }
        [EmailAddress]        
        public string Email { get; set; }        
    }
}
