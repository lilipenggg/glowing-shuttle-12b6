using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApp {
	public class Startup {

		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app) {
			app.UseMvc();
			
			app.Run(context => {
				return context.Response.WriteAsync("Hello world");
			});

		}
	}
}

