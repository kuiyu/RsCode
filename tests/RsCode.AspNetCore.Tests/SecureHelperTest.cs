using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace RsCode
{
	public class SecureHelperTest
	{
		[Fact]
		public void AesEncryptTest()
		{
			var s = SecureHelper.AESEncrypt("111111", "7f37d53bdebe47a084c5");
			var s2 = SecureHelper.AESDecrypt(s, "7f37d53bdebe47a084c5");
			Assert.Equal(s2, "111111");
		}
	}
}
