using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRelization.Context.Dto
{
    public class ShortLinkDto
    {
        public Guid Id { get; set; }
        public string? full_url { get; set; }
        public string? short_url { get; set; } 
        public object? created_date { get; set; }
    }

}
