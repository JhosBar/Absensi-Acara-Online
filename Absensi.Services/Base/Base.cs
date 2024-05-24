using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services.Base
{
    public class BaseRequest
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    public class BasePaging
    {
        public string? SortBy { get; set; }
        public string? Column { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
    }

    public class BaseResponse<T>
    {
        public T? Result { get; set; }
        public T? Resuls { get; set; }
        public string? Message { get; set; }
        public int Total { get; set; }
    }

    public class Paging
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Dir { get; set; }
        public string Col { get; set; }
    }
}
