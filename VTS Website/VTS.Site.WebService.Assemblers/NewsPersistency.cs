using System;
using System.Collections.Generic;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public class NewsPersistency
    {
        public static IEnumerable<SystemNews> GetLast(int topCount)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            foreach (SystemNewsDto newsDto in service.NewsGetLast(topCount))
            {
                yield return SystemNewsAssembler.FromDtoToDomainObject(newsDto);
            }
            service.Close();
        }

        public IEnumerable<SystemNews> GetAll()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            foreach (SystemNewsDto dto in service.NewsGetAll())
            {
                yield return SystemNewsAssembler.FromDtoToDomainObject(dto);
            }
            service.Close();
        }

        public SystemNews Get(long id)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            return SystemNewsAssembler.FromDtoToDomainObject(service.NewsGet(id));
        }

        public void Update(SystemNews item)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.NewsUpdate(SystemNewsAssembler.FromDomainObjectToDto(item));
            service.Close();
        }

        public void Persist(SystemNews item)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.NewsPersist(SystemNewsAssembler.FromDomainObjectToDto(item));
            service.Close();
        }

        public void Delete(long id)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.NewsDelete(id);
            service.Close();
        }
    }
}
