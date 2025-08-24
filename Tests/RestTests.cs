using Core.Utilities;
using CrossCutting.Providers;
using Microsoft.Extensions.Configuration;
using Business.Models;
using System.Text.Json;
using RestSharp;
using System.Net;
using CrossCutting.Static;

namespace Tests
{
    public class RestTests
    {
        private const string Category = "API";
        
        private string endpoint = string.Empty;
        private string usersMethod = string.Empty;
        private string invalidMethod = string.Empty;

        private RestSharpClient? client;
        protected readonly ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            IConfiguration testData = serviceProvider.GetTestData();
            IConfiguration configuration = serviceProvider.GetConfiguration();

            client = serviceProvider.GetRestClient();
            endpoint = testData.GetSection(ConfigurationKeys.Rest.Endpoint).Value ?? throw new ArgumentNullException("Endpoint is null");
            usersMethod = testData.GetSection(ConfigurationKeys.Rest.UsersMethod).Value ?? throw new ArgumentNullException("UsersMethod is null");
            invalidMethod = testData.GetSection(ConfigurationKeys.Rest.InvalidMethod).Value ?? throw new ArgumentNullException("InvalidMethod is null");
        }

        [TearDown]
        public void Teardown()
        {
            client = null;
        }

        /*
         * Tasks #1. Validate that the list of users can be received successfully
            Create and send request to https://jsonplaceholder.typicode.com/usersusing GET method
            Validate that user recives a list of users with the following information: "id",  "name", "username", "email", "address”,     "phone",   "website",  "company";
            Validate that user receives 200 OK response code. There are no error messages;
        */
        [Test]
        [Category(Category)]
        public async Task GetUsersWorks()
        {
            RestResponse response = await client!
                .SetEndpoint(endpoint)
                .SetMethod(usersMethod)
                .GetAsync();
            AssertResponse(response);

            List<User?>? users = JsonSerializer.Deserialize<List<User?>?>(response.Content!);
            Assert.That(users, Is.Not.Null, "Users list is null");
            if (users == null)
            {
                return;
            }

            Assert.That(users, Is.Not.Empty, "Users list is empty");

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user!.Id, Is.GreaterThan(0), "User ID is not greater than 0");
                    Assert.That(user!.Name, Is.Not.Null.And.Not.Empty, "User Name is null or empty");
                    Assert.That(user!.Username, Is.Not.Null.And.Not.Empty, "User Username is null or empty");
                    Assert.That(user!.Email, Is.Not.Null.And.Not.Empty, "User Email is null or empty");
                    Assert.That(user!.Address, Is.Not.Null, "User Address is null");
                    Assert.That(user!.Phone, Is.Not.Null.And.Not.Empty, "User Phone is null or empty");
                    Assert.That(user!.Website, Is.Not.Null.And.Not.Empty, "User Website is null or empty");
                    Assert.That(user!.Company, Is.Not.Null, "User Company is null");
                });
            }
        }

        /*
            Tasks #2. Validate response header for a list of users 
            Create and send request to https://jsonplaceholder.typicode.com/users using GET method.
            Validate content-type header exists in the obtained response.
            The value of the content-type header is application/json; charset=utf-8.
            Validate that user receives 200 OK response code. There are no error messages.
        */
        [Test]
        [Category(Category)]
        public async Task GetUsersHeaderWorks()
        {
            RestResponse response = await client!
                .SetEndpoint(endpoint)
                .SetMethod(usersMethod)
                .GetAsync();
            AssertResponse(response);

            Assert.Multiple(() =>
            {
                HeaderParameter? contentTypeHeader = response.Headers?
                    .FirstOrDefault(h => h.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));
                Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header is missing");
                if (contentTypeHeader != null)
                {
                    Assert.That(contentTypeHeader!.Value.ToString(), Is.EqualTo("application/json"), "Content-Type header value is incorrect");
                }

                HeaderParameter? charsetHeader = response.Headers?
                    .FirstOrDefault(h => h.Name.Equals("charset", StringComparison.OrdinalIgnoreCase));
                Assert.That(charsetHeader, Is.Not.Null, "charset header is missing");
                if (charsetHeader != null)
                {
                    Assert.That(contentTypeHeader!.Value.ToString(), Is.EqualTo("utf-8"), "charset header value is incorrect");
                }
            });
        }

        /*
            Tasks #3. Validate response header for a list of users 
            Create and send request to https://jsonplaceholder.typicode.com/usersusing GET method. 
            Validate that the content of the response body is the array of 10 users.
            Validate that each user should be with different ID.
            Validate that each user should be with non-empty Name and Username.
            Validate that each user contains the Company with non-empty Name 
            Validate that user receives 200 OK response code. There are no error messages.
        */
        [Test]
        [Category(Category)]
        public async Task GetUsersContentWorks()
        {
            RestResponse response = await client!
                .SetEndpoint(endpoint)
                .SetMethod(usersMethod)
                .GetAsync();
            AssertResponse(response);

            List<User?>? users = JsonSerializer.Deserialize<List<User?>?>(response.Content!);
            Assert.That(users, Is.Not.Null, "Users list is null");
            if (users == null)
            {
                return;
            }

            Assert.That(users!, Has.Count.EqualTo(10), "Users list does not contain 10 users");
            HashSet<int> userIds = [];
            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user!.Id, Is.GreaterThan(0), "User ID is not greater than 0");
                    Assert.That(user!.Name, Is.Not.Null.And.Not.Empty, "User Name is null or empty");
                    Assert.That(user!.Username, Is.Not.Null.And.Not.Empty, "User Username is null or empty");
                    Assert.That(user!.Company, Is.Not.Null, "User Company is null");
                    Assert.That(user!.Company.Name, Is.Not.Null.And.Not.Empty, "User Company Name is null or empty");
                    
                    bool isUnique = userIds.Add(user.Id);
                    Assert.That(isUnique, Is.True, $"User ID {user.Id} is not unique");
                });
            }
        }

        /*
            Tasks #4. Validate that user can be created
            Create and send request to https://jsonplaceholder.typicode.com/users using POST method with Name and Username fields 
            Validate that response is not empty and contains the ID value
            Validate that user receives 201 Created response code. There are no error messages
        */
        [Test]
        [Category(Category)]
        public async Task CreateUserWorks()
        {
            var newUser = new
            {
                name = "John Doe",
                username = "johndoe"
            };
            RestResponse response = await client!.PostAsync(endpoint + usersMethod, newUser);
            AssertResponse(response, HttpStatusCode.Created);
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response content is null or empty");

            User? createdUser = JsonSerializer.Deserialize<User?>(response.Content!);
            Assert.That(createdUser, Is.Not.Null, "Created user is null");
            if (createdUser == null)
            {
                return;
            }

            Assert.Multiple(() =>
            {
                Assert.That(createdUser!.Id, Is.GreaterThan(0), "Created user ID is not greater than 0");
                Assert.That(createdUser!.Name, Is.EqualTo(newUser.name), "Created user Name does not match");
                Assert.That(createdUser!.Username, Is.EqualTo(newUser.username), "Created user Username does not match");
            });
        }

        /*
            Tasks #5. Validate that user is notified if resource doesn’t exist
            Create and send a request to https://jsonplaceholder.typicode.com/invalidendpoint using GET method.
            Validate that user receives 404 Not Found response code. There are no error messages.  
        */
        [Test]
        [Category(Category)]
        public async Task GetInvalidEndpointWorks()
        {
            RestResponse response = await client!
                .SetEndpoint(endpoint)
                .SetMethod(invalidMethod)
                .GetAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.False, "Response was successful but should not be");
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code is not 404 Not Found");
            });
        }

        private static void AssertResponse(RestResponse response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True, "Response was not successful");
                Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Status code is not 200 OK");
            });
        }
    }
}
