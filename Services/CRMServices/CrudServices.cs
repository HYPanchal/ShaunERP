using Core;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CRMServices
{
    public class CrudServices
    {
        private readonly CRMDBContext db;

        private readonly Lead lead;
        public CrudServices(CRMDBContext dbContext, Lead lead)
        {
            this.db = dbContext;
            this.lead = lead;
        }

        public Lead getLeadById(int id)
        {
            Lead templead = db.Leads.Find(id);

            return templead;
        }

        public List<Lead> getAllLead()
        {
            return db.Leads.ToList();
        }

        public Lead addlead(Lead lead)
        {
            db.Leads.Add(lead);
            db.SaveChanges();
            return lead;
        }

        public bool updateLead(int id, Lead updatelead)
        {
            Lead templead = db.Leads.Find(id);

            if (templead == null) { return false; }

            templead = updatelead;
            db.SaveChanges();

            return true;
        }

        public bool deleteLead(int id)
        {
            var tempLead = db.Leads.Find(id);

            if (tempLead == null) { return false; }

            db.Leads.Remove(tempLead);
            db.SaveChanges();

            return true;
        }
    }
}
