using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace <SolutionName>.ServiceDefinition
{
    public class <Name>Service : Service
    {
        private I<Name>Manager <name>Manager;

        public <Name>Service(I<Name>Manager <name>Manager)
        {
            this.<name>Manager = <name>Manager;
        }

        public Create<Name>Response Create(Create<Name>Request request)
        {
            var response = new Create<Name>Response();
            
            <name>Manager.Create(request.<Name>);
          
            return response;
        }

        public Read<Name>Response Read(Read<Name>Request request)
        {
            var response = new Read<Name>Response();
            
            response.<Name> = <name>Manager.Read(request.<Name>);
            
            return response;
        }

        public Update<Name>Response Update(Update<Name>Request request)
        {
            var response = new Update<Name>Response();
            
            <name>Manager.Update(request.<Name>);
            
            return response;
        }

        public Delete<Name>Response Delete(Delete<Name>Request request)
        {
            var response = new Delete<Name>Response();
            
            <name>Manager.Delete(request.<Name>);
            
            return response;
        }
    }
}
