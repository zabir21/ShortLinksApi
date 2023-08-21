using System.Text;

namespace DapperRelization.Configurations
{
    public class PropertyNameToSnakeCaseConvension: Dapper.FluentMap.Conventions.Convention
    {
        public PropertyNameToSnakeCaseConvension()
        {
            Properties().Configure(c => c.Transform(s=> ToSnakeCase(s)));
        }

        private string ToSnakeCase(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                return text;
            }

            if (text.Length < 2)
            {
                return text;
            }

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));

            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
