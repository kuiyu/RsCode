using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Douyin.Core
{
	public class DouyinHttpClientHandler:HttpClientHandler
	{
		public Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request,CancellationToken cancellationToken)
		{
			return this.SendAsync(request, cancellationToken);
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return base.SendAsync(request, cancellationToken);
		}
	}
}
