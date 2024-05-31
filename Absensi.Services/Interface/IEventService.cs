using Absensi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services.Interface
{
	public interface IEventService
	{
        public BaseResponse<bool> Create(EventCreate req);
        public BaseResponse<List<EData>> Read(Paging paging, EventFilterData filterValue);
        public BaseResponse<bool> Update(EventData req);
        public BaseResponse<bool> Delete(long id, string Event);
        //public List<EventList> GetEventList();
        public List<EventList> GetEventList(string qEventSearch);
        public List<AttenderList> GetAttenderList(long id);


    }
}
