using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Net.Http;
using TodoApi.Dto;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.UnitTests.service
{
    public class DepartamentServiceTest
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        [Fact]
        public async Task GetExistingItem()
        {
            //Crea el cliente con la configuracion del archivo appsetting que se encuentra en la solucion
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()
                                        .UseConfiguration(GetIConfigurationRoot(System.IO.Directory.GetCurrentDirectory()))
                                        .UseEnvironment("Testing")
                                        );

            var client = server.CreateClient();
            client.DefaultRequestHeaders.Add("client", "1");


            var User = new LoginDTO()
            {
                Email = "flor_murano@hotmail.com",
                Password = "Cualquiera1*"
            };

            //Log y optencion de token
            var json = JsonConvert.SerializeObject(User);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Se intenta registrar y Loguear
            var responseRegister = await client.PostAsync("/api/Account/Register", data);
            responseRegister.EnsureSuccessStatusCode();


            var response = await client.PostAsync("/api/Account/Login", data);
            response.EnsureSuccessStatusCode();

            string Token = await response.Content.ReadAsStringAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // Crear departamento 

            var departarmentnew = new NewDepartmentDTO()
            {
                Name = "Recursos Humanos"
            };
            json = JsonConvert.SerializeObject(departarmentnew);
            var datadepartment = new StringContent(json, Encoding.UTF8, "application/json");

            var CreateDepartment = await client.PostAsync("/api/Department", datadepartment);


            string resultdepartmentnew = await CreateDepartment.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(resultdepartmentnew);

            long id = obj.id;

            //Obtener departamento 
            var content = await client.GetAsync("/api/Department/" + id);

            if (content.IsSuccessStatusCode)
            {

                string resultdepartmentcreate = await content.Content.ReadAsStringAsync();
                var obj2 = JsonConvert.DeserializeObject<NewDepartmentDTO>(resultdepartmentcreate);
                Assert.Equal(obj2.Name, departarmentnew.Name);
            }
            //Eliminar departamento

            var responsedelete = await client.DeleteAsync("/api/Department/" + id);

            responsedelete.EnsureSuccessStatusCode();



        }

    }
}