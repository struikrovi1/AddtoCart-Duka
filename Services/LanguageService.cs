using DataAccess;
using Entities;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class LanguageService
    {

        private readonly AgencyContext _context;
       

        public LanguageService(AgencyContext context )
        {
            _context = context;
       
        }

        public List<Language> GetAll()
        {


            return (_context.Languages.ToList());
        }

    
    
     

        
    }
}
