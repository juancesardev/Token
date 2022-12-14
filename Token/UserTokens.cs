namespace JwTokens.Token
{
    public class UserTokens
    {
		public Guid Id { get; set; } 
		public string Token { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public TimeSpan Validity { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
		public string EmailId { get; set; } = string.Empty;
		public Guid GuidId { get; set; }
		public DateTime ExpiredTime { get; set; }
	}
}
